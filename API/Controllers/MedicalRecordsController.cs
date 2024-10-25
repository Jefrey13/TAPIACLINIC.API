using Application.Commands.MedicalRecords;
using Application.Models.ReponseDtos;
using Application.Services;
using API.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRecordsController : ControllerBase
    {
        private readonly IMedicalRecordAppService _medicalRecordAppService;

        /// <summary>
        /// Initializes a new instance of the MedicalRecordsController class.
        /// </summary>
        /// <param name="medicalRecordAppService">The service for managing medical records.</param>
        public MedicalRecordsController(IMedicalRecordAppService medicalRecordAppService)
        {
            _medicalRecordAppService = medicalRecordAppService;
        }

        /// <summary>
        /// Retrieves all medical records in the system.
        /// </summary>
        /// <returns>A list of all medical records as MedicalRecordResponseDto.</returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<MedicalRecordResponseDto>>>> GetAllMedicalRecords()
        {
            var records = await _medicalRecordAppService.GetAllMedicalRecordsAsync();

            if (records == null || !records.Any())
            {
                return NotFound(new ApiResponse<IEnumerable<MedicalRecordResponseDto>>(false, "No medical records found", null, 404));
            }

            var response = new ApiResponse<IEnumerable<MedicalRecordResponseDto>>(true, "Medical records retrieved successfully", records, 200);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves a specific medical record by its ID.
        /// </summary>
        /// <param name="id">The ID of the medical record to retrieve.</param>
        /// <returns>The medical record details or a 404 if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<MedicalRecordResponseDto>>> GetMedicalRecordById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ApiResponse<MedicalRecordResponseDto>(false, "Invalid medical record ID", null, 400));
            }

            var record = await _medicalRecordAppService.GetMedicalRecordByIdAsync(id);
            if (record == null)
            {
                return NotFound(new ApiResponse<MedicalRecordResponseDto>(false, "Medical record not found", null, 404));
            }

            var response = new ApiResponse<MedicalRecordResponseDto>(true, "Medical record retrieved successfully", record, 200);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves a medical record by the patient's ID.
        /// </summary>
        /// <param name="patientId">The ID of the patient whose medical record is being retrieved.</param>
        /// <returns>The patient's medical record details or a 404 if not found.</returns>
        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult<ApiResponse<MedicalRecordResponseDto>>> GetMedicalRecordByPatientId(int patientId)
        {
            if (patientId <= 0)
            {
                return BadRequest(new ApiResponse<MedicalRecordResponseDto>(false, "Invalid patient ID", null, 400));
            }

            var record = await _medicalRecordAppService.GetMedicalRecordByPatientIdAsync(patientId);
            if (record == null)
            {
                return NotFound(new ApiResponse<MedicalRecordResponseDto>(false, "Medical record not found for the patient", null, 404));
            }

            var response = new ApiResponse<MedicalRecordResponseDto>(true, "Medical record for patient retrieved successfully", record, 200);
            return Ok(response);
        }

        /// <summary>
        /// Creates a new medical record in the system.
        /// </summary>
        /// <param name="command">The command containing the data for the new medical record.</param>
        /// <returns>The ID of the newly created medical record.</returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<int>>> CreateMedicalRecord([FromBody] CreateMedicalRecordCommand command)
        {
            if (command == null)
            {
                return BadRequest(new ApiResponse<int>(false, "Medical record data is required", default, 400));
            }

            var recordId = await _medicalRecordAppService.CreateMedicalRecordAsync(command);
            var response = new ApiResponse<int>(true, "Medical record created successfully", recordId, 201);
            return CreatedAtAction(nameof(GetMedicalRecordById), new { id = recordId }, response);
        }

        /// <summary>
        /// Updates an existing medical record in the system.
        /// </summary>
        /// <param name="id">The ID of the medical record to update.</param>
        /// <param name="command">The command containing the updated data for the medical record.</param>
        /// <returns>No content if the update is successful, or a BadRequest if the IDs do not match.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> UpdateMedicalRecord(int id, [FromBody] UpdateMedicalRecordCommand command)
        {
            if (id <= 0 || command == null)
            {
                return BadRequest(new ApiResponse<string>(false, "Invalid medical record ID or data", null, 400));
            }

            await _medicalRecordAppService.UpdateMedicalRecordAsync(command);
            var response = new ApiResponse<string>(true, "Medical record updated successfully", null, 200);
            return Ok(response);
        }

        /// <summary>
        /// Deletes a medical record from the system.
        /// </summary>
        /// <param name="id">The ID of the medical record to delete.</param>
        /// <returns>No content if the deletion is successful.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteMedicalRecord(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ApiResponse<string>(false, "Invalid medical record ID", null, 400));
            }

            var record = await _medicalRecordAppService.GetMedicalRecordByIdAsync(id);
            if (record == null)
            {
                return NotFound(new ApiResponse<string>(false, "Medical record not found", null, 404));
            }

            await _medicalRecordAppService.DeleteMedicalRecordAsync(id);
            var response = new ApiResponse<string>(true, "Medical record deleted successfully", null, 204);
            return Ok(response);
        }
    }
}