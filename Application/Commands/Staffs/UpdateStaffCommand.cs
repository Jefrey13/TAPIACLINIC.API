using Application.Models;
using MediatR;

namespace Application.Commands.Staffs
{
    /// <summary>
    /// Command to update an existing staff.
    /// </summary>
    public class UpdateStaffCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public StaffDto StaffDto { get; set; }

        public UpdateStaffCommand(int id, StaffDto staffDto)
        {
            Id = id;
            StaffDto = staffDto;
        }
    }
}