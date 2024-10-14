/// <summary>
/// Service responsible for calculating a patient's age based on their birthdate.
/// It calculates the difference in years between the current date and the birthdate.
/// </summary>
namespace Domain.Services.Impl; 
public class AgeCalculationService : IAgeCalculationService
{
    /// <summary>
    /// Calculates the age of a patient based on their birthdate.
    /// </summary>
    /// <param name="birthDate">The birthdate of the patient.</param>
    /// <returns>The calculated age of the patient.</returns>
    public int CalculateAge(DateTime birthDate)
    {
        var today = DateTime.Today;
        var age = today.Year - birthDate.Year;

        // If the birthday hasn't occurred yet this year, subtract one year from the age.
        if (birthDate.Date > today.AddYears(-age))
        {
            age--;
        }

        return age;
    }
}