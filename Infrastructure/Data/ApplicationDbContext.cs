using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSets for your entities
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Consultation> Consultations { get; set; }
        public DbSet<ConsultationExam> ConsultationExams { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RecordSurgery> RecordSurgeries { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Surgery> Surgeries { get; set; }
        public DbSet<SurgeryStaff> SurgeryStaffs { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<User> Users { get; set; }

        // Intermediate tables (many-to-many relationships)
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<RoleMenu> RoleMenus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ValueObject configurations
            modelBuilder.Owned<DateRange>(); // Using 'Owned' for Value Objects
            modelBuilder.Owned<Domain.ValueObjects.Email>();
            modelBuilder.Owned<PhoneNumber>();

            // Configure many-to-many relationship between roles and menus
            modelBuilder.Entity<RoleMenu>()
                .HasKey(rm => rm.Id);  // Primary key for RoleMenu
            modelBuilder.Entity<RoleMenu>()
                .HasOne(rm => rm.Role)
                .WithMany(r => r.RoleMenus)
                .HasForeignKey(rm => rm.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<RoleMenu>()
                .HasOne(rm => rm.Menu)
                .WithMany(m => m.RoleMenus)
                .HasForeignKey(rm => rm.MenuId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure many-to-many relationship between roles and permissions
            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => rp.Id);  // Primary key for RolePermission
            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure composite key for ConsultationExam
            modelBuilder.Entity<ConsultationExam>()
                .HasKey(ce => new { ce.ConsultationId, ce.ExamId });

            modelBuilder.Entity<ConsultationExam>()
                .HasOne(ce => ce.Consultation)
                .WithMany(c => c.ConsultationExams)
                .HasForeignKey(ce => ce.ConsultationId);

            modelBuilder.Entity<ConsultationExam>()
                .HasOne(ce => ce.Exam)
                .WithMany(e => e.ConsultationExams)
                .HasForeignKey(ce => ce.ExamId);

            // Configure composite key for RecordSurgery
            modelBuilder.Entity<RecordSurgery>()
                .HasKey(rs => new { rs.RecordId, rs.SurgeryId });

            modelBuilder.Entity<RecordSurgery>()
                .HasOne(rs => rs.MedicalRecord)
                .WithMany(mr => mr.RecordSurgeries)
                .HasForeignKey(rs => rs.RecordId)
                .OnDelete(DeleteBehavior.Restrict);  // Prevent multiple cascade paths

            modelBuilder.Entity<RecordSurgery>()
                .HasOne(rs => rs.Surgery)
                .WithMany(s => s.RecordSurgeries)
                .HasForeignKey(rs => rs.SurgeryId);

            modelBuilder.Entity<RecordSurgery>()
                .HasOne(rs => rs.Consultation)
                .WithMany(c => c.RecordSurgeries)
                .HasForeignKey(rs => rs.ConsultationId);

            // Configure composite key for SurgeryStaff
            modelBuilder.Entity<SurgeryStaff>()
                .HasKey(ss => new { ss.SurgeryId, ss.StaffId });

            modelBuilder.Entity<SurgeryStaff>()
                .HasOne(ss => ss.Surgery)
                .WithMany(s => s.SurgeryStaffs)
                .HasForeignKey(ss => ss.SurgeryId)
                .OnDelete(DeleteBehavior.Restrict);  // Prevent multiple cascade paths

            modelBuilder.Entity<SurgeryStaff>()
                .HasOne(ss => ss.Staff)
                .WithMany(st => st.SurgeryStaffs)
                .HasForeignKey(ss => ss.StaffId)
                .OnDelete(DeleteBehavior.Restrict);  // Prevent multiple cascade paths

            // Configure one-to-many relationship between Specialty and Staff
            modelBuilder.Entity<Specialty>()
                .HasMany(s => s.Staffs)
                .WithOne(st => st.Specialty)
                .HasForeignKey(st => st.SpecialtyId);

            // Configure MedicalRecord relationships
            modelBuilder.Entity<MedicalRecord>()
                .HasOne(mr => mr.Patient)
                .WithMany()
                .HasForeignKey(mr => mr.PatientId)
                .OnDelete(DeleteBehavior.Restrict);  // Prevent cascade cycle

            modelBuilder.Entity<MedicalRecord>()
                .HasOne(mr => mr.Staff)
                .WithMany()
                .HasForeignKey(mr => mr.StaffId)
                .OnDelete(DeleteBehavior.Restrict);  // Prevent cascade cycle

            modelBuilder.Entity<MedicalRecord>()
                .HasOne(mr => mr.State)
                .WithMany()
                .HasForeignKey(mr => mr.StateId)
                .OnDelete(DeleteBehavior.Restrict);  // Prevent cascade cycle

            // Configure Appointment relationships
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany()
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);  // Prevent multiple cascade paths

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Staff)
                .WithMany()
                .HasForeignKey(a => a.StaffId)
                .OnDelete(DeleteBehavior.Restrict);  // Prevent multiple cascade paths

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Specialty)
                .WithMany()
                .HasForeignKey(a => a.SpecialtyId)
                .OnDelete(DeleteBehavior.Restrict);  // Prevent multiple cascade paths

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Schedule)
                .WithMany()
                .HasForeignKey(a => a.ScheduleId)
                .OnDelete(DeleteBehavior.Restrict);  // Prevent multiple cascade paths

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.State)
                .WithMany()
                .HasForeignKey(a => a.StateId)
                .OnDelete(DeleteBehavior.Restrict);  // Prevent multiple cascade paths
        }
    }
}