using System.Linq.Expressions; // Importing the System.Linq.Expressions namespace to use the Expression<Func<T, bool>> type for the FindAsync method, which allows for flexible querying based on a predicate.

namespace ClinicSystem.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class // Constrain T to be a reference type (class) to ensure that it can be used with Entity Framework Core, which typically works with classes representing database entities.
    {
       
        Task<T?> GetByIdAsync(int id); // Return type is nullable to handle cases where the entity might not be found
        Task<IEnumerable<T>> GetAllAsync(); // Return type is IEnumerable to allow for multiple entities to be returned

        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);// Return type is IEnumerable to allow for multiple entities to be returned based on the provided predicate, which allows for flexible querying of the database.
        Task AddAsync(T entity); // No return type needed for AddAsync as it simply adds the entity to the database
        Task UpdateAsync(T entity); // No return type needed for UpdateAsync as it simply updates the entity in the database
        void Remove(T entity); // No return type needed for Remove as it simply removes the entity from the database
        Task DeleteAsync(int id); // No return type needed for DeleteAsync as it simply deletes the entity from the database based on the provided id
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate); // Return type is bool to indicate whether any entities exist that match the provided predicate, allowing for checks before performing operations like updates or deletions.

    }
}

    

