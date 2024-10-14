using Application.Commands.Staffs;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Handlers.Staffs
{
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
            // Create user first
            var user = _mapper.Map<User>(request.StaffDto.User);
            await _userRepository.AddAsync(user);

            // Create staff
            var staff = _mapper.Map<Staff>(request.StaffDto);
            staff.UserId = user.Id;
            await _staffRepository.AddAsync(staff);

            return staff.Id;
        }
    }
}