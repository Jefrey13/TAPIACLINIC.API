using Application.Commands.Staffs;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Handlers.Staffs
{
    /// <summary>
    /// Handler for creating a new staff member.
    /// Maps the StaffRequestDto to the Staff entity and saves it to the database.
    /// </summary>
    public class CreateStaffCommandHandler : IRequestHandler<CreateStaffCommand, bool>
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IMapper _mapper;

        public CreateStaffCommandHandler(IStaffRepository staffRepository, IMapper mapper)
        {
            _staffRepository = staffRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateStaffCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Validate incoming data
                if (request.StaffDto == null)
                {
                    throw new ValidationException("Request data name is required.");
                }

                // Map StaffRequestDto to Staff entity
                var staff = _mapper.Map<Staff>(request.StaffDto);

                BCrypt.Net.BCrypt.HashPassword(staff.User.Password);

                // Save to repository
                await _staffRepository.AddAsync(staff);

                // Validate the operation
                if (staff.Id <= 0)
                {
                    throw new OperationFailedException("Failed to create the staff member.");
                }

                return true;
            }
            catch (ValidationException ex)
            {
                throw new ValidationException($"Validation error: {ex.Message}");
            }
            catch (OperationFailedException ex)
            {
                throw new OperationFailedException($"Operation error: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"An unexpected error occurred while creating the staff member: {ex.InnerException}");
            }
        }
    }
}