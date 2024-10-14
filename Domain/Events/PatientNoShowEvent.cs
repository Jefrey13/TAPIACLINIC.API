/// <summary>
/// This event is triggered when a patient does not show up for a scheduled appointment (no-show).
/// It may trigger actions like rescheduling or contacting the patient.
/// </summary>
namespace Domain.Enums; 
public class PatientNoShowEvent
{
    public int AppointmentId { get; }
    public int PatientId { get; }

    public PatientNoShowEvent(int appointmentId, int patientId)
    {
        AppointmentId = appointmentId;
        PatientId = patientId;
    }
}