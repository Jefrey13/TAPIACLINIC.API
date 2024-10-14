//public class RecordSurgeryService
//{
//    private readonly IRecordSurgeryRepository _recordSurgeryRepository;
//    private readonly ISurgeryStaffRepository _surgeryStaffRepository;

//    public RecordSurgeryService(IRecordSurgeryRepository recordSurgeryRepository, ISurgeryStaffRepository surgeryStaffRepository)
//    {
//        _recordSurgeryRepository = recordSurgeryRepository;
//        _surgeryStaffRepository = surgeryStaffRepository;
//    }

//    public void RecordSurgery(int recordId, int surgeryId, string complications, string result, List<SurgeryStaff> surgeryStaffList)
//    {
//        var recordSurgery = _recordSurgeryRepository.GetByRecordAndSurgeryId(recordId, surgeryId);

//        if (recordSurgery == null)
//        {
//            throw new InvalidOperationException("Surgery record not found.");
//        }

//        // Update the surgery details
//        recordSurgery.Complications = complications;
//        recordSurgery.Result = result;
//        recordSurgery.UpdatedAt = DateTime.Now;

//        _recordSurgeryRepository.UpdateAsync(recordSurgery);

//        // Record the medical staff involved in the surgery
//        foreach (var staff in surgeryStaffList)
//        {
//            _surgeryStaffRepository.AddAsync(new SurgeryStaff
//            {
//                SurgeryId = surgeryId,
//                StaffId = staff.StaffId,
//                StaffRole = staff.StaffRole,
//                ParticipationDuration = staff.ParticipationDuration,
//                CreatedAt = DateTime.Now,
//                UpdatedAt = DateTime.Now
//            });
//        }
//    }
//}