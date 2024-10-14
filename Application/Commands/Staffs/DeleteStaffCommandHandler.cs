using Application.Commands.Staffs;
using Application.Exceptions;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Handlers.Staffs
{
    public class DeleteStaffCommandHandler : IRequestHandler<DeleteStaffCommand, Unit>
    {
        private readonly IStaffRepository _staffRepository;

        public DeleteStaffCommandHandler(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        public async Task<Unit> Handle(DeleteStaffCommand request, CancellationToken cancellationToken)
        {
            var staff = await _staffRepository.GetByIdAsync(request.Id);

            if (staff == null)
            {
                throw new NotFoundException(nameof(Staff), request.Id);
            }

            await _staffRepository.DeleteAsync(staff);

            return Unit.Value;
        }
    }
}