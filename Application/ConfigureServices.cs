using Application.PipelineBehaviors;
using Application.Services.Impl;
using Application.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using QuestPDF.Infrastructure;

namespace Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {

            // Configurar la licencia de QuestPDF
            QuestPDF.Settings.License = LicenseType.Community;
            // Registrar servicios de la aplicación
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            });

            // Registrar el servicio de JWT en la capa de aplicación
            services.AddTransient<IJwtTokenService, JwtTokenService>();

            // Registrar otros servicios de la aplicación
            services.AddTransient<IExamAppService, ExamAppService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IScheduleAppService, ScheduleAppService>();
            services.AddTransient<ISpecialtyAppService, SpecialtyAppService>();
            services.AddTransient<ISurgeryAppService, SurgeryAppService>();
            services.AddTransient<IUserAppService, UserAppService>();
            services.AddTransient<IStaffAppService, StaffAppService>();
            services.AddTransient<IRoleAppService, RoleAppService>();
            services.AddTransient<IPermissionAppService, PermissionAppService>();
            services.AddTransient<IAppointmentAppService, AppointmentAppService>();
            services.AddTransient<IMedicalRecordAppService, MedicalRecordAppService>();
            services.AddTransient<IMenuAppService, MenuAppService>();
            services.AddTransient<IStateAppService, StateAppService>();
            services.AddTransient<IPrescriptionAppService, PrescriptionAppService>();
            services.AddTransient<IContactService, ContactService>();



            return services;
        }
    }
}