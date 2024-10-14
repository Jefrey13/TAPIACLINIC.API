using Application.Commands.Specialties;
using Application.Models;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialtiesController : ControllerBase
    {
        private readonly ISpecialtyAppService _specialtyAppService;

        public SpecialtiesController(ISpecialtyAppService specialtyAppService)
        {
            _specialtyAppService = specialtyAppService;
        }

        /// <summary>
        /// Retrieves all medical specialties.
        /// </summary>
        /// <returns>A list of SpecialtyDto with details of all specialties.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpecialtyDto>>> GetAllSpecialties()
        {
            var specialties = await _specialtyAppService.GetAllSpecialtiesAsync();
            return Ok(specialties);
        }

        /// <summary>
        /// Retrieves a specific medical specialty by its ID.
        /// </summary>
        /// <param name="id">The ID of the specialty to retrieve.</param>
        /// <returns>The SpecialtyDto of the requested specialty, or 404 if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<SpecialtyDto>> GetSpecialtyById(int id)
        {
            var specialty = await _specialtyAppService.GetSpecialtyByIdAsync(id);
            if (specialty == null)
            {
                return NotFound();
            }
            return Ok(specialty);
        }

        /// <summary>
        /// Creates a new medical specialty.
        /// </summary>
        /// <param name="specialtyDto">The details of the specialty to be created.</param>
        /// <returns>The ID of the newly created specialty.</returns>
        [HttpPost]
        public async Task<ActionResult<int>> CreateSpecialty([FromBody] SpecialtyDto specialtyDto)
        {
            var createdSpecialtyId = await _specialtyAppService.CreateSpecialtyAsync(new CreateSpecialtyCommand(specialtyDto));
            return CreatedAtAction(nameof(GetSpecialtyById), new { id = createdSpecialtyId }, createdSpecialtyId);
        }

        /// <summary>
        /// Updates an existing medical specialty.
        /// </summary>
        /// <param name="id">The ID of the specialty to update.</param>
        /// <param name="specialtyDto">The updated details of the specialty.</param>
        /// <returns>No content if the update is successful, 400 if the ID mismatch occurs.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSpecialty(int id, [FromBody] SpecialtyDto specialtyDto)
        {
            if (id != specialtyDto.Id)
            {
                return BadRequest("Specialty ID in the request does not match the one in the body.");
            }

            await _specialtyAppService.UpdateSpecialtyAsync(new UpdateSpecialtyCommand(id, specialtyDto));
            return NoContent();
        }

        /// <summary>
        /// Deletes a medical specialty by its ID.
        /// </summary>
        /// <param name="id">The ID of the specialty to delete.</param>
        /// <returns>No content if the deletion is successful.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpecialty(int id)
        {
            await _specialtyAppService.DeleteSpecialtyAsync(id);
            return NoContent();
        }
    }
}