using AutoMapper;
using Application.Commands.Exams;
using Application.Commands.Schedules;
using Application.Commands.Specialties;
using Application.Commands.Surgeries;
using Application.Models;
using Domain.Entities;
using Application.Commands.Staffs;
using Application.Commands.Menus;
using Domain.Enums;
using Domain.ValueObjects;
using Application.Models.ReponseDtos;
using Application.Models.RequestDtos;

namespace Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Map de User a UserDto
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Value))  // Mapear el valor del Value Object a string
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone.Value)); // Igual para PhoneNumber

            // Map de UserDto a User
            CreateMap<UserDto, User>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => new Email(src.Email)))  // Mapear de string a Value Object
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => new PhoneNumber(src.Phone)));

            // Mapeo para la entidad Staff y su DTO
            CreateMap<Staff, StaffDto>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ReverseMap();

            // Mapeo para la entidad Appointment y su DTO
            CreateMap<Appointment, AppointmentDto>()
                .ReverseMap();

            // Mapeo para la entidad MedicalRecord y su DTO
            CreateMap<MedicalRecord, MedicalRecordDto>()
                .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient.FirstName + " " + src.Patient.LastName))
                .ForMember(dest => dest.StaffName, opt => opt.MapFrom(src => src.Staff.User.FirstName + " " + src.Staff.User.LastName))
                .ReverseMap();

            // Mapeo para la entidad Menu y su DTO
            CreateMap<Menu, MenuDto>()
                .ReverseMap();

            // Mapeo para la entidad Permission y su DTO
            CreateMap<Permission, PermissionDto>()
                .ReverseMap();

            CreateMap<Role, RoleRequestDto>()
     .ForMember(dest => dest.PermissionIds, opt => opt.MapFrom(src => src.RolePermissions.Select(rp => rp.PermissionId)))
     .ForMember(dest => dest.MenuIds, opt => opt.MapFrom(src => src.RoleMenus.Select(rm => rm.MenuId)));

            CreateMap<RoleRequestDto, Role>();

            // Mapping from Role entity to RoleResponseDto
            CreateMap<Role, RoleResponseDto>()
                .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.RolePermissions.Select(rp => rp.Permission)))
                .ForMember(dest => dest.Menus, opt => opt.MapFrom(src => src.RoleMenus.Select(rm => rm.Menu)));

            // Mapeo para la entidad Schedule y su DTO
            CreateMap<Schedule, ScheduleDto>()
                .ReverseMap();

            // Mapeo para la entidad Specialty y su DTO
            CreateMap<Specialty, SpecialtyDto>()
                .ReverseMap();

            // Mapeo para la entidad Surgery y su DTO
            CreateMap<Surgery, SurgeryDto>()
                .ReverseMap();

            // Mapeo para la entidad Exam y su DTO
            CreateMap<Exam, ExamDto>()
                .ReverseMap();

            // Mapear de State a StateDto
            CreateMap<State, StateDto>()
                .ForMember(dest => dest.StateName, opt => opt.MapFrom(src => src.StateName))
                .ForMember(dest => dest.StateType, opt => opt.MapFrom(src => src.StateType))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Active));

            // Mapear de StateDto a State
            CreateMap<StateDto, State>()
                .ForMember(dest => dest.StateName, opt => opt.MapFrom(src => src.StateName))
                .ForMember(dest => dest.StateType, opt => opt.MapFrom(src => src.StateType))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Active)); 
        }
    }
}