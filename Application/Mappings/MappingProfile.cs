using AutoMapper;
using Application.Commands.Exams;
using Application.Commands.Schedules;
using Application.Commands.Specialties;
using Application.Commands.Surgeries;
using Application.Models;
using Domain.Entities;
using Application.Commands.Staffs;
using Application.Commands.Menus;

namespace Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Exams
            CreateMap<CreateExamCommand, Exam>();
            CreateMap<UpdateExamCommand, Exam>();
            CreateMap<Exam, ExamDto>();

            // Schedules
            CreateMap<CreateScheduleCommand, Schedule>();
            CreateMap<UpdateScheduleCommand, Schedule>();
            CreateMap<Schedule, ScheduleDto>();

            // Specialties
            CreateMap<CreateSpecialtyCommand, Specialty>();
            CreateMap<UpdateSpecialtyCommand, Specialty>();
            CreateMap<Specialty, SpecialtyDto>();

            // Surgeries
            CreateMap<CreateSurgeryCommand, Surgery>();
            CreateMap<UpdateSurgeryCommand, Surgery>();
            CreateMap<Surgery, SurgeryDto>();

            // Map from User entity to UserDto and vice versa
            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<Staff, StaffDto>()
               .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
               .ReverseMap();

            CreateMap<CreateStaffCommand, Staff>()
                .ForMember(dest => dest.User, opt => opt.Ignore());  // User is created separately

            // Mapping from Permission entity to PermissionDto
            CreateMap<Permission, PermissionDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Active));

            // Mapping from PermissionDto to Permission entity
            CreateMap<PermissionDto, Permission>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Active));

            // Mapping from Role entity to RoleDto
            CreateMap<Role, RoleDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Active))
                .ForMember(dest => dest.PermissionIds, opt => opt.MapFrom(src => src.RolePermissions.Select(rp => rp.PermissionId).ToList()))
                .ForMember(dest => dest.MenuIds, opt => opt.MapFrom(src => src.RoleMenus.Select(rm => rm.MenuId).ToList()));

            // Mapping from RoleDto to Role entity
            CreateMap<RoleDto, Role>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Active));

            CreateMap<Menu, MenuDto>().ReverseMap();

            // Menu to MenuDto mapping
            CreateMap<Menu, MenuDto>()
                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.Parent != null ? src.Parent.Id : (int?)null));

            // CreateMenuCommand to Menu mapping
            CreateMap<CreateMenuCommand, Menu>();

            // UpdateMenuCommand to Menu mapping
            CreateMap<UpdateMenuCommand, Menu>();
        }
    }
}