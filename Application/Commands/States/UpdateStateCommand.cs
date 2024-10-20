using Application.Models;
using MediatR;

namespace Application.Commands.States
{
    /// <summary>
    /// Command to update an existing state.
    /// </summary>
    public class UpdateStateCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public StateDto StateDto { get; set; }

        public UpdateStateCommand(int id, StateDto stateDto)
        {
            Id = id;
            StateDto = stateDto;
        }
    }
}