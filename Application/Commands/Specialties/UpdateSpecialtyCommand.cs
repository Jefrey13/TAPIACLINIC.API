using Application.Models;
using MediatR;

namespace Application.Commands.Specialties
{
    /// <summary>
    /// Command to update an existing Specialty.
    /// </summary>
    public class UpdateSpecialtyCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public SpecialtyDto SpecialtyDto { get; set; }

        public UpdateSpecialtyCommand(int id, SpecialtyDto specialtyDto)
        {
            Id = id;
            SpecialtyDto = specialtyDto;
        }
    }
}