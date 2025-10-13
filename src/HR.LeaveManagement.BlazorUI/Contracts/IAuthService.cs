namespace HR.LeaveManagement.BlazorUI.Contracts;

public interface IAuthService
{
    Task<bool> AuthenticatedAsync(string email, string password);
    
    Task LogoutAsync();
    
    Task<bool> RegisterAsync(string firstName, string lastName, string userName, string email, string password);
}