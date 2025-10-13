using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Identity;
using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HR.LeaveManagement.Identity.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JwtSettings _jwtSettings;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AuthService(UserManager<ApplicationUser> userManager,
        IOptions<JwtSettings> jwtSettings,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _jwtSettings = jwtSettings.Value;
        _signInManager = signInManager;
    }
    
    public async Task<AuthResponse> Login(AuthRequest authRequest)
    {
        var user = await _userManager.FindByEmailAsync(authRequest.Email);
        
        if (user == null)
        {
            throw new NotFoundException($"User with email '{authRequest.Email}' not found", authRequest.Email);
        }
        
        var result = await _signInManager.CheckPasswordSignInAsync(user, authRequest.Password, false);

        if (!result.Succeeded)
        {
            throw new BadRequestException($"Credentials for {authRequest.Email }are invalid");
        }
        
        JwtSecurityToken jwtSecurityToken = await GenerateToken(user);
        
        var authResponse = new AuthResponse
        {
            Id = user.Id,
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            Email = user.Email,
            UserName = user.UserName
        };
        
        return authResponse;
    }
    
    public async Task<RegistrationResponse> Register(RegistrationRequest registrationRequest)
    {
        var user = new ApplicationUser()
        {
            UserName = registrationRequest.UserName,
            Email = registrationRequest.Email,
            EmailConfirmed = true,
            FirstName = registrationRequest.FirstName,
            LastName = registrationRequest.LastName
        };
        
        var result = await _userManager.CreateAsync(user, registrationRequest.Password);
        
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "Employee");
            
            return new RegistrationResponse(){ UserId = user.Id};
        }
        else
        {
            throw new BadRequestException($"Registration failed {result.Errors}");
        }
    }

    private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);
        
        var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("uid", user.Id)
        }
        .Union(userClaims)
        .Union(roleClaims);

        var symmetricSecurityKeys = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        
        var signingCredentials = new SigningCredentials(symmetricSecurityKeys, SecurityAlgorithms.HmacSha256);
        
        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes),
            signingCredentials: signingCredentials
        );
        
        return jwtSecurityToken;
    }
}