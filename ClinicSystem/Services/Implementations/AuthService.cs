using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ClinicSystem.DTOs.Auth;
using ClinicSystem.Models;
using ClinicSystem.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace ClinicSystem.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly IConfiguration _config;

    public AuthService(
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        IConfiguration config)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _config = config;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        // Check if email already exists
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
            throw new InvalidOperationException("Email is already registered.");

        // Check if role exists
        var roleExists = await _roleManager.RoleExistsAsync(request.Role);
        if (!roleExists)
            throw new InvalidOperationException($"Role '{request.Role}' does not exist.");

        // Create new user
        var user = new User
        {
            FullName = request.FullName,
            Email = request.Email,
            UserName = request.Email,
            PhoneNumber = request.Phone,
            Address = request.Address
        };

        // Identity handles password hashing
        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"User creation failed: {errors}");
        }

        // Assign role
        await _userManager.AddToRoleAsync(user, request.Role);

        return await GenerateTokenAsync(user);
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        // Find user by email
        var user = await _userManager.FindByEmailAsync(request.Email)
            ?? throw new UnauthorizedAccessException("Invalid email or password.");

        // Check if account is locked
        if (await _userManager.IsLockedOutAsync(user))
            throw new UnauthorizedAccessException("Account is locked. Please try again later.");

        // Check password
        var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!isPasswordValid)
        {
            // Increment failed attempts
            await _userManager.AccessFailedAsync(user);
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        // Reset failed attempts on success
        await _userManager.ResetAccessFailedCountAsync(user);

        return await GenerateTokenAsync(user);
    }

    private async Task<AuthResponse> GenerateTokenAsync(User user)
    {
        var jwtKey = _config["Jwt:Key"]
            ?? throw new InvalidOperationException("JWT key not configured.");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiry = DateTime.UtcNow.AddHours(
            double.Parse(_config["Jwt:ExpiresInHours"] ?? "24"));

        // Get user roles
        var roles = await _userManager.GetRolesAsync(user);
        var role = roles.FirstOrDefault() ?? string.Empty;

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(ClaimTypes.Name, user.FullName),
            new Claim(ClaimTypes.Role, role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: expiry,
            signingCredentials: creds);

        return new AuthResponse
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            FullName = user.FullName,
            Email = user.Email!,
            Role = role,
            ExpiresAt = expiry
        };
    }
}