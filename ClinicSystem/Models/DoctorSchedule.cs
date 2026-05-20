namespace ClinicSystem.Models
{
    public class DoctorSchedule
    {

        public int Id { get; set; }
        public int DoctorId { get; set; }
        public DayOfWeek DayOfWeek { get; set; } // 0 = Sunday, 1 = Monday, ..., 6 = Saturday
        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public bool IsActive { get; set; } = true;
        
        public string? Exceptions { get; set; } // JSON string to store exceptions like holidays or special days off

        public string? DoctorLeaves { get; set; } // JSON string to store doctor's leaves or time off


        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set;} = DateTime.UtcNow;


        public Doctor Doctor { get; set; } = null!;

    }

}
