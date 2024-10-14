/// <summary>
/// Triggered when a new patient is registered in the system.
/// May trigger the creation of a medical record or send a welcome email.
/// </summary>
namespace Domain.Enums; 
public class PatientRegisteredEvent
{
    public int PatientId { get; }
    public string PatientName { get; }

    public PatientRegisteredEvent(int patientId, string patientName)
    {
        PatientId = patientId;
        PatientName = patientName;
    }
}