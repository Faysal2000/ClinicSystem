
using ClinicSystem.Enums;

namespace ClinicSystem.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public int DoctorServiceId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
        public string? Notes { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


        public DoctorService DoctorService { get; set; } = null!;
        //public ICollection<Service> Services { get; set; }
        public Doctor Doctor { get; set; } = null!;
        public Patient Patient { get; set; } = null!;
        public Invoice? Invoice { get; set; }=null!;    



    }
}
