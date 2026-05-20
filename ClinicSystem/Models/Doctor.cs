namespace ClinicSystem.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public int userId { get; set; }
        public int ExperienceYears { get; set; }
        public DateTime CreatedAt {  get; set; } = DateTime.UtcNow;
        public DateTime UpdateAt {  get; set; } = DateTime.UtcNow;



        public ICollection <DoctorSpecialty> DoctorSpecialties { get; set; } = new List<DoctorSpecialty>();

        public ICollection <DoctorService> DoctorServices { get; set; } = new List<DoctorService>();

        public ICollection <DoctorSchedule> DoctorSchedules { get; set; } = new List<DoctorSchedule>();

        public ICollection <Appointment> Appointments { get; set; } = new List<Appointment>();






    }
}
