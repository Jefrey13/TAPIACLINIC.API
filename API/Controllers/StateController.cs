using Application.Commands.States;
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
    public class StateController : ControllerBase
    {
        private readonly IStateAppService _stateAppService;

        public StateController(IStateAppService stateAppService)
        {
            _stateAppService = stateAppService;
        }

        /// <summary>
        /// Retrieves all the states in the system.
        /// </summary>
        /// <returns>A list of StateDto containing details of all states.</returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<StateDto>>>> GetAllStates()
        {
            var states = await _stateAppService.GetAllStatesAsync();

            if (states == null || !states.Any())
            {
                return NotFound(new ApiResponse<IEnumerable<StateDto>>(false, "No states found", null, 404));
            }

            var response = new ApiResponse<IEnumerable<StateDto>>(true, "States retrieved successfully", states, 200);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves a specific state by its ID.
        /// </summary>
        /// <param name="id">The ID of the state to retrieve.</param>
        /// <returns>The StateDto of the requested state, or 404 if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<StateDto>>> GetStateById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ApiResponse<StateDto>(false, "Invalid state ID", null, 400));
            }

            var state = await _stateAppService.GetStateByIdAsync(id);
            if (state == null)
            {
                return NotFound(new ApiResponse<StateDto>(false, "State not found", null, 404));
            }

            var response = new ApiResponse<StateDto>(true, "State retrieved successfully", state, 200);
            return Ok(response);
        }

        /// <summary>
        /// Creates a new state.
        /// </summary>
        /// <param name="command">The details of the state to be created.</param>
        /// <returns>The ID of the newly created state.</returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<int>>> CreateState([FromBody] CreateStateCommand command)
        {
            if (command == null)
            {
                return BadRequest(new ApiResponse<int?>(false, "State data is required", null, 400));
            }

            var createdStateId = await _stateAppService.CreateStateAsync(command);
            var response = new ApiResponse<int>(true, "State created successfully", createdStateId, 201);
            return CreatedAtAction(nameof(GetStateById), new { id = createdStateId }, response);
        }

        /// <summary>
        /// Updates an existing state.
        /// </summary>
        /// <param name="id">The ID of the state to update.</param>
        /// <param name="command">The updated details of the state.</param>
        /// <returns>No content if the update is successful, or 400 if there is a mismatch of ID.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> UpdateState(int id, [FromBody] UpdateStateCommand command)
        {
            if (id <= 0 || command == null)
            {
                return BadRequest(new ApiResponse<string>(false, "Invalid state ID or data", null, 400));
            }

            var existingState = await _stateAppService.GetStateByIdAsync(id);
            if (existingState == null)
            {
                return NotFound(new ApiResponse<string>(false, "State not found", null, 404));
            }

            await _stateAppService.UpdateStateAsync(command);
            var response = new ApiResponse<string>(true, "State updated successfully", null, 200);
            return Ok(response);
        }

        /// <summary>
        /// Deletes a state by its ID.
        /// </summary>
        /// <param name="id">The ID of the state to delete.</param>
        /// <returns>No content if the deletion is successful.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteState(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ApiResponse<string>(false, "Invalid state ID", null, 400));
            }

            var existingState = await _stateAppService.GetStateByIdAsync(id);
            if (existingState == null)
            {
                return NotFound(new ApiResponse<string>(false, "State not found", null, 404));
            }

            await _stateAppService.DeleteStateAsync(new DeleteStateCommand(id));
            var response = new ApiResponse<string>(true, "State deleted successfully", null, 204);
            return Ok(response);
        }
    }
}