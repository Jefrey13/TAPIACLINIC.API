using Application.Commands.Staffs;
using Application.Exceptions;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using System.Threading.Tasks;

namespace Application.Commands.Staffs
{
    /// <summary>
    /// Handler for deleting a staff member by ID.
    /// The associated user is not deleted.
    /// </summary>
    public class DeleteStaffCommandHandler : IRequestHandler<DeleteStaffCommand, Unit>
    {
        private readonly IStaffRepository _staffRepository;

        public DeleteStaffCommandHandler(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        public async Task<Unit> Handle(DeleteStaffCommand request, CancellationToken cancellationToken)
        {
            // Check if the staff member exists
            var staff = await _staffRepository.GetByIdAsync(request.Id);

            if (staff == null)
            {
                throw new NotFoundException(nameof(Staff), request.Id);
            }

            // Delete the staff member but not the associated user
            await _staffRepository.DeleteAsync(staff);

            return Unit.Value; // Return Unit to indicate completion
        }
    }
}