namespace ClinicSystem.Models
{
    public class Patient
    {

        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public enum Gender { Male, Female }
        public string Phone { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;


        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();   


    }

}
