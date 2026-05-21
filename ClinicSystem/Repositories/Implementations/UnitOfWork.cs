using ClinicSystem.Data;
using ClinicSystem.Models;
using ClinicSystem.Repositories.Interfaces;


namespace ClinicSystem.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly AppDbContext _context;

        private IGenericRepository<Role>? _roles;
        private IGenericRepository<User>? _users;
        private IGenericRepository<Doctor>? _doctors;
        private IGenericRepository<Patient>? _patients;
        private IGenericRepository<Specialty>? _specialties;
        private IGenericRepository<DoctorSpecialty>? _doctorSpecialties;
        private IGenericRepository<Service>? _services;
        private IGenericRepository<DoctorService>? _doctorServices;
        private IGenericRepository<DoctorSchedule>? _doctorSchedules;
        private IGenericRepository<Appointment>? _appointments;
        private IGenericRepository<Invoice>? _invoices;
        private IGenericRepository<Payment>? _payments;


        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        // Lazy initialization of repositories to ensure they are only created when needed,
        // optimizing resource usage and improving performance by avoiding unnecessary
        // instantiation of repositories that may not be used during the lifetime of the UnitOfWork.
        public IGenericRepository<Role> Roles => 
        _roles ??= new GenericRepository<Role>(_context);
        public IGenericRepository<User> Users =>
           _users ??= new GenericRepository<User>(_context);

        public IGenericRepository<Doctor> Doctors =>
            _doctors ??= new GenericRepository<Doctor>(_context);

        public IGenericRepository<Patient> Patients =>
            _patients ??= new GenericRepository<Patient>(_context);

        public IGenericRepository<Specialty> Specialties =>
            _specialties ??= new GenericRepository<Specialty>(_context);

        public IGenericRepository<DoctorSpecialty> DoctorSpecialties =>
            _doctorSpecialties ??= new GenericRepository<DoctorSpecialty>(_context);

        public IGenericRepository<Service> Services =>
            _services ??= new GenericRepository<Service>(_context);

        public IGenericRepository<DoctorService> DoctorServices =>
            _doctorServices ??= new GenericRepository<DoctorService>(_context);

        public IGenericRepository<DoctorSchedule> DoctorSchedules =>
            _doctorSchedules ??= new GenericRepository<DoctorSchedule>(_context);

        public IGenericRepository<Appointment> Appointments =>
            _appointments ??= new GenericRepository<Appointment>(_context);

        public IGenericRepository<Invoice> Invoices =>
            _invoices ??= new GenericRepository<Invoice>(_context);

        public IGenericRepository<Payment> Payments =>
            _payments ??= new GenericRepository<Payment>(_context);

        public async Task<int> SaveChangesAsync() =>
            await _context.SaveChangesAsync();

        public void Dispose() =>
            _context.Dispose();

    }
}
