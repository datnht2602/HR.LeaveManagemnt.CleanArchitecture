using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Models.Identity;
using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace HR.LeaveManagement.Identity.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    public async Task<List<Employee>> GetEmployees()
    {
        var employess = await _userManager.GetUsersInRoleAsync("Employee");
        return employess.Select(q => new Employee()
        {
            Id = q.Id,
            Email = q.Email,
            FirstName = q.FirstName,
            LastName = q.LastName,
        }).ToList();
    }

    public async Task<Employee> GetEmployeeById(string id)
    {
        var employee = await _userManager.FindByIdAsync(id);
        return new Employee()
        {
            Email = employee.Email,
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
        };
    }
}