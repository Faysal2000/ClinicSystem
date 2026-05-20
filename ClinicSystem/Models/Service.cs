namespace ClinicSystem.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int DurationInMinutes { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;

        public ICollection<DoctorService> DoctorServices { get; set; } = new List<DoctorService>();


    }
}
