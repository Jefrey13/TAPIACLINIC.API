    using Domain.ValueObjects;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace Domain.Entities;
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public User Patient { get; set; }

        [Required]
        public int? StaffId { get; set; }
        [ForeignKey("StaffId")]
        public Staff? Staff { get; set; }

        [Required]
        public int SpecialtyId { get; set; }
        [ForeignKey("SpecialtyId")]
        public Specialty Specialty { get; set; }

        [Required]
        public int ScheduleId { get; set; }
        [ForeignKey("ScheduleId")]
        public Schedule Schedule { get; set; }

        [Required]
        public int StateId { get; set; }
        [ForeignKey("StateId")]
        public State State { get; set; }

        [MaxLength(255)]
        public string Reason { get; set; }

        [MaxLength(255)]
        public string? ChangeReason { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

            public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }