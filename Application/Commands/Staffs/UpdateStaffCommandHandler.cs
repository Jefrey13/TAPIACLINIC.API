using Application.Commands.Staffs;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Handlers.Staffs
{
    public class UpdateStaffCommandHandler : IRequestHandler<UpdateStaffCommand, Unit>
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IMapper _mapper;

        public UpdateStaffCommandHandler(IStaffRepository staffRepository, IMapper mapper)
        {
            _staffRepository = staffRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateStaffCommand request, CancellationToken cancellationToken)
        {
            var staff = await _staffRepository.GetByIdAsync(request.Id);

            if (staff == null)
            {
                throw new NotFoundException(nameof(Staff), request.Id);
            }

            _mapper.Map(request.StaffDto, staff);
            staff.UpdatedAt = DateTime.Now;
            await _staffRepository.UpdateAsync(staff);

            return Unit.Value;
        }
    }
}