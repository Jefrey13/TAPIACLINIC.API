/// <summary>
/// Important event when an appointment is canceled, either by the patient or medical staff.
/// This can free up time slots and allow rescheduling.
/// </summary>
namespace Domain.Enums;
public class AppointmentCanceledEvent
{
    public int AppointmentId { get; }
    public int PatientId { get; }

    public AppointmentCanceledEvent(int appointmentId, int patientId)
    {
        AppointmentId = appointmentId;
        PatientId = patientId;
    }
}