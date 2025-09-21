using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories;

public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
{
    public LeaveAllocationRepository(HrDatabaseContext context) : base(context)
    {
    }

    public Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
    {
        return _context.LeaveAllocations.AsNoTracking().FirstOrDefaultAsync(q => q.Id == id);
    }

    public Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails()
    {
        return _context.LeaveAllocations.Include(q => q.LeaveType).ToListAsync();
    }

    public Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId)
    {
        return _context.LeaveAllocations.Where(q => q.EmployeeId == userId)
            .Include(q => q.LeaveType)
            .ToListAsync();
    }

    public Task<bool> AllocationExists(int leaveTypeId, string userId, int period)
    {
        return _context.LeaveAllocations.AnyAsync(q => q.EmployeeId == userId
            && q.LeaveTypeId == leaveTypeId
            && q.Period == period);
    }

    public async Task AddAllocations(List<LeaveAllocation> leaveAllocations)
    {
        await _context.AddRangeAsync(leaveAllocations);
        await _context.SaveChangesAsync();
    }

    public Task<LeaveAllocation> GetUserAllocations(string userId, int leaveTypeId)
    {
        return _context.LeaveAllocations.FirstOrDefaultAsync(q => q.EmployeeId == userId && q.LeaveTypeId == leaveTypeId);
    }
}