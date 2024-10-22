using Application.Commands.MedicalRecords;
using Application.Models.ReponseDtos;
using Application.Services;
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
        public async Task<ActionResult<IEnumerable<MedicalRecordResponseDto>>> GetAllMedicalRecords()
        {
            var records = await _medicalRecordAppService.GetAllMedicalRecordsAsync();
            return Ok(records);
        }

        /// <summary>
        /// Retrieves a specific medical record by its ID.
        /// </summary>
        /// <param name="id">The ID of the medical record to retrieve.</param>
        /// <returns>The medical record details or a 404 if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalRecordResponseDto>> GetMedicalRecordById(int id)
        {
            var record = await _medicalRecordAppService.GetMedicalRecordByIdAsync(id);
            if (record == null)
            {
                return NotFound();
            }
            return Ok(record);
        }

        /// <summary>
        /// Retrieves a medical record by the patient's ID.
        /// </summary>
        /// <param name="patientId">The ID of the patient whose medical record is being retrieved.</param>
        /// <returns>The patient's medical record details or a 404 if not found.</returns>
        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult<MedicalRecordResponseDto>> GetMedicalRecordByPatientId(int patientId)
        {
            var record = await _medicalRecordAppService.GetMedicalRecordByPatientIdAsync(patientId);
            if (record == null)
            {
                return NotFound();
            }
            return Ok(record);
        }

        /// <summary>
        /// Creates a new medical record in the system.
        /// </summary>
        /// <param name="command">The command containing the data for the new medical record.</param>
        /// <returns>The ID of the newly created medical record.</returns>
        [HttpPost]
        public async Task<ActionResult<int>> CreateMedicalRecord([FromBody] CreateMedicalRecordCommand command)
        {
            var recordId = await _medicalRecordAppService.CreateMedicalRecordAsync(command);
            return CreatedAtAction(nameof(GetMedicalRecordById), new { id = recordId }, recordId);
        }

        /// <summary>
        /// Updates an existing medical record in the system.
        /// </summary>
        /// <param name="id">The ID of the medical record to update.</param>
        /// <param name="command">The command containing the updated data for the medical record.</param>
        /// <returns>No content if the update is successful, or a BadRequest if the IDs do not match.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMedicalRecord(int id, [FromBody] UpdateMedicalRecordCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("Record ID mismatch.");
            }

            await _medicalRecordAppService.UpdateMedicalRecordAsync(command);
            return NoContent();
        }

        /// <summary>
        /// Deletes a medical record from the system.
        /// </summary>
        /// <param name="id">The ID of the medical record to delete.</param>
        /// <returns>No content if the deletion is successful.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicalRecord(int id)
        {
            await _medicalRecordAppService.DeleteMedicalRecordAsync(id);
            return NoContent();
        }
    }
}