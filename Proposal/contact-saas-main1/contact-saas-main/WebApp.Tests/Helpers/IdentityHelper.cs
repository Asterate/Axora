using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using App.Helpers;
using App.Modules.Identity.Application.DTO;
using Xunit;

namespace WebApp.Tests.Helpers;

public static class IdentityHelper
{
    public static async Task<JwtResponse> SetupUserAsync(HttpClient httpClient , string firstName, string lastName, string password, string email)
    {
        var data = new Register()
        {
            Password = password,
            Email = email,
        };

        // Act
        var response = await httpClient.PostAsync(
            "/api/v1/account/register",
            new StringContent(
                System.Text.Json.JsonSerializer.Serialize(data, JsonHelpers.JsonSerializerOptionsCamelCase),
                Encoding.UTF8, "application/json")
        );

        var responseString = await response.Content.ReadAsStringAsync();
        
        // Assert
        response.EnsureSuccessStatusCode();
        
        var jwtResponse = System.Text.Json.JsonSerializer.Deserialize<JwtResponse>(responseString, JsonHelpers.JsonSerializerOptionsCamelCase);

        Assert.NotNull(jwtResponse);

        return jwtResponse;
    }
}