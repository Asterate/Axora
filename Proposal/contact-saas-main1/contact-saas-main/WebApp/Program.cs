using System.Text;
using App.Modules.Audit.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using WebApp.Helpers;
using WebApp.Setup;
using App.Modules.Identity;
using App.Modules.Identity.Infrastructure;
using App.Modules.Lab.Infrastructure;
using App.Modules.Project.Infrastructure;
using App.Shared.Domain;
using Microsoft.AspNetCore.DataProtection;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Service registration
builder.Services.AddAppIdentity();

#pragma warning disable CS0618
NpgsqlConnection.GlobalTypeMapper.EnableDynamicJson();
#pragma warning restore CS0618
LangStr.DefaultCulture = builder.Configuration.GetValue<string>("LangStrDefaultCulture") ?? "en";

// Add authentication: Cookie for MVC, JWT for API
builder.Services.AddAuthentication(options =>
{
    // Default to cookie auth (for MVC views)
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.Cookie.Name = "AppAuth";
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    options.LoginPath = "/Identity/Account/Login?ReturnUrl=%2F";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
})
.AddJwtBearer("Bearer", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!))
    };
});

// Configure the Default HTTP client for internal API calls
var baseUrl = builder.Configuration["AppBaseUrl"] ?? builder.Configuration["Urls"] ?? "http://localhost:5000";
builder.Services.AddHttpClient("Default")
    .ConfigureHttpClient(client => client.BaseAddress = new Uri(baseUrl));
builder.Services.AddSingleton<AppNameService>();
builder.Services.AddAppControllers();
builder.Services.AddForwardedHeaders();
builder.Services.AddAppCors();
builder.Services.AddAppApiVersioning();
builder.Services.AddAppSwagger();
builder.Services.AddAppLocalization(builder.Configuration);

//Modular monolith
builder.Services.AddModuleDatabase<AuditDbContext>(builder.Configuration, builder.Environment);
builder.Services.AddModuleDatabase<IdentityModuleDbContext>(builder.Configuration, builder.Environment);
builder.Services.AddIdentityDatabase(builder.Configuration, builder.Environment);
builder.Services.AddModuleDatabase<LabDbContext>(builder.Configuration, builder.Environment);
builder.Services.AddLabDatabase(builder.Configuration, builder.Environment);
builder.Services.AddModuleDatabase<ResearchDbContext>(builder.Configuration, builder.Environment);
builder.Services.AddResearchDatabase(builder.Configuration, builder.Environment);

builder.Services.AddAuditModule();
builder.Services.AddIdentityModule();
builder.Services.AddLabModule();
builder.Services.AddProjectModule();

builder.Services.AddDataProtection()
     .PersistKeysToDbContext<IdentityModuleDbContext>();

// Build and configure pipeline
var app = builder.Build();

app.SetupAppData();
app.UseMiddleware<ErrorLoggingMiddleware>();
app.UseAppMiddleware();
app.UseAppSwagger();
app.MapAppEndpoints();


app.Run();


// this is needed for unit testing
// ReSharper disable once ClassNeverInstantiated.Global
public partial class Program
{
}
