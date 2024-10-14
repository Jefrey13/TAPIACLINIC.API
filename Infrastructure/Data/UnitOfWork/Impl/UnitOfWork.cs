using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.UnitOfWork.Impl;

public class UnitOfWork
{
    //private readonly ApplicationDbContext _context;

    //public UnitOfWork(ApplicationDbContext context,
    //                  IAppointmentRepository appointmentRepository,
    //                  IConsultationRepository consultationRepository,
    //                  IExamRepository examRepository,
    //                  IImageRepository imageRepository,
    //                  IMedicalRecordRepository medicalRecordRepository,
    //                  IMenuRepository menuRepository,
    //                  IPermissionRepository permissionRepository,
    //                  IRecordSurgeryRepository recordSurgeryRepository,
    //                  IRoleRepository roleRepository,
    //                  IScheduleRepository scheduleRepository,
    //                  ISpecialtyRepository specialtyRepository,
    //                  IStaffRepository staffRepository,
    //                  IStateRepository stateRepository,
    //                  ISurgeryRepository surgeryRepository,
    //                  ISurgeryStaffRepository surgeryStaffRepository,
    //                  ITokenRepository tokenRepository,
    //                  IUserRepository userRepository)
    //{
    //    _context = context;
    //    Appointments = appointmentRepository;
    //    Consultations = consultationRepository;
    //    Exams = examRepository;
    //    Images = imageRepository;
    //    MedicalRecords = medicalRecordRepository;
    //    Menus = menuRepository;
    //    Permissions = permissionRepository;
    //    RecordSurgeries = recordSurgeryRepository;
    //    Roles = roleRepository;
    //    Schedules = scheduleRepository;
    //    Specialties = specialtyRepository;
    //    Staffs = staffRepository;
    //    States = stateRepository;
    //    Surgeries = surgeryRepository;
    //    SurgeryStaffs = surgeryStaffRepository;
    //    Tokens = tokenRepository;
    //    Users = userRepository;
    //}

    //public IAppointmentRepository Appointments { get; }
    //public IConsultationRepository Consultations { get; }
    //public IExamRepository Exams { get; }
    //public IImageRepository Images { get; }
    //public IMedicalRecordRepository MedicalRecords { get; }
    //public IMenuRepository Menus { get; }
    //public IPermissionRepository Permissions { get; }
    //public IRecordSurgeryRepository RecordSurgeries { get; }
    //public IRoleRepository Roles { get; }
    //public IScheduleRepository Schedules { get; }
    //public ISpecialtyRepository Specialties { get; }
    //public IStaffRepository Staffs { get; }
    //public IStateRepository States { get; }
    //public ISurgeryRepository Surgeries { get; }
    //public ISurgeryStaffRepository SurgeryStaffs { get; }
    //public ITokenRepository Tokens { get; }
    //public IUserRepository Users { get; }

    //public async Task<int> CommitAsync()
    //{
    //    return await _context.SaveChangesAsync();
    //}

    //public void Dispose()
    //{
    //    _context.Dispose();
    //}
}