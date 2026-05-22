using ClinicSystem.Models;


namespace ClinicSystem.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        //IGenericRepository<Role> Roles { get; }
        //IGenericRepository<User> Users { get; }
        IGenericRepository<Doctor> Doctors { get; } // Return type is IGenericRepository<Doctor> to provide access to the generic repository for the Doctor entity, allowing for CRUD operations and querying of doctors in the database.
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
