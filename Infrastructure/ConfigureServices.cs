using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Data;
using Infrastructure.Email;
using Infrastructure.Logging;
using Domain.Repositories;
using Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Application.Services;
using Application.Services.Impl;
using Infrastructure.Recaptcha;
//using Infrastructure.Data.UnitOfWork.Impl;
//using Infrastructure.Data.UnitOfWork;

namespace Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register DbContext with connection string from appsettings.json
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DbConnectionString")
                ?? throw new InvalidOperationException("Database connection string 'DbConnectionString' is not configured or is null.")));

            //Recaptcha
            services.Configure<RecaptchaSettings>(configuration.GetSection("Recaptcha"));

            services.AddScoped<IRecaptchaService, RecaptchaService>();

            // Register JWT token service
            services.AddTransient<IJwtTokenService, JwtTokenService>();

            // Register email sender service
            services.AddTransient<IEmailSender, SmtpEmailSender>();

            // Register logging service
            services.AddTransient(typeof(ILoggerAdapter<>), typeof(SerilogAdapter<>));

            // Register Unit of Work
            //services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddMemoryCache();

            // Register repositories
            services.AddTransient<IAppointmentRepository, AppointmentRepository>();
            services.AddTransient<IConsultationExamRepository, ConsultationExamRepository>();
            services.AddTransient<IConsultationRepository, ConsultationRepository>();
            services.AddTransient<IExamRepository, ExamRepository>();
            services.AddTransient<IImageRepository, ImageRepository>();
            services.AddTransient<IMedicalRecordRepository, MedicalRecordRepository>();
            services.AddTransient<IMenuRepository, MenuRepository>();
            services.AddTransient<IPermissionRepository, PermissionRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IRecordSurgeryRepository, RecordSurgeryRepository>();
            services.AddTransient<IScheduleRepository, ScheduleRepository>();
            services.AddTransient<ISpecialtyRepository, SpecialtyRepository>();
            services.AddTransient<IStaffRepository, StaffRepository>();
            services.AddTransient<IStateRepository, StateRepository>();
            services.AddTransient<ISurgeryRepository, SurgeryRepository>();
            services.AddTransient<ISurgeryStaffRepository, SurgeryStaffRepository>();
            services.AddTransient<ITokenRepository, TokenRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IEmailSender, SmtpEmailSender>();


            return services;
        }
    }
}