
using ClinicSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {


        }
        public DbSet<Doctor> Doctors => Set<Doctor>();
        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<Specialty> Specialties => Set<Specialty>();
        public DbSet<DoctorSpecialty> DoctorSpecialties => Set<DoctorSpecialty>();
        public DbSet<Service> Services => Set<Service>();
        public DbSet<DoctorService> DoctorServices => Set<DoctorService>();
        public DbSet<DoctorSchedule> DoctorSchedules => Set<DoctorSchedule>();
        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<Invoice> Invoices => Set<Invoice>();
        public DbSet<Payment> Payments => Set<Payment>();



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        

            // Doctor
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


            // DoctorSpecialty
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


            // Service
            modelBuilder.Entity<Service>(e =>
            {
                e.HasKey(s => s.Id);
                e.Property(s => s.Name).IsRequired().HasMaxLength(150);
                e.Property(s => s.Description).HasMaxLength(500);
            });


            // DoctorService
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

            // DoctorSchedule
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


            // Appointment
            modelBuilder.Entity<Appointment>(e =>
            {
                e.HasKey(a => a.Id);
                e.Property(a => a.Notes).HasMaxLength(500);
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


            // Invoice
            modelBuilder.Entity<Invoice>(e =>
            {
                e.HasKey(i => i.Id);
                e.Property(i => i.TotalAmount).HasColumnType("decimal(10,2)");
                e.HasOne(i => i.Appointment)
                 .WithOne(a => a.Invoice)
                 .HasForeignKey<Invoice>(i => i.AppointmentId)
                 .OnDelete(DeleteBehavior.Cascade);
            });


            // Payment
            modelBuilder.Entity<Payment>(e =>
            {
                e.HasKey(p => p.Id);
                e.Property(p => p.Amount).HasColumnType("decimal(10,2)");
                e.HasOne(p => p.Invoice)
                 .WithMany(i => i.Payments)
                 .HasForeignKey(p => p.InvoiceId)
                 .OnDelete(DeleteBehavior.Cascade);
            });


            // Seed Roles
            modelBuilder.Entity<Role>().HasData(
                //new Role { Id = 1, Name = "Admin", NormalizedName = "ADMIN", CreatedAt = new DateTime(2026, 5, 11), UpdatedAt = new DateTime(2024, 1, 1) },
                //new Role { Id = 2, Name = "Doctor", NormalizedName = "DOCTOR", CreatedAt = new DateTime(2026, 5, 20), UpdatedAt = new DateTime(2024, 1, 1) },
                //new Role { Id = 3, Name = "Receptionist", NormalizedName = "RECEPTIONIST", CreatedAt = new DateTime(2026, 5, 19), UpdatedAt = new DateTime(2024, 1, 1) }
            );
        }
    }
}
