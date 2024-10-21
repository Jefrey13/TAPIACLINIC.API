using Application.Commands.Staffs;
using Application.Models.RequestDtos;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using System.Threading.Tasks;

namespace Application.Commands.Staffs
{
    /// <summary>
    /// Handler for creating a new staff member.
    /// Verifies if the user exists and creates a new user if necessary.
    /// </summary>
    public class CreateStaffCommandHandler : IRequestHandler<CreateStaffCommand, int>
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CreateStaffCommandHandler(IStaffRepository staffRepository, IUserRepository userRepository, IMapper mapper)
        {
            _staffRepository = staffRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateStaffCommand request, CancellationToken cancellationToken)
        {
            // Check if the user already exists by UserName
            var existingUser = await _userRepository.GetUserByUserNameAsync(request.StaffDto.User.UserName);

            User user;
            if (existingUser == null)
            {
                // Create a new user if not found
                user = _mapper.Map<User>(request.StaffDto.User);
                await _userRepository.AddAsync(user);
            }
            else
            {
                user = existingUser;
            }

            // Create the staff member with the associated user
            var staff = _mapper.Map<Staff>(request.StaffDto);
            staff.UserId = user.Id;
            await _staffRepository.AddAsync(staff);

            return staff.Id;
        }
    }
}