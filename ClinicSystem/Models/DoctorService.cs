namespace ClinicSystem.Models
{
    public class DoctorService
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int ServiceId { get; set; }
        public int DurationInMinutes { get; set; }
        public float Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;



        public Doctor Doctors { get; set; } = null;
        public Service Services { get; set; } = null;
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();



    }
}
