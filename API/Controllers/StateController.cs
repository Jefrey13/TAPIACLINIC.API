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
            try
            {
                var states = await _stateAppService.GetAllStatesAsync();
                if (states == null || !states.Any())
                {
                    return ResponseHelper.NotFound<IEnumerable<StateDto>>("No states found.");
                }

                return ResponseHelper.Success(states, "States retrieved successfully.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error<IEnumerable<StateDto>>($"An error occurred: {ex.Message}");
            }
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
                return ResponseHelper.BadRequest<StateDto>("Invalid state ID.");
            }

            try
            {
                var state = await _stateAppService.GetStateByIdAsync(id);
                if (state == null)
                {
                    return ResponseHelper.NotFound<StateDto>("State not found.");
                }

                return ResponseHelper.Success(state, "State retrieved successfully.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error<StateDto>($"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Creates a new state.
        /// </summary>
        /// <param name="command">The details of the state to be created.</param>
        /// <returns>The ID of the newly created state.</returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<int>>> CreateState([FromBody] StateDto stateDto)
        {
            if (stateDto == null)
            {
                return ResponseHelper.BadRequest<int>("State data is required.");
            }

            try
            {
                var createdStateId = await _stateAppService.CreateStateAsync(new CreateStateCommand(stateDto)); ;
                return ResponseHelper.Success(createdStateId, "State created successfully.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error<int>($"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates an existing state.
        /// </summary>
        /// <param name="id">The ID of the state to update.</param>
        /// <param name="command">The updated details of the state.</param>
        /// <returns>A success message if the update is successful.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> UpdateState(int id, [FromBody] StateDto stateDto)
        {
            if (id <= 0 || stateDto == null)
            {
                return ResponseHelper.BadRequest<string>("Invalid state ID or data.");
            }

            try
            {
                var existingState = await _stateAppService.GetStateByIdAsync(id);
                if (existingState == null)
                {
                    return ResponseHelper.NotFound<string>("State not found.");
                }

                await _stateAppService.UpdateStateAsync(new UpdateStateCommand(id, stateDto));
                return ResponseHelper.Success<string>(null, "State updated successfully.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error<string>($"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes a state by its ID.
        /// </summary>
        /// <param name="id">The ID of the state to delete.</param>
        /// <returns>A success message if the deletion is successful.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteState(int id)
        {
            if (id <= 0)
            {
                return ResponseHelper.BadRequest<string>("Invalid state ID.");
            }

            try
            {
                var existingState = await _stateAppService.GetStateByIdAsync(id);
                if (existingState == null)
                {
                    return ResponseHelper.NotFound<string>("State not found.");
                }

                await _stateAppService.DeleteStateAsync(new DeleteStateCommand(id));
                return ResponseHelper.Success<string>(null, "State deleted successfully.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error<string>($"An error occurred: {ex.Message}");
            }
        }
    }
}
