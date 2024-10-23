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
            var record = await _medicalRecordAppService.GetMedicalRecordByIdAsync(id);
            if (record == null)
            {
                var errorResponse = new ApiResponse<MedicalRecordResponseDto>(false, "Medical record not found", null, 404);
                return NotFound(errorResponse);
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
            var record = await _medicalRecordAppService.GetMedicalRecordByPatientIdAsync(patientId);
            if (record == null)
            {
                var errorResponse = new ApiResponse<MedicalRecordResponseDto>(false, "Medical record not found for the patient", null, 404);
                return NotFound(errorResponse);
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
            if (id != command.Id)
            {
                var errorResponse = new ApiResponse<string>(false, "Record ID mismatch.", null, 400);
                return BadRequest(errorResponse);
            }

            await _medicalRecordAppService.UpdateMedicalRecordAsync(command);
            var response = new ApiResponse<string>(true, "Medical record updated successfully", null, 204);
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
            await _medicalRecordAppService.DeleteMedicalRecordAsync(id);
            var response = new ApiResponse<string>(true, "Medical record deleted successfully", null, 204);
            return Ok(response);
        }
    }
}