//using Domain.Enums;

//public class ScheduleSurgeryService
//{
//    private readonly IRecordSurgeryRepository _recordSurgeryRepository;
//    private readonly IScheduleRepository _scheduleRepository;

//    public ScheduleSurgeryService(IRecordSurgeryRepository recordSurgeryRepository, IScheduleRepository scheduleRepository)
//    {
//        _recordSurgeryRepository = recordSurgeryRepository;
//        _scheduleRepository = scheduleRepository;
//    }

//    public RecordSurgery ScheduleSurgery(int recordId, int surgeryId, DateRange surgerySchedule, SurgeryType surgeryType)
//    {
//        // Validate availability of the surgery schedule
//        if (!_scheduleRepository.IsTimeSlotAvailable(surgerySchedule))
//        {
//            throw new InvalidOperationException("The surgery schedule is not available.");
//        }

//        // Schedule the surgery
//        var recordSurgery = new RecordSurgery
//        {
//            RecordId = recordId,
//            SurgeryId = surgeryId,
//            SurgerySchedule = surgerySchedule,
//            SurgeryType = surgeryType,
//            CreatedAt = DateTime.Now,
//            UpdatedAt = DateTime.Now
//        };

//        _recordSurgeryRepository.AddAsync(recordSurgery);
//        return recordSurgery;
//    }
//}