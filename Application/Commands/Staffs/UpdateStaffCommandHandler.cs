using Application.Commands.Staffs;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Handlers.Staffs
{
    /// <summary>
    /// Handler for updating an existing staff member.
    /// Maps updated fields from StaffRequestDto to the Staff entity.
    /// </summary>
    public class UpdateStaffCommandHandler : IRequestHandler<UpdateStaffCommand, bool>
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IMapper _mapper;

        public UpdateStaffCommandHandler(IStaffRepository staffRepository, IMapper mapper)
        {
            _staffRepository = staffRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateStaffCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Validate the ID
                if (request.Id <= 0)
                {
                    throw new ValidationException("Invalid staff ID.");
                }

                // Retrieve staff entity
                var staff = await _staffRepository.GetByIdAsync(request.Id);
                if (staff == null)
                {
                    throw new NotFoundException(nameof(Staff), request.Id);
                }

                // Map updated fields
                _mapper.Map(request.StaffDto, staff);

                // Save changes
                await _staffRepository.UpdateAsync(staff);

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
                throw new Exception($"An unexpected error occurred while updating the staff member: {ex.Message}", ex);
            }
        }
    }
}