using Domain.Entities;

/// <summary>
/// Interface for generating a summary of a medical consultation, including diagnosis, treatment,
/// and the date of the next scheduled appointment (if any).
/// </summary>
namespace Domain.Services;
public interface IConsultationSummaryGeneratorService
{
    /// <summary>
    /// Generates a summary of the medical consultation.
    /// </summary>
    /// <param name="consultation">The consultation for which the summary is generated.</param>
    /// <returns>A string summary of the consultation.</returns>
    string GenerateSummary(Consultation consultation);
}