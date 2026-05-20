namespace ClinicSystem.Models
{
    public class DoctorSpecialty
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int SpecialtyId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt {  get; set; } = DateTime.UtcNow;



        public Doctor Doctor { get; set; }
        public Specialty Specialty { get; set; }
    }
}
