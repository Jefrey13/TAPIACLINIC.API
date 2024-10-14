/// <summary>
/// This event is relevant when a patient's medical record has been updated.
/// Can keep a history of changes or notify the staff about updates.
/// </summary>
namespace Domain.Enums; 
public class MedicalRecordUpdatedEvent
{
    public int MedicalRecordId { get; }
    public int PatientId { get; }
    public string UpdatedFields { get; }

    public MedicalRecordUpdatedEvent(int medicalRecordId, int patientId, string updatedFields)
    {
        MedicalRecordId = medicalRecordId;
        PatientId = patientId;
        UpdatedFields = updatedFields;
    }
}