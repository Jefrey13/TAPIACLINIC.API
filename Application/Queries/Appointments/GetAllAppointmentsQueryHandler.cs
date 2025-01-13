using Application.Exceptions;
using Application.Models.ResponseDtos;
using Application.Queries.Appointments;
using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.Handlers.Appointments
{
    /// <summary>
    /// Handler for retrieving all appointments.
    /// </summary>
    public class GetAllAppointmentsQueryHandler : IRequestHandler<GetAllAppointmentsQuery, IEnumerable<AppointmentResponseDto>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;

        public GetAllAppointmentsQueryHandler(IAppointmentRepository appointmentRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AppointmentResponseDto>> Handle(GetAllAppointmentsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var appointments = await _appointmentRepository.GetAllAsync(request.RoleId, request.Id);

                if (appointments == null || !appointments.Any())
                {
                    throw new NotFoundException("Appointments", "No appointments found");
                }

                return _mapper.Map<IEnumerable<AppointmentResponseDto>>(appointments);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving all appointments: {ex.Message}");
                throw;
            }
        }
    }
}
