using Application.Commands.Prescriptions;
using Application.Models.ResponseDtos;
using Application.Queries.Prescriptions;
using AutoMapper;
using DinkToPdf;
using MediatR;
using QuestPDF.Fluent;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace Application.Services.Impl
{
    /// <summary>
    /// Service implementation for handling prescription operations.
    /// Utilizes MediatR to send commands and queries, and AutoMapper for entity-DTO conversions.
    /// </summary>
    public class PrescriptionAppService : IPrescriptionAppService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor to initialize dependencies for MediatR and AutoMapper.
        /// </summary>
        /// <param name="mediator">MediatR instance to dispatch commands and queries.</param>
        /// <param name="mapper">AutoMapper instance to map between entities and DTOs.</param>
        public PrescriptionAppService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Sends a command to create a prescription.
        /// The command is handled asynchronously through MediatR.
        /// </summary>
        /// <param name="command">Command with prescription creation details.</param>
        /// <returns>Returns the ID of the created prescription.</returns>
        public async Task<int> CreatePrescriptionAsync(CreatePrescriptionCommand command)
        {
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Sends a query to retrieve all prescriptions.
        /// The result is a collection of PrescriptionResponseDto containing prescription information.
        /// </summary>
        /// <returns>Returns a list of prescriptions with their details.</returns>
        public async Task<IEnumerable<PrescriptionResponseDto>> GetAllPrescriptionsAsync()
        {
            return await _mediator.Send(new GetAllPrescriptionsQuery());
        }

        /// <summary>
        /// Sends a query to retrieve a prescription by its ID.
        /// Returns detailed information about the prescription, or null if not found.
        /// </summary>
        /// <param name="id">The ID of the prescription to retrieve.</param>
        /// <returns>Returns a PrescriptionResponseDto with the prescription's details.</returns>
        public async Task<PrescriptionResponseDto> GetPrescriptionByIdAsync(int id)
        {
            return await _mediator.Send(new GetPrescriptionByIdQuery(id));
        }

        /// <summary>
        /// Sends a query to retrieve prescriptions by the patient's ID.
        /// Returns detailed information about the prescriptions for the specified patient.
        /// </summary>
        /// <param name="patientId">The ID of the patient whose prescriptions are being retrieved.</param>
        /// <returns>Returns a list of PrescriptionResponseDto with the patient's prescription details.</returns>
        public async Task<IEnumerable<PrescriptionResponseDto>> GetPrescriptionsByPatientIdAsync(int patientId)
        {
            return await _mediator.Send(new GetPrescriptionsByPatientIdQuery(patientId));
        }

        public byte[] GeneratePdf(PrescriptionResponseDto prescription)
        {
            var document = new PrescriptionDocument(prescription);
            return document.GeneratePdf();

        }
    }
}