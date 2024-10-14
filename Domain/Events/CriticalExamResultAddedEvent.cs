/// <summary>
/// This event is triggered when a critical exam result is added.
/// Critical results may require immediate attention, triggering alerts.
/// </summary>
namespace Domain.Enums; 
public class CriticalExamResultAddedEvent
{
    public int ExamId { get; }
    public int PatientId { get; }
    public string Result { get; }

    public CriticalExamResultAddedEvent(int examId, int patientId, string result)
    {
        ExamId = examId;
        PatientId = patientId;
        Result = result;
    }
}