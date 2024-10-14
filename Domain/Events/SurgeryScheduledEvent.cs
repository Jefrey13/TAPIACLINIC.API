using Domain.ValueObjects;

/// <summary>
/// This event is triggered when a surgery is scheduled. It notifies the involved staff
/// and prepares the necessary resources (operating room, equipment, etc.).
/// </summary>
namespace Domain.Enums;
public class SurgeryScheduledEvent
{
    public int SurgeryId { get; }
    public int PatientId { get; }
    public DateRange SurgerySchedule { get; }

    public SurgeryScheduledEvent(int surgeryId, int patientId, DateRange surgerySchedule)
    {
        SurgeryId = surgeryId;
        PatientId = patientId;
        SurgerySchedule = surgerySchedule;
    }
}