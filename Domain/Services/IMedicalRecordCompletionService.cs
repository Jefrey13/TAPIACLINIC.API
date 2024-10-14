using Domain.Entities;

/// <summary>
/// Interface for checking if a patient's medical record is complete, ensuring that certain
/// fields like allergies, past illnesses, and past surgeries are filled in.
/// </summary>
namespace Domain.Services; 
public interface IMedicalRecordCompletionService
{
    /// <summary>
    /// Checks if a patient's medical record is complete.
    /// </summary>
    /// <param name="record">The medical record to check for completeness.</param>
    /// <returns>True if the record is complete; otherwise, false.</returns>
    bool IsMedicalRecordComplete(MedicalRecord record);
}