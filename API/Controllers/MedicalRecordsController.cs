using Application.Commands.MedicalRecords;
using Application.Models;
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

        public MedicalRecordsController(IMedicalRecordAppService medicalRecordAppService)
        {
            _medicalRecordAppService = medicalRecordAppService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicalRecordDto>>> GetAllMedicalRecords()
        {
            var records = await _medicalRecordAppService.GetAllMedicalRecordsAsync();
            return Ok(records);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalRecordDto>> GetMedicalRecordById(int id)
        {
            var record = await _medicalRecordAppService.GetMedicalRecordByIdAsync(id);
            if (record == null)
            {
                return NotFound();
            }
            return Ok(record);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateMedicalRecord([FromBody] CreateMedicalRecordCommand command)
        {
            var recordId = await _medicalRecordAppService.CreateMedicalRecordAsync(command);
            return CreatedAtAction(nameof(GetMedicalRecordById), new { id = recordId }, recordId);
        }

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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicalRecord(int id)
        {
            await _medicalRecordAppService.DeleteMedicalRecordAsync(id);
            return NoContent();
        }
    }
}