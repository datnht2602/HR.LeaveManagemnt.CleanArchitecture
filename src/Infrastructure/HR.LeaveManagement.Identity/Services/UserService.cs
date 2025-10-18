using System.Security.Claims;
using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Models.Identity;
using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace HR.LeaveManagement.Identity.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
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

    public string UserId { get => _httpContextAccessor.HttpContext?.User?.FindFirstValue("uid"); }
}