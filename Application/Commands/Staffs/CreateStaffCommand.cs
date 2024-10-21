using Application.Models.RequestDtos;
using MediatR;

namespace Application.Commands.Staffs
{
    /// <summary>
    /// Command to create a new staff member.
    /// </summary>
    public class CreateStaffCommand : IRequest<int>
    {
        public StaffRequestDto StaffDto { get; set; }

        public CreateStaffCommand(StaffRequestDto staffDto)
        {
            StaffDto = staffDto;
        }
    }
}