using Application.Models;
using MediatR;

namespace Application.Commands.Specialties
{
    /// <summary>
    /// Command to create a new Specialty.
    /// </summary>
    public class CreateSpecialtyCommand : IRequest<int>
    {
        public SpecialtyDto SpecialtyDto { get; set; }

        public CreateSpecialtyCommand(SpecialtyDto specialtyDto)
        {
            SpecialtyDto = specialtyDto;
        }
    }
}