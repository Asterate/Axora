using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using App.DAL.EF;
using App.Domain.Entities;
using App.Domain.Identity;
using WebApp.ViewModels;
using App.DTO.v1.Identity;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class RegisterController : Controller
{
    private readonly AppDbContext _context;
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IConfiguration _configuration;

    public RegisterController(
        AppDbContext context,
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        IConfiguration configuration)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var model = new RegisterViewModel();
        
        // Load institutes and institute types directly from EF
        var institutes = await _context.Institutes
            .Where(i => i.Active)
            .OrderBy(i => i.InstituteName)
            .Select(i => new RegisterViewModel.LookupItem 
            { 
                Id = i.Id, 
                Name = i.InstituteName 
            })
            .ToListAsync();
        
        model.Institutes = institutes;

        var instituteTypes = await _context.InstituteTypes
            .ToListAsync();
        model.InstituteTypes = instituteTypes.Select(t => new RegisterViewModel.LookupItem 
        { 
            Id = t.Id, 
            Name = t.Name?.Translate() ?? "???" 
        }).OrderBy(x => x.Name).ToList();
        
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            // Reload dropdown data
            await LoadDropdowns(model);
            return View(model);
        }

        try
        {
            // Create the user using Identity
            var appUser = new AppUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(appUser, model.Password);
            
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                await LoadDropdowns(model);
                return View(model);
            }
            

            // Sign in the user and generate tokens
            await _signInManager.SignInAsync(appUser, isPersistent: false);
            
            // Generate JWT token manually (similar to API approach)
            var tokenResponse = await GenerateJwtTokenAsync(appUser);
            
            if (tokenResponse != null)
            {
                // Store tokens in cookies for auto-login
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddDays(7)
                };

                Response.Cookies.Append("JWT", tokenResponse.JWT, cookieOptions);
                Response.Cookies.Append("RefreshToken", tokenResponse.RefreshToken, cookieOptions);
            }

            // Redirect to HomeDashboard after successful registration
            return RedirectToAction("Index", "InstituteChoice");



        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "An error occurred: " + ex.Message);
        }

        // Reload dropdown data on error
        await LoadDropdowns(model);
        return View(model);
    }

    private async Task LoadDropdowns(RegisterViewModel model)
    {
        var institutes = await _context.Institutes
            .Where(i => i.Active)
            .OrderBy(i => i.InstituteName)
            .Select(i => new RegisterViewModel.LookupItem 
            { 
                Id = i.Id, 
                Name = i.InstituteName 
            })
            .ToListAsync();
        
        model.Institutes = institutes;

        var instituteTypes = await _context.InstituteTypes
            .ToListAsync();
        model.InstituteTypes = instituteTypes.Select(t => new RegisterViewModel.LookupItem 
        { 
            Id = t.Id, 
            Name = t.Name?.Translate() ?? "???" 
        }).OrderBy(x => x.Name).ToList();
    }

    private async Task<JWTResponse?> GenerateJwtTokenAsync(AppUser user)
    {
        try
        {
            // Get JWT settings from configuration
            var key = _configuration["JWT:Key"];
            var issuer = _configuration["JWT:Issuer"];
            var audience = _configuration["JWT:Audience"];
            var expiresInSeconds = _configuration.GetValue<int>("JWT:ExpiresInSeconds", 3600);
            var refreshTokenExpiresInSeconds = _configuration.GetValue<int>("JWT:RefreshTokenExpiresInSeconds", 86400);

            if (string.IsNullOrEmpty(key))
            {
                return null;
            }

            // Get user roles
            var roles = await _userManager.GetRolesAsync(user);
            
            // Create claims
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
                new Claim(ClaimTypes.Name, user.UserName ?? ""),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Create signing key
            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Create JWT token
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddSeconds(expiresInSeconds),
                signingCredentials: credentials
            );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            // Generate refresh token
            var refreshToken = Guid.NewGuid().ToString();
            
            // Store refresh token in database
            var refreshTokenEntity = new AppRefreshToken
            {
                Id = Guid.NewGuid(),
                RefreshToken = refreshToken,
                UserId = user.Id,
                Expiration = DateTime.UtcNow.AddSeconds(refreshTokenExpiresInSeconds)
            };
            
            _context.RefreshTokens.Add(refreshTokenEntity);
            await _context.SaveChangesAsync();

            return new JWTResponse
            {
                JWT = jwtToken,
                RefreshToken = refreshToken
            };
        }
        catch
        {
            return null;
        }
    }
}