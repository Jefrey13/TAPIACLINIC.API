using Application.Commands.Prescriptions;
using Application.Models.ResponseDtos;
using Application.Services;
using API.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Models.RequestDtos;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionsController : ControllerBase
    {
        private readonly IPrescriptionAppService _prescriptionAppService;

        public PrescriptionsController(IPrescriptionAppService prescriptionAppService)
        {
            _prescriptionAppService = prescriptionAppService;
        }

        /// <summary>
        /// Retrieves all prescriptions from the system.
        /// </summary>
        /// <returns>A list of prescriptions as PrescriptionResponseDto.</returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<PrescriptionResponseDto>>>> GetAllPrescriptions()
        {
            try
            {
                var prescriptions = await _prescriptionAppService.GetAllPrescriptionsAsync();

                if (prescriptions == null || !prescriptions.Any())
                {
                    return ResponseHelper.NotFound<IEnumerable<PrescriptionResponseDto>>("No prescriptions found.");
                }

                return ResponseHelper.Success(prescriptions, "Prescriptions retrieved successfully.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error<IEnumerable<PrescriptionResponseDto>>($"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a specific prescription by its ID.
        /// </summary>
        /// <param name="id">The ID of the prescription to retrieve.</param>
        /// <returns>The details of the prescription or a 404 if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<PrescriptionResponseDto>>> GetPrescriptionById(int id)
        {
            if (id <= 0)
            {
                return ResponseHelper.BadRequest<PrescriptionResponseDto>("Invalid prescription ID.");
            }

            try
            {
                var prescription = await _prescriptionAppService.GetPrescriptionByIdAsync(id);
                if (prescription == null)
                {
                    return ResponseHelper.NotFound<PrescriptionResponseDto>("Prescription not found.");
                }

                return ResponseHelper.Success(prescription, "Prescription retrieved successfully.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error<PrescriptionResponseDto>($"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves prescriptions for a specific patient by their ID.
        /// </summary>
        /// <param name="patientId">The ID of the patient whose prescriptions are to be retrieved.</param>
        /// <returns>A list of prescriptions for the specified patient.</returns>
        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<PrescriptionResponseDto>>>> GetPrescriptionsByPatientId(int patientId)
        {
            if (patientId <= 0)
            {
                return ResponseHelper.BadRequest<IEnumerable<PrescriptionResponseDto>>("Invalid patient ID.");
            }

            try
            {
                var prescriptions = await _prescriptionAppService.GetPrescriptionsByPatientIdAsync(patientId);
                if (prescriptions == null || !prescriptions.Any())
                {
                    return ResponseHelper.NotFound<IEnumerable<PrescriptionResponseDto>>("No prescriptions found for the specified patient.");
                }

                return ResponseHelper.Success(prescriptions, "Prescriptions for the patient retrieved successfully.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error<IEnumerable<PrescriptionResponseDto>>($"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Creates a new prescription in the system.
        /// </summary>
        /// <param name="command">The command containing the prescription details to create.</param>
        /// <returns>The ID of the newly created prescription.</returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<int>>> CreatePrescription([FromBody] PrescriptionRequestDto prescriptionRequestDto)
        {
            if (prescriptionRequestDto == null)
            {
                return ResponseHelper.BadRequest<int>("Prescription data is required.");
            }

            try
            {
                var prescriptionId = await _prescriptionAppService.CreatePrescriptionAsync(new CreatePrescriptionCommand(prescriptionRequestDto));
                return ResponseHelper.Success(prescriptionId, "Prescription created successfully.", 201);
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error<int>($"Error creating the prescription: {ex.Message}");
            }
        }

        /// <summary>
        /// Downloads a prescription PDF.
        /// </summary>
        /// <param name="id">The prescription ID to download.</param>
        /// <returns>A file response containing the PDF or an appropriate error response.</returns>
        [HttpGet("{id}/download")]
        public async Task<IActionResult> DownloadPrescription(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid prescription ID.");
            }

            try
            {
                var prescription = await _prescriptionAppService.GetPrescriptionByIdAsync(id);

                if (prescription == null)
                {
                    return NotFound("Prescription not found.");
                }

                var pdfBytes = _prescriptionAppService.GeneratePdf(prescription);

                // Return the PDF file as a response with the appropriate MIME type
                return File(pdfBytes, "application/pdf", $"Prescription_{id}.pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error generating the prescription PDF: {ex.Message}");
            }
        }

    }
}