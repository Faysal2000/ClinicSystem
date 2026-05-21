using ClinicSystem.Models;


namespace ClinicSystem.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        IGenericRepository<Role> Roles { get; }// Return type is IGenericRepository<Role> to provide access to the generic repository for the Role entity, allowing for CRUD operations and querying of roles in the database.
        IGenericRepository<User> Users { get; }
        IGenericRepository<Doctor> Doctors { get; }
        IGenericRepository<Patient> Patients { get; }
        IGenericRepository<Specialty> Specialties { get; }
        IGenericRepository<DoctorSpecialty> DoctorSpecialties { get; }
        IGenericRepository<Service> Services { get; }
        IGenericRepository<DoctorService> DoctorServices { get; }
        IGenericRepository<DoctorSchedule> DoctorSchedules { get; }
        IGenericRepository<Appointment> Appointments { get; }
        IGenericRepository<Invoice> Invoices { get; }
        IGenericRepository<Payment> Payments { get; }

        Task<int> SaveChangesAsync();

    }
}
