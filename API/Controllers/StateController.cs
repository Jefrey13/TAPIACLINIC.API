using Application.Commands.States;
using Application.Models;
using Application.Services;
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
        public async Task<ActionResult<IEnumerable<StateDto>>> GetAllStates()
        {
            var states = await _stateAppService.GetAllStatesAsync();
            return Ok(states);
        }

        /// <summary>
        /// Retrieves a specific state by its ID.
        /// </summary>
        /// <param name="id">The ID of the state to retrieve.</param>
        /// <returns>The StateDto of the requested state, or 404 if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<StateDto>> GetStateById(int id)
        {
            var state = await _stateAppService.GetStateByIdAsync(id);
            if (state == null)
            {
                return NotFound();
            }
            return Ok(state);
        }

        /// <summary>
        /// Creates a new state.
        /// </summary>
        /// <param name="command">The details of the state to be created.</param>
        /// <returns>The ID of the newly created state.</returns>
        [HttpPost]
        public async Task<ActionResult<int>> CreateState([FromBody] CreateStateCommand command)
        {
            var createdStateId = await _stateAppService.CreateStateAsync(command);
            return CreatedAtAction(nameof(GetStateById), new { id = createdStateId }, createdStateId);
        }

        /// <summary>
        /// Updates an existing state.
        /// </summary>
        /// <param name="id">The ID of the state to update.</param>
        /// <param name="command">The updated details of the state.</param>
        /// <returns>No content if the update is successful, or 400 if there is a mismatch of ID.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateState(int id, [FromBody] UpdateStateCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("State ID in the request does not match the one in the body.");
            }

            await _stateAppService.UpdateStateAsync(command);
            return NoContent();
        }

        /// <summary>
        /// Deletes a state by its ID.
        /// </summary>
        /// <param name="id">The ID of the state to delete.</param>
        /// <returns>No content if the deletion is successful.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteState(int id)
        {
            await _stateAppService.DeleteStateAsync(new DeleteStateCommand(id));
            return NoContent();
        }
    }
}