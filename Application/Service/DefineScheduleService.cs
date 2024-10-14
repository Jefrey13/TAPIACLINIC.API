//public class DefineScheduleService
//{
//    private readonly IScheduleRepository _scheduleRepository;

//    public DefineScheduleService(IScheduleRepository scheduleRepository)
//    {
//        _scheduleRepository = scheduleRepository;
//    }

//    public void DefineSchedule(int staffId, string dayOfWeek, TimeSpan startTime, TimeSpan endTime)
//    {
//        // Validate that there are no conflicts with other schedules
//        if (_scheduleRepository.HasConflictingSchedule(staffId, dayOfWeek, startTime, endTime))
//        {
//            throw new InvalidOperationException("Conflict with another defined schedule.");
//        }

//        // Define the new schedule
//        var schedule = new Schedule
//        {
//            StaffId = staffId,
//            DayOfWeek = dayOfWeek,
//            StartTime = startTime,
//            EndTime = endTime,
//            Active = true,
//            CreatedAt = DateTime.Now,
//            UpdatedAt = DateTime.Now
//        };

//        _scheduleRepository.AddAsync(schedule);
//    }
//}