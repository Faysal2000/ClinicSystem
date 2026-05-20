using ClinicSystem.Enums;

namespace ClinicSystem.Models;
    public class Invoice
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public decimal TotalAmount { get; set; }
        public InvoiceStatus Status { get; set; } = InvoiceStatus.Pending;
        public DateTime IssuedDate { get; set; } = DateTime.UtcNow;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set;} = DateTime.UtcNow;


        public Appointment Appointment { get; set; } = null!;
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();


}

