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
using Application.Models.ResponseDtos;
using Application.Models.RequestDtos.UpdateRequestDto;

namespace Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapeo de User a UserResponseDto
            CreateMap<User, UserResponseDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Value))  // Mapear el valor del Email (Value Object)
                .ForMember(dest => dest.PhoneAddress, opt => opt.MapFrom(src => src.Phone.Value))  // Mapear el valor de Phone (Value Object)
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()))  // Convertir enum Gender a string
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
                .ForMember(dest => dest.IdCard, opt => opt.MapFrom(src => src.IdCard))
                .ForMember(dest => dest.StateId, opt => opt.MapFrom(src => src.State.Id))  // Mapear ID del estado
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State.Name))  // Mapear nombre del estado
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Role != null ? src.Role.Id : (int?)null))  // Manejar posibles valores nulos de Role
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role != null ? src.Role.Name : null));  // Manejar posibles valores nulos de Role

            // Mapeo de UserRequestDto a User
            CreateMap<UserRequestDto, User>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => new Email(src.Email)))  // Crear un nuevo Value Object para Email
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => new PhoneNumber(src.PhoneAddress)))  // Crear un nuevo Value Object para PhoneNumber
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => Enum.Parse<Gender>(src.Gender)))  // Convertir string a enum Gender
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
                .ForMember(dest => dest.IdCard, opt => opt.MapFrom(src => src.IdCard))
                .ForMember(dest => dest.StateId, opt => opt.MapFrom(src => src.StateId))  // Mapear ID del estado
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId))  // Mapear ID del rol
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));  // Actualizar el campo UpdatedAt automáticamente

            // Mapeo de Staff a StaffResponseDto
            CreateMap<Staff, StaffResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)) // Mapear Id del staff
                .ForMember(dest => dest.MinsaCode, opt => opt.MapFrom(src => src.MinsaCode)) // Mapear el código MINSA
                .ForMember(dest => dest.YearsExperience, opt => opt.MapFrom(src => src.YearsExperience)) // Mapear los años de experiencia
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.User.StateId == 1)) // Determinar si está activo en función del estado del usuario
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User)) // Mapear la relación con el usuario
                .ForMember(dest => dest.SpecialtyId, opt => opt.MapFrom(src => src.SpecialtyId)) // Mapear el ID de la especialidad
                .ForMember(dest => dest.SpecialtyName, opt => opt.MapFrom(src => src.Specialty != null ? src.Specialty.Name : null)) // Mapear el nombre de la especialidad
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt)) // Mapear la fecha de creación
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt)); // Mapear la fecha de actualización

            // Mapeo de StaffRequestDto a Staff
            CreateMap<StaffRequestDto, Staff>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore()) // El UserId se asignará después de crear el usuario
                .ForMember(dest => dest.SpecialtyId, opt => opt.MapFrom(src => src.SpecialtyId)) // Mapear el ID de la especialidad
                .ForMember(dest => dest.MinsaCode, opt => opt.MapFrom(src => src.MinsaCode)) // Mapear el código MINSA
                .ForMember(dest => dest.YearsExperience, opt => opt.MapFrom(src => src.YearsExperience)) // Mapear los años de experiencia
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // La fecha de creación se maneja en la entidad
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore()); // La fecha de actualización se maneja en la entidad


            // Mapping from AppointmentRequestDto to Appointment
            CreateMap<AppointmentRequestDto, Appointment>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.PatientId, opt => opt.MapFrom(src => src.PatientId))
                .ForMember(dest => dest.StaffId, opt => opt.MapFrom(src => src.StaffId))
                .ForMember(dest => dest.SpecialtyId, opt => opt.MapFrom(src => src.SpecialtyId))
                .ForMember(dest => dest.ScheduleId, opt => opt.MapFrom(src => src.ScheduleId))
                .ForMember(dest => dest.StateId, opt => opt.MapFrom(src => src.StateId))
                .ForMember(dest => dest.Reason, opt => opt.MapFrom(src => src.Reason))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())  // CreatedAt will be set when creating the entity
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore()); // UpdatedAt will be set when updating the entity

            // Mapping from Appointment to AppointmentResponseDto
            CreateMap<Appointment, AppointmentResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Patient, opt => opt.MapFrom(src => src.Patient))  // Map to UserResponseDto
                .ForMember(dest => dest.Staff, opt => opt.MapFrom(src => src.Staff))  // Map to StaffResponseDto
                .ForMember(dest => dest.Specialty, opt => opt.MapFrom(src => src.Specialty))  // Map to SpecialtyDto
                .ForMember(dest => dest.Schedule, opt => opt.MapFrom(src => src.Schedule))  // Map to ScheduleResponseDto
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))  // Map to StateDto
                .ForMember(dest => dest.Reason, opt => opt.MapFrom(src => src.Reason));

            // Mapeo para la entidad MedicalRecord y su DTO
            CreateMap<MedicalRecordRequestDto, MedicalRecord>();

            // Mapeo para la entidad MedicalRecord y su DTO
            CreateMap<MedicalRecord, MedicalRecordResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Patient, opt => opt.MapFrom(src => src.Patient)) // Mapear UserDto
                .ForMember(dest => dest.Staff, opt => opt.MapFrom(src => src.Staff)) // Mapear StaffResponseDto
                .ForMember(dest => dest.OpeningDate, opt => opt.MapFrom(src => src.OpeningDate))
                .ForMember(dest => dest.Allergies, opt => opt.MapFrom(src => src.Allergies))
                .ForMember(dest => dest.PastIllnesses, opt => opt.MapFrom(src => src.PastIllnesses))
                .ForMember(dest => dest.PastSurgeries, opt => opt.MapFrom(src => src.PastSurgeries))
                .ForMember(dest => dest.FamilyHistory, opt => opt.MapFrom(src => src.FamilyHistory))
                .ForMember(dest => dest.StateId, opt => opt.MapFrom(src => src.StateId))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Active))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt));


            // Mapeo para la entidad Menu y su DTO
            CreateMap<Menu, MenuDto>()
                .ReverseMap();


            CreateMap<Menu, MenuResponseDto>()
            .ForMember(dest => dest.Children, opt => opt.MapFrom(src => src.Children));


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

            // Mapping from Schedule to ScheduleResponseDto
            CreateMap<Schedule, ScheduleResponseDto>()
                .ForMember(dest => dest.Specialty, opt => opt.MapFrom(src => src.Specialty))
                .ForMember(dest => dest.DayOfWeek, opt => opt.MapFrom(src => src.DayOfWeek))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime));

            // Mapping from ScheduleRequestDto to Schedule
            CreateMap<ScheduleResquestDto, Schedule>()
                .ForMember(dest => dest.SpecialtyId, opt => opt.MapFrom(src => src.SpecialtyId))
                .ForMember(dest => dest.DayOfWeek, opt => opt.MapFrom(src => src.DayOfWeek))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => TimeSpan.Parse(src.StartTime)))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => TimeSpan.Parse(src.EndTime)));

            // Optional: Mapping from ScheduleResponseDto back to Schedule (if needed)
            CreateMap<ScheduleResponseDto, Schedule>()
                .ForMember(dest => dest.Specialty, opt => opt.Ignore()) // Assuming the Specialty object needs to be handled separately
                .ForMember(dest => dest.DayOfWeek, opt => opt.MapFrom(src => src.DayOfWeek))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime));

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
                .ForMember(dest => dest.StateName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.StateType, opt => opt.MapFrom(src => src.StateType))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Active));

            // Mapear de StateDto a State
            CreateMap<StateDto, State>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.StateName))
                .ForMember(dest => dest.StateType, opt => opt.MapFrom(src => src.StateType))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Active));

            CreateMap<LoginResponseDto, User>();

            // Mapping from UserUpdateRequestDto to User
            CreateMap<UserUpdateRequestDto, User>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => new PhoneNumber(src.PhoneAddress)))  // Crear un nuevo Value Object para PhoneNumber
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => Enum.Parse<Gender>(src.Gender)))  // Convertir string a enum Gender
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
                .ForMember(dest => dest.StateId, opt => opt.MapFrom(src => src.StateId))  // Mapear ID del estado
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId))  // Mapear ID del rol
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));  // Actualizar el campo UpdatedAt automáticamente

            // Mapping from StaffUpdateRequestDto to Staff
            CreateMap<StaffUpdateRequestDto, Staff>()
                .ForMember(dest => dest.YearsExperience, opt => opt.MapFrom(src => src.YearsExperience)) // Mapear los años de experiencia
                .ForMember(dest => dest.SpecialtyId, opt => opt.MapFrom(src => src.SpecialtyId)) // Mapear el ID de la especialidad
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));// Actualizar el campo UpdatedAt automáticamente

        }
    }
}