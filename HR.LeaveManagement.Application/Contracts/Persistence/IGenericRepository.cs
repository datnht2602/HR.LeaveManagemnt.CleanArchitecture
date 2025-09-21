using HR.LeaveManagement.Domain.Common;

namespace HR.LeaveManagement.Application.Contracts.Persistence;

public interface IRepository;

public interface IGenericRepository<T> : IRepository where T : BaseEntity
{
    Task<T> GetByIdAsync(int id);
    
    Task<IReadOnlyList<T>> ListAllAsync();
    
    Task AddAsync(T entity);
    
    Task UpdateAsync(T entity);
    
    Task DeleteAsync(T entity);
}