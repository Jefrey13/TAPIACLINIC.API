using Application.Commands.Staffs;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using System.Threading.Tasks;

namespace Application.Commands.Staffs
{
    /// <summary>
    /// Handler for updating an existing staff member.
    /// Optionally updates the associated user.
    /// </summary>
    public class UpdateStaffCommandHandler : IRequestHandler<UpdateStaffCommand, Unit>
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UpdateStaffCommandHandler(IStaffRepository staffRepository, IUserRepository userRepository, IMapper mapper)
        {
            _staffRepository = staffRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateStaffCommand request, CancellationToken cancellationToken)
        {
            // Check if the staff member exists
            var staff = await _staffRepository.GetByIdAsync(request.Id);

            if (staff == null)
            {
                throw new NotFoundException(nameof(Staff), request.Id);
            }

            // Update the staff member with new data from the DTO
            _mapper.Map(request.StaffDto, staff);
            staff.UpdatedAt = DateTime.Now; // Update modification date
            await _staffRepository.UpdateAsync(staff);

            // Optionally update the associated user if necessary
            var user = await _userRepository.GetByIdAsync(staff.UserId);
            if (user != null && request.StaffDto.User != null)
            {
                // Update user information
                _mapper.Map(request.StaffDto.User, user);
                await _userRepository.UpdateAsync(user);
            }

            return Unit.Value; // Return Unit to indicate completion
        }
    }
}