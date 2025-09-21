using System.Reflection;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Persistence.DatabaseContext;
using HR.LeaveManagement.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HR.LeaveManagement.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection serviceCollection, IConfiguration configuration, Assembly assembly)
    {
        serviceCollection.AddDbContext<HrDatabaseContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("HrDatabaseConnection"));
        });
        
        var repositories = assembly.GetTypes().Where(ass =>
            !ass.IsAbstract && ass.IsClass && typeof(IRepository).IsAssignableFrom(ass));

        foreach (var repository in repositories)
        {
            var repositoryInterface = repository.GetInterfaces().SingleOrDefault(i =>
                !i.IsGenericType && typeof(IRepository) != i &&
                typeof(IRepository).IsAssignableFrom(i));

            if (repositoryInterface != null)
            {
                serviceCollection.AddScoped(repositoryInterface, repository);
            }
        }
        
        return serviceCollection;
    }
}