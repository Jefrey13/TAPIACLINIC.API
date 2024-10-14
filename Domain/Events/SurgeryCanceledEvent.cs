/// <summary>
/// Event triggered when a surgery is canceled. Notifies the medical staff and frees up the operating room.
/// </summary>
namespace Domain.Enums;
public class SurgeryCanceledEvent
{
    public int SurgeryId { get; }
    public int PatientId { get; }

    public SurgeryCanceledEvent(int surgeryId, int patientId)
    {
        SurgeryId = surgeryId;
        PatientId = patientId;
    }
}