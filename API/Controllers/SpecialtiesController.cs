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
            try
            {
                var specialties = await _specialtyAppService.GetAllSpecialtiesAsync();
                if (specialties == null || !specialties.Any())
                {
                    return ResponseHelper.NotFound<IEnumerable<SpecialtyDto>>("No specialties found.");
                }

                return ResponseHelper.Success(specialties, "Specialties retrieved successfully.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error<IEnumerable<SpecialtyDto>>($"An error occurred: {ex.Message}");
            }
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
                return ResponseHelper.BadRequest<SpecialtyDto>("Invalid specialty ID.");
            }

            try
            {
                var specialty = await _specialtyAppService.GetSpecialtyByIdAsync(id);
                if (specialty == null)
                {
                    return ResponseHelper.NotFound<SpecialtyDto>("Specialty not found.");
                }

                return ResponseHelper.Success(specialty, "Specialty retrieved successfully.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error<SpecialtyDto>($"An error occurred: {ex.Message}");
            }
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
                return ResponseHelper.BadRequest<int>("Specialty data is required.");
            }

            try
            {
                var createdSpecialtyId = await _specialtyAppService.CreateSpecialtyAsync(new CreateSpecialtyCommand(specialtyDto));
                return ResponseHelper.Success(createdSpecialtyId, "Specialty created successfully.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error<int>($"{ex.Message}");
            }
        }

        /// <summary>
        /// Updates an existing medical specialty.
        /// </summary>
        /// <param name="id">The ID of the specialty to update.</param>
        /// <param name="specialtyDto">The updated details of the specialty.</param>
        /// <returns>A success message if the update is successful.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> UpdateSpecialty(int id, [FromBody] SpecialtyDto specialtyDto)
        {
            if (id <= 0 || specialtyDto == null)
            {
                return ResponseHelper.BadRequest<string>("Invalid ID or specialty data.");
            }

            try
            {
                var existingSpecialty = await _specialtyAppService.GetSpecialtyByIdAsync(id);
                if (existingSpecialty == null)
                {
                    return ResponseHelper.NotFound<string>("Specialty not found.");
                }

                await _specialtyAppService.UpdateSpecialtyAsync(new UpdateSpecialtyCommand(id, specialtyDto));
                return ResponseHelper.Success<string>(null, "Specialty updated successfully.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error<string>($"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes a medical specialty by its ID.
        /// </summary>
        /// <param name="id">The ID of the specialty to delete.</param>
        /// <returns>A success message if the deletion is successful.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteSpecialty(int id)
        {
            if (id <= 0)
            {
                return ResponseHelper.BadRequest<string>("Invalid specialty ID.");
            }

            try
            {
                var existingSpecialty = await _specialtyAppService.GetSpecialtyByIdAsync(id);
                if (existingSpecialty == null)
                {
                    return ResponseHelper.NotFound<string>("Specialty not found.");
                }

                await _specialtyAppService.DeleteSpecialtyAsync(id);
                return ResponseHelper.Success<string>(null, "Specialty deleted successfully.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error<string>($"An error occurred: {ex.Message}");
            }
        }
    }
}