﻿using Application.Commands;
using Application.Commands.Schedules;
using Application.Models.ReponseDtos;
using Application.Queries;
using Application.Queries.Schedules;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Impl
{
    /// <summary>
    /// Provides operations to manage schedules.
    /// Uses MediatR for handling commands and queries, and AutoMapper for mapping entities to DTOs.
    /// </summary>
    public class ScheduleAppService : IScheduleAppService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ScheduleAppService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Sends a command to create a new schedule.
        /// </summary>
        /// <param name="command">The schedule data in DTO format.</param>
        /// <returns>The ID of the newly created schedule.</returns>
        public async Task<int> CreateScheduleAsync(CreateScheduleCommand command)
        {
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Sends a command to update an existing schedule.
        /// </summary>
        /// <param name="command">The updated schedule details.</param>
        public async Task UpdateScheduleAsync(UpdateScheduleCommand command)
        {
            await _mediator.Send(command);
        }

        /// <summary>
        /// Sends a command to delete a schedule by its ID.
        /// </summary>
        /// <param name="id">The unique ID of the schedule to delete.</param>
        public async Task DeleteScheduleAsync(int id)
        {
            await _mediator.Send(new DeleteScheduleCommand(id));
        }

        /// <summary>
        /// Queries all schedules and maps them to DTOs.
        /// </summary>
        /// <returns>A list of all schedules as DTOs.</returns>
        public async Task<IEnumerable<ScheduleResponseDto>> GetAllSchedulesAsync()
        {
            return await _mediator.Send(new GetAllSchedulesQuery());
        }

        /// <summary>
        /// Retrieves a schedule by its ID.
        /// </summary>
        /// <param name="id">The unique ID of the schedule.</param>
        /// <returns>The DTO of the retrieved schedule.</returns>
        public async Task<ScheduleResponseDto> GetScheduleByIdAsync(int id)
        {
            return await _mediator.Send(new GetScheduleByIdQuery(id));
        }

        /// <summary>
        /// Retrieves schedules associated with a specific specialty.
        /// </summary>
        /// <param name="specialtyId">The ID of the specialty to filter schedules.</param>
        /// <returns>A list of schedules associated with the specialty.</returns>
        public async Task<IEnumerable<ScheduleResponseDto>> GetSchedulesBySpecialtyAsync(int specialtyId)
        {
            return await _mediator.Send(new GetSchedulesBySpecialtyQuery(specialtyId));
        }
    }
}