using Application.Commands.Staffs;
using Application.Exceptions;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Handlers.Staffs
{
    /// <summary>
    /// Handler for deleting a staff member.
    /// Toggles the active state of the staff member.
    /// </summary>
    public class DeleteStaffCommandHandler : IRequestHandler<DeleteStaffCommand, bool>
    {
        private readonly IStaffRepository _staffRepository;

        public DeleteStaffCommandHandler(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        public async Task<bool> Handle(DeleteStaffCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Validate the ID
                if (request.Id <= 0)
                {
                    throw new ValidationException("Invalid staff ID.");
                }

                // Retrieve the staff member
                var staff = await _staffRepository.GetByIdAsync(request.Id);
                if (staff == null)
                {
                    throw new NotFoundException(nameof(Staff), request.Id);
                }

                // Toggle active state
                await _staffRepository.ToggleActiveStateAsync(staff);

                return true;
            }
            catch (ValidationException ex)
            {
                throw new ValidationException($"Validation error: {ex.Message}");
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException($"Staff member not found: {ex.Message}", request.Id);
            }
            catch (Exception ex)
            {
                throw new Exception($"An unexpected error occurred while deleting the staff member: {ex.Message}", ex);
            }
        }
    }
}