using Application.Models;
using MediatR;

namespace Application.Commands.States
{
    /// <summary>
    /// Command to create a new state.
    /// </summary>
    public class CreateStateCommand : IRequest<int>
    {
        public StateDto StateDto { get; set; }

        public CreateStateCommand(StateDto stateDto)
        {
            StateDto = stateDto;
        }
    }
}