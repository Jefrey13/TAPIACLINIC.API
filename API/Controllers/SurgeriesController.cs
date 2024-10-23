using Application.Commands.Surgeries;
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
        public async Task<ActionResult<ApiResponse<IEnumerable<SurgeryDto>>>> GetAllSurgeries()
        {
            var surgeries = await _surgeryAppService.GetAllSurgeriesAsync();
            var response = new ApiResponse<IEnumerable<SurgeryDto>>(true, "Surgeries retrieved successfully", surgeries, 200);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves a specific surgery by its ID.
        /// </summary>
        /// <param name="id">The ID of the surgery to retrieve.</param>
        /// <returns>The SurgeryDto of the requested surgery, or 404 if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<SurgeryDto>>> GetSurgeryById(int id)
        {
            var surgery = await _surgeryAppService.GetSurgeryByIdAsync(id);
            if (surgery == null)
            {
                var errorResponse = new ApiResponse<SurgeryDto>(false, "Surgery not found", null, 404);
                return NotFound(errorResponse);
            }
            var response = new ApiResponse<SurgeryDto>(true, "Surgery retrieved successfully", surgery, 200);
            return Ok(response);
        }

        /// <summary>
        /// Creates a new surgery record.
        /// </summary>
        /// <param name="surgeryDto">The details of the surgery to be created.</param>
        /// <returns>The ID of the newly created surgery.</returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<int>>> CreateSurgery([FromBody] SurgeryDto surgeryDto)
        {
            var createdSurgeryId = await _surgeryAppService.CreateSurgeryAsync(new CreateSurgeryCommand(surgeryDto));
            var response = new ApiResponse<int>(true, "Surgery created successfully", createdSurgeryId, 201);
            return CreatedAtAction(nameof(GetSurgeryById), new { id = createdSurgeryId }, response);
        }

        /// <summary>
        /// Updates an existing surgery record.
        /// </summary>
        /// <param name="id">The ID of the surgery to update.</param>
        /// <param name="surgeryDto">The updated details of the surgery.</param>
        /// <returns>No content if the update is successful, 400 if the ID mismatch occurs.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> UpdateSurgery(int id, [FromBody] SurgeryDto surgeryDto)
        {
            if (id != surgeryDto.Id)
            {
                var errorResponse = new ApiResponse<string>(false, "Surgery ID mismatch", null, 400);
                return BadRequest(errorResponse);
            }

            await _surgeryAppService.UpdateSurgeryAsync(new UpdateSurgeryCommand(id, surgeryDto));
            var response = new ApiResponse<string>(true, "Surgery updated successfully", null, 204);
            return Ok(response);
        }

        /// <summary>
        /// Deletes a surgery record by its ID.
        /// </summary>
        /// <param name="id">The ID of the surgery to delete.</param>
        /// <returns>No content if the deletion is successful.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteSurgery(int id)
        {
            await _surgeryAppService.DeleteSurgeryAsync(id);
            var response = new ApiResponse<string>(true, "Surgery deleted successfully", null, 204);
            return Ok(response);
        }
    }
}