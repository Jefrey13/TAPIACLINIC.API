using Application.Commands.MedicalRecords;
using Application.Models.ReponseDtos;
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
    public class MedicalRecordsController : ControllerBase
    {
        private readonly IMedicalRecordAppService _medicalRecordAppService;

        public MedicalRecordsController(IMedicalRecordAppService medicalRecordAppService)
        {
            _medicalRecordAppService = medicalRecordAppService;
        }

        /// <summary>
        /// Recupera todos los expedientes médicos del sistema.
        /// </summary>
        /// <returns>Una lista de expedientes médicos como MedicalRecordResponseDto.</returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<MedicalRecordResponseDto>>>> GetAllMedicalRecords()
        {
            try
            {
                var records = await _medicalRecordAppService.GetAllMedicalRecordsAsync();

                if (records == null || !records.Any())
                {
                    return ResponseHelper.NotFound<IEnumerable<MedicalRecordResponseDto>>("No se encontraron expedientes médicos");
                }

                return ResponseHelper.Success(records, "Expedientes médicos recuperados exitosamente");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error<IEnumerable<MedicalRecordResponseDto>>($"Ocurrió un error: {ex.Message}");
            }
        }

        /// <summary>
        /// Recupera un expediente médico específico por su ID.
        /// </summary>
        /// <param name="id">El ID del expediente médico a recuperar.</param>
        /// <returns>Los detalles del expediente médico o un 404 si no se encuentra.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<MedicalRecordResponseDto>>> GetMedicalRecordById(int id)
        {
            if (id <= 0)
            {
                return ResponseHelper.BadRequest<MedicalRecordResponseDto>("ID de expediente médico inválido");
            }

            try
            {
                var record = await _medicalRecordAppService.GetMedicalRecordByIdAsync(id);
                if (record == null)
                {
                    return ResponseHelper.NotFound<MedicalRecordResponseDto>("Expediente médico no encontrado");
                }

                return ResponseHelper.Success(record, "Expediente médico recuperado exitosamente");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error<MedicalRecordResponseDto>($"Ocurrió un error: {ex.Message}");
            }
        }

        /// <summary>
        /// Recupera un expediente médico por el ID del paciente.
        /// </summary>
        /// <param name="patientId">El ID del paciente cuyo expediente médico se recuperará.</param>
        /// <returns>Los detalles del expediente médico del paciente o un 404 si no se encuentra.</returns>
        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult<ApiResponse<MedicalRecordResponseDto>>> GetMedicalRecordByPatientId(int patientId)
        {
            if (patientId <= 0)
            {
                return ResponseHelper.BadRequest<MedicalRecordResponseDto>("ID de paciente inválido");
            }

            try
            {
                var record = await _medicalRecordAppService.GetMedicalRecordByPatientIdAsync(patientId);
                if (record == null)
                {
                    return ResponseHelper.NotFound<MedicalRecordResponseDto>("Expediente médico no encontrado para el paciente");
                }

                return ResponseHelper.Success(record, "Expediente médico para el paciente recuperado exitosamente");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error<MedicalRecordResponseDto>($"Ocurrió un error. Intente mas tarde");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<int>>> CreateMedicalRecord([FromBody] MedicalRecordRequestDto medicalRecordRequest)
        {
            // Extraer el JWT del encabezado Authorization
            var authorizationHeader = Request.Headers["Authorization"].ToString();
            var jwtToken = authorizationHeader.StartsWith("Bearer ") ? authorizationHeader.Substring("Bearer ".Length).Trim() : string.Empty;
            if (medicalRecordRequest == null)
            {
                return ResponseHelper.BadRequest<int>("Los datos del expediente médico son requeridos");
            }

            try
            {
                var recordId = await _medicalRecordAppService.CreateMedicalRecordAsync(new CreateMedicalRecordCommand(medicalRecordRequest), jwtToken);
                return ResponseHelper.Success(recordId, "Expediente médico creado exitosamente");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error<int>($"Error al crear el expediente médico: {ex.Message}");
            }
        }


        /// <summary>
        /// Actualiza un expediente médico existente en el sistema.
        /// </summary>
        /// <param name="id">El ID del expediente médico a actualizar.</param>
        /// <param name="command">El comando que contiene los datos actualizados del expediente médico.</param>
        /// <returns>Respuesta con estado del proceso.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> UpdateMedicalRecord(int id, [FromBody] UpdateMedicalRecordCommand command)
        {
            if (id <= 0 || command == null)
            {
                return ResponseHelper.BadRequest<string>("ID de expediente médico o datos inválidos");
            }

            try
            {
                await _medicalRecordAppService.UpdateMedicalRecordAsync(command);
                return ResponseHelper.Success("Expediente médico actualizado exitosamente");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error<string>($"Ocurrió un error: {ex.Message}");
            }
        }

        /// <summary>
        /// Elimina un expediente médico del sistema.
        /// </summary>
        /// <param name="id">El ID del expediente médico a eliminar.</param>
        /// <returns>Respuesta con estado del proceso.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteMedicalRecord(int id)
        {
            if (id <= 0)
            {
                return ResponseHelper.BadRequest<string>("ID de expediente médico inválido");
            }

            try
            {
                var record = await _medicalRecordAppService.GetMedicalRecordByIdAsync(id);
                if (record == null)
                {
                    return ResponseHelper.NotFound<string>("Expediente médico no encontrado");
                }

                await _medicalRecordAppService.DeleteMedicalRecordAsync(id);
                return ResponseHelper.Success("Expediente médico eliminado exitosamente");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error<string>($"Error al eliminar el expediente médico: {ex.Message}");
            }
        }
    }
}