using System.Reflection;
using Application.PipelineBehaviors;
using Application.Services;
using Application.Services.Impl;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register AutoMapper with the executing assembly
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Register all validators in the current assembly
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Register MediatR services
            services.AddMediatR(cfg =>
            {
                // Register MediatR handlers
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

                // Add pipeline behaviors
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>)); // Logging
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>)); // Validation
            });

            // Register your application services
            services.AddTransient<IExamAppService, ExamAppService>();
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

            return services;
        }
    }
}