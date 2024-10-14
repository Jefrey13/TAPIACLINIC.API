using Domain.Entities;

/// <summary>
/// Service that generates a summary of a medical consultation, including diagnosis, treatment,
/// and the date of the next scheduled appointment (if any).
/// </summary>
namespace Domain.Services.Impl; 
public class ConsultationSummaryGeneratorService : IConsultationSummaryGeneratorService
{
    /// <summary>
    /// Generates a summary of the medical consultation.
    /// </summary>
    /// <param name="consultation">The consultation for which the summary is generated.</param>
    /// <returns>A string summary of the consultation.</returns>
    public string GenerateSummary(Consultation consultation)
    {
        return $"Consulta realizada el {consultation.ConsultationDate.ToShortDateString()}:\n"
            + $"Diagnóstico: {consultation.Diagnosis}\n"
            + $"Tratamiento: {consultation.Treatment}\n"
            + $"Fecha de próxima consulta: {consultation.NextAppointmentDate?.ToShortDateString()}";
    }
}