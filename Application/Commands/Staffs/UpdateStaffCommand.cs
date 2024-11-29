using Application.Models.RequestDtos;
using Application.Models.RequestDtos.UpdateRequestDto;
using MediatR;

namespace Application.Commands.Staffs
{
    /// <summary>
    /// Command to update an existing staff member.
    /// </summary>
    public class UpdateStaffCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public StaffUpdateRequestDto StaffDto { get; set; }

        public UpdateStaffCommand(int id, StaffUpdateRequestDto staffDto)
        {
            Id = id;
            StaffDto = staffDto;
        }
    }
}