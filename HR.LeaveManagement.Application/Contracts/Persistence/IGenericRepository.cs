using HR.LeaveManagement.Domain.Common;

namespace HR.LeaveManagement.Application.Contracts.Persistence;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T> GetByIdAsync(int id);
    
    Task<List<T>> ListAllAsync();
    
    Task<T> AddAsync(T entity);
    
    Task<T> UpdateAsync(T entity);
    
    Task<bool> DeleteAsync(T entity);
}