using Application.Exceptions;
using Application.Models.ReponseDtos;
using Application.Models.ResponseDtos;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries.Appointments
{
    public class GetAppointmentsByStateQueryHandler : IRequestHandler<GetAppointmentsByStateQuery, IEnumerable<AppointmentResponseDto>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;

        public GetAppointmentsByStateQueryHandler(IAppointmentRepository appointmentRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AppointmentResponseDto>> Handle(GetAppointmentsByStateQuery request, CancellationToken cancellationToken)
        {
            var appointments = await _appointmentRepository.GetByStateAsync(request.StateName);

            if (appointments == null || !appointments.Any())
            {
                throw new NotFoundException("Appointment", request.StateName);
            }

            return _mapper.Map<IEnumerable<AppointmentResponseDto>>(appointments);
        }
    }
}