using Application.Commands.MedicalRecords;
using Application.Models.ReponseDtos;
using Application.Queries.MedicalRecords;
using Application.Queries.Staffs;
using Application.Queries.Users;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Impl
{
    /// <summary>
    /// Service implementation for handling medical record operations.
    /// Utilizes MediatR to send commands and queries, and AutoMapper for entity-DTO conversions.
    /// </summary>
    public class MedicalRecordAppService : IMedicalRecordAppService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IJwtTokenService _jwtTokenService;

        /// <summary>
        /// Constructor to initialize dependencies for MediatR and AutoMapper.
        /// </summary>
        /// <param name="mediator">MediatR instance to dispatch commands and queries.</param>
        /// <param name="mapper">AutoMapper instance to map between entities and DTOs.</param>
        public MedicalRecordAppService(IMediator mediator, IMapper mapper, IJwtTokenService jwtTokenService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _jwtTokenService = jwtTokenService;
        }

        /// <summary>
        /// Sends a command to create a medical record.
        /// The command is handled asynchronously through MediatR.
        /// </summary>
        /// <param name="command">Command with medical record creation details.</param>
        /// <returns>Returns the ID of the created medical record.</returns>
        public async Task<int> CreateMedicalRecordAsync(CreateMedicalRecordCommand command, string jwtToken)
        {
            var username = _jwtTokenService.GetUsernameFromToken(jwtToken);

            var user = await _mediator.Send(new GetUsersByUsernameQuery(username));

            var result = await _mediator.Send(new GetStaffByUserIdQuery(user.Id));

            command.MedicalRecordDto.StaffId = result.Id;
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Sends a command to update a medical record.
        /// The command is dispatched asynchronously through MediatR and does not return any data.
        /// </summary>
        /// <param name="command">Command containing the updated medical record data.</param>
        public async Task UpdateMedicalRecordAsync(UpdateMedicalRecordCommand command)
        {
            await _mediator.Send(command);
        }

        /// <summary>
        /// Sends a command to delete a medical record by its ID.
        /// The operation is asynchronous and is handled through MediatR.
        /// </summary>
        /// <param name="id">The ID of the medical record to delete.</param>
        public async Task DeleteMedicalRecordAsync(int id)
        {
            await _mediator.Send(new DeleteMedicalRecordCommand(id));
        }

        /// <summary>
        /// Sends a query to retrieve all medical records.
        /// The result is a collection of MedicalRecordResponseDto containing medical record information.
        /// </summary>
        /// <returns>Returns a list of medical records with their details.</returns>
        public async Task<IEnumerable<MedicalRecordResponseDto>> GetAllMedicalRecordsAsync()
        {
            return await _mediator.Send(new GetAllMedicalRecordsQuery());
        }

        /// <summary>
        /// Sends a query to retrieve a medical record by its ID.
        /// Returns detailed information about the medical record, or null if not found.
        /// </summary>
        /// <param name="id">The ID of the medical record to retrieve.</param>
        /// <returns>Returns a MedicalRecordResponseDto with the medical record's details.</returns>
        public async Task<MedicalRecordResponseDto> GetMedicalRecordByIdAsync(int id)
        {
            return await _mediator.Send(new GetMedicalRecordByIdQuery(id));
        }

        /// <summary>
        /// Sends a query to retrieve medical records by the patient's ID.
        /// Returns detailed information about the medical records for the specified patient.
        /// </summary>
        /// <param name="patientId">The ID of the patient whose medical records are being retrieved.</param>
        /// <returns>A list of MedicalRecordResponseDto with the patient's medical records details.</returns>
        public async Task<List<MedicalRecordResponseDto>> GetMedicalRecordsByPatientIdAsync(int patientId)
        {
            return await _mediator.Send(new GetMedicalRecordsByPatientIdQuery(patientId));
        }
    }
}