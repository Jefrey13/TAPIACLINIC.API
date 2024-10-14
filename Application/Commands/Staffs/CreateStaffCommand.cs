using Application.Models;
using MediatR;

namespace Application.Commands.Staffs
{
    /// <summary>
    /// Command to create a new staff.
    /// </summary>
    public class CreateStaffCommand : IRequest<int>
    {
        public StaffDto StaffDto { get; set; }

        public CreateStaffCommand(StaffDto staffDto)
        {
            StaffDto = staffDto;
        }
    }
}