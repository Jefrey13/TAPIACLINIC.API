using Application.Commands.Surgeries;
using Application.Models;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurgeriesController : ControllerBase
    {
        private readonly ISurgeryAppService _surgeryAppService;

        public SurgeriesController(ISurgeryAppService surgeryAppService)
        {
            _surgeryAppService = surgeryAppService;
        }

        /// <summary>
        /// Retrieves all surgeries in the system.
        /// </summary>
        /// <returns>A list of SurgeryDto containing details of all surgeries.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SurgeryDto>>> GetAllSurgeries()
        {
            var surgeries = await _surgeryAppService.GetAllSurgeriesAsync();
            return Ok(surgeries);
        }

        /// <summary>
        /// Retrieves a specific surgery by its ID.
        /// </summary>
        /// <param name="id">The ID of the surgery to retrieve.</param>
        /// <returns>The SurgeryDto of the requested surgery, or 404 if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<SurgeryDto>> GetSurgeryById(int id)
        {
            var surgery = await _surgeryAppService.GetSurgeryByIdAsync(id);
            if (surgery == null)
            {
                return NotFound();
            }
            return Ok(surgery);
        }

        /// <summary>
        /// Creates a new surgery record.
        /// </summary>
        /// <param name="surgeryDto">The details of the surgery to be created.</param>
        /// <returns>The ID of the newly created surgery.</returns>
        [HttpPost]
        public async Task<ActionResult<int>> CreateSurgery([FromBody] SurgeryDto surgeryDto)
        {
            var createdSurgeryId = await _surgeryAppService.CreateSurgeryAsync(new CreateSurgeryCommand(surgeryDto));
            return CreatedAtAction(nameof(GetSurgeryById), new { id = createdSurgeryId }, createdSurgeryId);
        }

        /// <summary>
        /// Updates an existing surgery record.
        /// </summary>
        /// <param name="id">The ID of the surgery to update.</param>
        /// <param name="surgeryDto">The updated details of the surgery.</param>
        /// <returns>No content if the update is successful, 400 if the ID mismatch occurs.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSurgery(int id, [FromBody] SurgeryDto surgeryDto)
        {
            if (id != surgeryDto.Id)
            {
                return BadRequest("Surgery ID in the request does not match the one in the body.");
            }

            await _surgeryAppService.UpdateSurgeryAsync(new UpdateSurgeryCommand(id, surgeryDto));
            return NoContent();
        }

        /// <summary>
        /// Deletes a surgery record by its ID.
        /// </summary>
        /// <param name="id">The ID of the surgery to delete.</param>
        /// <returns>No content if the deletion is successful.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSurgery(int id)
        {
            await _surgeryAppService.DeleteSurgeryAsync(id);
            return NoContent();
        }
    }
}