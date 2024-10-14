using Application.Models;
using MediatR;

namespace Application.Commands.Surgeries
{
    /// <summary>
    /// Command to create a new Surgery.
    /// </summary>
    public class CreateSurgeryCommand : IRequest<int>
    {
        public SurgeryDto SurgeryDto { get; set; }

        public CreateSurgeryCommand(SurgeryDto surgeryDto)
        {
            SurgeryDto = surgeryDto;
        }
    }
}