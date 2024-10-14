using Domain.Entities;

/// <summary>
/// Service that checks if a patient's medical record is complete, ensuring that certain
/// fields like allergies, past illnesses, and past surgeries are filled in.
/// </summary>
namespace Domain.Services.Impl; 
public class MedicalRecordCompletionService : IMedicalRecordCompletionService
{
    /// <summary>
    /// Checks if a patient's medical record is complete.
    /// </summary>
    /// <param name="record">The medical record to check for completeness.</param>
    /// <returns>True if the record is complete; otherwise, false.</returns>
    public bool IsMedicalRecordComplete(MedicalRecord record)
    {
        return !string.IsNullOrEmpty(record.Allergies)
            && !string.IsNullOrEmpty(record.PastIllnesses)
            && !string.IsNullOrEmpty(record.PastSurgeries);
    }
}