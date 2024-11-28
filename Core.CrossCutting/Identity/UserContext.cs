using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Core.CrossCutting.Identity;
public interface IUserContext
{
    string UserId { get; }
    string UserName { get; }
    IEnumerable<string> Roles { get; }
    bool IsInRole(string role);
}

public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    public string? UserName => _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;

    // Recupera todos los roles desde los claims
    public IEnumerable<string> Roles =>
        _httpContextAccessor.HttpContext?.User?.Claims
            .Where(c => c.Type == ClaimTypes.Role)
            .Select(c => c.Value)
        ?? Enumerable.Empty<string>();

    // Verifica si el usuario tiene un rol específico
    public bool IsInRole(string role) => Roles.Contains(role);
}