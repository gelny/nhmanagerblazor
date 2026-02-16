using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace NHManager.Blazor.Auth;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly ProtectedSessionStorage _sessionStorage;
    private readonly ProtectedLocalStorage _localStorage;
    private readonly ILogger<CustomAuthStateProvider> _logger;
    private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());

    public CustomAuthStateProvider(ProtectedSessionStorage sessionStorage, ProtectedLocalStorage localStorage, ILogger<CustomAuthStateProvider> logger)
    {
        _sessionStorage = sessionStorage;
        _localStorage = localStorage;
        _logger = logger;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            _logger.LogDebug("GetAuthenticationStateAsync: Checking SessionStorage...");
            var userSessionResult = await _sessionStorage.GetAsync<UserSession>("UserSession");
            var userSession = userSessionResult.Success ? userSessionResult.Value : null;

            if (userSession == null)
            {
                _logger.LogDebug("GetAuthenticationStateAsync: Session empty, checking LocalStorage...");
                try 
                {
                    var userSessionLocalResult = await _localStorage.GetAsync<UserSession>("UserSession");
                    userSession = userSessionLocalResult.Success ? userSessionLocalResult.Value : null;
                    _logger.LogDebug("GetAuthenticationStateAsync: LocalStorage result: {Result}", userSession != null ? "Found" : "Not Found");
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "GetAuthenticationStateAsync: LocalStorage error");
                }
                
                if (userSession != null)
                {
                    _logger.LogInformation("GetAuthenticationStateAsync: Restored user {Username} with role {Role}", userSession.Username, userSession.Role);
                    await _sessionStorage.SetAsync("UserSession", userSession);
                }
            }
            else
            {
                _logger.LogDebug("GetAuthenticationStateAsync: Found in SessionStorage");
            }

            if (userSession == null)
                return new AuthenticationState(_anonymous);

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, userSession.Username),
                new(ClaimTypes.Role, userSession.Role),
                new("UserId", userSession.UserId.ToString())
            };

            if (userSession.WorkerId.HasValue)
            {
                claims.Add(new Claim("WorkerId", userSession.WorkerId.Value.ToString()));
            }

            if (!string.IsNullOrEmpty(userSession.WorkerFullName))
            {
                claims.Add(new Claim("WorkerFullName", userSession.WorkerFullName));
            }

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "CustomAuth"));
            return new AuthenticationState(claimsPrincipal);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetAuthenticationStateAsync: Error retrieving auth state");
            return new AuthenticationState(_anonymous);
        }
    }

    public async Task UpdateAuthenticationState(UserSession? userSession, bool rememberMe = false)
    {
        ClaimsPrincipal claimsPrincipal;

        if (userSession != null)
        {
            await _sessionStorage.SetAsync("UserSession", userSession);
            
            if (rememberMe)
            {
                await _localStorage.SetAsync("UserSession", userSession);
            }
            else
            {
                await _localStorage.DeleteAsync("UserSession");
            }
            
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, userSession.Username),
                new(ClaimTypes.Role, userSession.Role),
                new("UserId", userSession.UserId.ToString())
            };

            if (userSession.WorkerId.HasValue)
            {
                claims.Add(new Claim("WorkerId", userSession.WorkerId.Value.ToString()));
            }

            if (!string.IsNullOrEmpty(userSession.WorkerFullName))
            {
                claims.Add(new Claim("WorkerFullName", userSession.WorkerFullName));
            }

            claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "CustomAuth"));
        }
        else
        {
            await _sessionStorage.DeleteAsync("UserSession");
            await _localStorage.DeleteAsync("UserSession");
            claimsPrincipal = _anonymous;
        }

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }

    public async Task<string?> GetCurrentUsername()
    {
        var authState = await GetAuthenticationStateAsync();
        return authState.User.Identity?.Name;
    }

    public async Task<int?> GetCurrentWorkerId()
    {
        var authState = await GetAuthenticationStateAsync();
        var claim = authState.User.FindFirst("WorkerId");
        if (claim != null && int.TryParse(claim.Value, out var workerId))
        {
            return workerId;
        }
        return null;
    }
}
