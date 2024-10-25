using Application.Commands.Specialties;
using Application.Models;
using Application.Services;
using API.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<ActionResult<ApiResponse<IEnumerable<SpecialtyDto>>>> GetAllSpecialties()
        {
            var specialties = await _specialtyAppService.GetAllSpecialtiesAsync();

            if (specialties == null || !specialties.Any())
            {
                return NotFound(new ApiResponse<IEnumerable<SpecialtyDto>>(false, "No specialties found", null, 404));
            }

            var response = new ApiResponse<IEnumerable<SpecialtyDto>>(true, "Specialties retrieved successfully", specialties, 200);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves a specific medical specialty by its ID.
        /// </summary>
        /// <param name="id">The ID of the specialty to retrieve.</param>
        /// <returns>The SpecialtyDto of the requested specialty, or 404 if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<SpecialtyDto>>> GetSpecialtyById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ApiResponse<SpecialtyDto>(false, "Invalid specialty ID", null, 400));
            }

            var specialty = await _specialtyAppService.GetSpecialtyByIdAsync(id);
            if (specialty == null)
            {
                return NotFound(new ApiResponse<SpecialtyDto>(false, "Specialty not found", null, 404));
            }

            var response = new ApiResponse<SpecialtyDto>(true, "Specialty retrieved successfully", specialty, 200);
            return Ok(response);
        }

        /// <summary>
        /// Creates a new medical specialty.
        /// </summary>
        /// <param name="specialtyDto">The details of the specialty to be created.</param>
        /// <returns>The ID of the newly created specialty.</returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<int>>> CreateSpecialty([FromBody] SpecialtyDto specialtyDto)
        {
            if (specialtyDto == null)
            {
                return BadRequest(new ApiResponse<int?>(false, "Specialty data is required", null, 400));
            }

            var createdSpecialtyId = await _specialtyAppService.CreateSpecialtyAsync(new CreateSpecialtyCommand(specialtyDto));
            var response = new ApiResponse<int>(true, "Specialty created successfully", createdSpecialtyId, 201);
            return CreatedAtAction(nameof(GetSpecialtyById), new { id = createdSpecialtyId }, response);
        }

        /// <summary>
        /// Updates an existing medical specialty.
        /// </summary>
        /// <param name="id">The ID of the specialty to update.</param>
        /// <param name="specialtyDto">The updated details of the specialty.</param>
        /// <returns>No content if the update is successful, 400 if the ID mismatch occurs.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> UpdateSpecialty(int id, [FromBody] SpecialtyDto specialtyDto)
        {
            if (id <= 0 || specialtyDto == null)
            {
                return BadRequest(new ApiResponse<string>(false, "Invalid ID or specialty data", null, 400));
            }

            var existingSpecialty = await _specialtyAppService.GetSpecialtyByIdAsync(id);
            if (existingSpecialty == null)
            {
                return NotFound(new ApiResponse<string>(false, "Specialty not found", null, 404));
            }

            await _specialtyAppService.UpdateSpecialtyAsync(new UpdateSpecialtyCommand(id, specialtyDto));
            var response = new ApiResponse<string>(true, "Specialty updated successfully", null, 200);
            return Ok(response);
        }

        /// <summary>
        /// Deletes a medical specialty by its ID.
        /// </summary>
        /// <param name="id">The ID of the specialty to delete.</param>
        /// <returns>No content if the deletion is successful.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteSpecialty(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ApiResponse<string>(false, "Invalid specialty ID", null, 400));
            }

            var existingSpecialty = await _specialtyAppService.GetSpecialtyByIdAsync(id);
            if (existingSpecialty == null)
            {
                return NotFound(new ApiResponse<string>(false, "Specialty not found", null, 404));
            }

            await _specialtyAppService.DeleteSpecialtyAsync(id);
            var response = new ApiResponse<string>(true, "Specialty deleted successfully", null, 204);
            return Ok(response);
        }
    }
}