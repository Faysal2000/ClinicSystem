
using ClinicSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {


        }

        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Doctor> Doctors { get; set; } = null!;

        public DbSet<DoctorSchedule> DoctorSchedules { get; set; } = null!;
        public DbSet<DoctorSpecialty> DoctorSpecialty { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Service> Services { get; set; } = null!;
        public DbSet<Specialty> Specialties { get; set; } = null!;

        public DbSet<Patient> Patients { get; set; } = null!;
        public DbSet<DoctorService> DoctorServices { get; set; } = null!;
        public DbSet<Appointment> Appointments { get; set; } = null!;
        public DbSet<Invoice> Invoices { get; set; } = null!;
        public DbSet<Payment> Payments { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Role
            modelBuilder.Entity<Role>(e =>
            {
                e.HasKey(r => r.Id);
                e.Property(r => r.Name).IsRequired().HasMaxLength(50);

            });

            //User
            modelBuilder.Entity<User>(e =>
            {

                e.HasKey(u => u.Id);
                e.Property(u => u.FullName).IsRequired().HasMaxLength(70);
                e.Property(u => u.Email).IsRequired().HasMaxLength(100);
                e.HasIndex(u => u.Email).IsUnique();
                e.Property(u => u.Password).IsRequired();
                e.Property(u => u.Phone).HasMaxLength(20);
                e.Property(u => u.Address).HasMaxLength(200);
                e.HasOne(u => u.Role)
                    .WithMany(r => r.Users)
                    .HasForeignKey(u => u.RoleId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            //Doctor
            modelBuilder.Entity<Doctor>(e =>
            {
                e.HasKey(d => d.Id);
                e.HasOne(d => d.User)
                 .WithOne(u => u.Doctor)
                 .HasForeignKey<Doctor>(d => d.UserId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            //Patient
            modelBuilder.Entity<Patient>(e =>
            {
                e.HasKey(p => p.Id);
                e.Property(p => p.FullName).IsRequired().HasMaxLength(50);
                e.Property(p => p.Phone).HasMaxLength(20);

            });

            //Specialty
            modelBuilder.Entity<Specialty>(e =>
            {
                e.HasKey(s => s.Id);
                e.Property(s => s.Name).IsRequired().HasMaxLength(100);
            });

            //DoctorSpecialty
            modelBuilder.Entity<DoctorSpecialty>(e =>
            {
                e.HasKey(ds => ds.Id);
                e.HasOne(ds => ds.Doctor)
                    .WithMany(d => d.DoctorSpecialties)
                    .HasForeignKey(ds => ds.DoctorId)
                    .OnDelete(DeleteBehavior.Cascade);
                e.HasOne(ds => ds.Specialty)
                    .WithMany(s => s.DoctorSpecialties)
                    .HasForeignKey(ds => ds.SpecialtyId)
                    .OnDelete(DeleteBehavior.Cascade);
            });


            //Service
            modelBuilder.Entity<Service>(e =>
            {
                e.HasKey(s => s.Id);
                e.Property(s => s.Name).IsRequired().HasMaxLength(100);
                e.Property(s => s.Description).HasMaxLength(300);
            });

            //DoctorService
            modelBuilder.Entity<DoctorService>(e =>
            {
                e.HasKey(ds => ds.Id);
                e.Property(ds => ds.Price).HasColumnType("decimal(10,2)");
                e.HasOne(ds => ds.Doctor)
                    .WithMany(d => d.DoctorServices)
                    .HasForeignKey(ds => ds.DoctorId)
                    .OnDelete(DeleteBehavior.Cascade);
                e.HasOne(ds => ds.Service)
                    .WithMany(s => s.DoctorServices)
                    .HasForeignKey(ds => ds.ServiceId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            //DoctorSchedule
            modelBuilder.Entity<DoctorSchedule>(e =>
            {
                e.HasKey(ds => ds.Id);
                e.Property(ds => ds.Exceptions).HasMaxLength(1000);
                e.Property(ds => ds.DoctorLeaves).HasMaxLength(1000);
                e.HasOne(ds => ds.Doctor)
                 .WithMany(d => d.DoctorSchedules)
                 .HasForeignKey(ds => ds.DoctorId)
                 .OnDelete(DeleteBehavior.Cascade);
            });


            //Appointment
            modelBuilder.Entity<Appointment>(e =>
            {
                e.HasKey(a => a.Id);
                e.Property(a => a.Notes).HasMaxLength(500);
                e.Property(a => a.AppointmentDate).IsRequired();
                e.HasOne(a => a.Doctor)
                    .WithMany(d => d.Appointments)
                    .HasForeignKey(a => a.DoctorId)
                    .OnDelete(DeleteBehavior.Restrict);
                e.HasOne(a => a.Patient)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(a => a.PatientId)
                    .OnDelete(DeleteBehavior.Restrict);
                e.HasOne(a => a.DoctorService)
                    .WithMany(ds => ds.Appointments)
                    .HasForeignKey(a => a.DoctorServiceId)
                    .OnDelete(DeleteBehavior.Restrict);
            });


            //Invoice
            modelBuilder.Entity<Invoice>(e =>
            {
                e.HasKey(i => i.Id);
                e.Property(i => i.TotalAmount).HasColumnType("decimal(10,2)");
                e.HasOne(i => i.Appointment)
                    .WithOne(a => a.Invoice)
                    .HasForeignKey<Invoice>(i => i.AppointmentId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            //Payment
            modelBuilder.Entity<Payment>(e =>
            {
                e.HasKey(p => p.Id);
                e.Property(p => p.Amount).HasColumnType("decimal(10,2)");
                e.Property(p => p.PaymentDate).IsRequired();
                e.HasOne(p => p.Invoice)
                    .WithMany(i => i.Payments)
                    .HasForeignKey(p => p.InvoiceId)
                    .OnDelete(DeleteBehavior.Cascade);
            });


            // Seed Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin", CreatedAt = new DateTime(2024, 1, 1), UpdatedAt = new DateTime(2024, 1, 1) },
                new Role { Id = 2, Name = "Doctor", CreatedAt = new DateTime(2024, 1, 1), UpdatedAt = new DateTime(2024, 1, 1) },
                new Role { Id = 3, Name = "Receptionist", CreatedAt = new DateTime(2024, 1, 1), UpdatedAt = new DateTime(2024, 1, 1) }
            );
        }
    }
}
