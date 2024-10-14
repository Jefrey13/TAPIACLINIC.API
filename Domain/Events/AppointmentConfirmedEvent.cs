/// <summary>
/// Event triggered when an appointment has been confirmed by the patient or medical staff.
/// Can trigger notifications or calendar slot bookings.
/// </summary>
namespace Domain.Enums;
public class AppointmentConfirmedEvent
{
    public int AppointmentId { get; }
    public int PatientId { get; }

    public AppointmentConfirmedEvent(int appointmentId, int patientId)
    {
        AppointmentId = appointmentId;
        PatientId = patientId;
    }
}