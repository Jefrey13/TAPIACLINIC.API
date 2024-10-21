using Application.Models.RequestDtos;
using MediatR;

namespace Application.Commands.Staffs
{
    /// <summary>
    /// Command to update an existing staff member.
    /// </summary>
    public class UpdateStaffCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public StaffRequestDto StaffDto { get; set; }

        public UpdateStaffCommand(int id, StaffRequestDto staffDto)
        {
            Id = id;
            StaffDto = staffDto;
        }
    }
}