using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace HR.LeaveManagement.Persistence.IntegrationTests;

public class HrDatabaseContextTests
{
    private readonly HrDatabaseContext _hrDatabaseContext;

    public HrDatabaseContextTests()
    {
        var dbOptions = new DbContextOptionsBuilder<HrDatabaseContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        
        _hrDatabaseContext = new HrDatabaseContext(dbOptions);
    }
    
    [Fact]
    public async Task Save_SetDateCreateAndDateModified()
    {
        var entity = new LeaveType { Name = "Test Leave Type" };
        await _hrDatabaseContext.LeaveTypes.AddAsync(entity);
        await _hrDatabaseContext.SaveChangesAsync();

        entity.DateCreated.ShouldNotBeNull();
        entity.DateModified.ShouldNotBeNull();
    }
    
}