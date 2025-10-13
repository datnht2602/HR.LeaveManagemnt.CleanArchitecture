using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace HR.LeaveManagement.BlazorUI.Provider;

public class ApiAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorageService;
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

    public ApiAuthenticationStateProvider(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
        _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
    }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var user = new ClaimsPrincipal(new ClaimsIdentity());
        var isTokenPresent = await _localStorageService.ContainKeyAsync("token");
        if (isTokenPresent)
        {
            return new AuthenticationState(user);
        }
        var token = await _localStorageService.GetItemAsync<string>("token");
        var tokenContent = _jwtSecurityTokenHandler.ReadJwtToken(token);

        if (tokenContent.ValidTo < DateTime.UtcNow)
        {
            await _localStorageService.RemoveItemAsync("token");
            return new AuthenticationState(user);       
        }
        
        var claims = await GetClaimsFromJwtToken();
        user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
        return new AuthenticationState(user);
    }

    public async Task LoggedIn()
    {
        var claims = await GetClaimsFromJwtToken();
        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
        var authState = new AuthenticationState(user);
        NotifyAuthenticationStateChanged(Task.FromResult(authState));
    }
    
    public async Task LoggedOut()
    {
        await _localStorageService.RemoveItemAsync("token");
        var user = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    private async Task<List<Claim>> GetClaimsFromJwtToken()
    {
        var token = await _localStorageService.GetItemAsync<string>("token");
        var tokenContent = _jwtSecurityTokenHandler.ReadJwtToken(token);
        var claims = tokenContent.Claims.ToList();
        claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));
        return claims;
    }
}