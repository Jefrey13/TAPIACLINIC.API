/// <summary>
/// Interface for calculating the age of a patient based on their birthdate.
/// </summary>
namespace Domain.Services;
public interface IAgeCalculationService
{
    /// <summary>
    /// Calculates the age of a patient based on their birthdate.
    /// </summary>
    /// <param name="birthDate">The birthdate of the patient.</param>
    /// <returns>The calculated age of the patient.</returns>
    int CalculateAge(DateTime birthDate);
}