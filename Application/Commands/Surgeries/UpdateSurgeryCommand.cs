using Application.Models;
using MediatR;

namespace Application.Commands.Surgeries
{
    /// <summary>
    /// Command to update an existing Surgery.
    /// </summary>
    public class UpdateSurgeryCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public SurgeryDto SurgeryDto { get; set; }

        public UpdateSurgeryCommand(int id, SurgeryDto surgeryDto)
        {
            Id = id;
            SurgeryDto = surgeryDto;
        }
    }
}