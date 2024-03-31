using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AvaloniaMiaDev.Services;

public interface ILoginService
{
    Task<AuthenticationResult?> Authenticate(string username, string password);
    Task<DummyUser[]> Users();
}

public class LoginService(HttpClient httpClient) : ILoginService
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    public async Task<AuthenticationResult?> Authenticate(string username, string password)
    {
        var response = await httpClient.PostAsync("auth/login", JsonContent.Create(new
        {
            username,
            password,
        }));
        var content = await response.Content.ReadAsStringAsync();
        return response.IsSuccessStatusCode
            ? JsonSerializer.Deserialize<AuthenticationResult>(content, JsonOptions)
            : null; }

    public async Task<DummyUser[]> Users()
    {
        var response = await httpClient.GetFromJsonAsync<UsersResponse>("users");
        return response is null ? Array.Empty<DummyUser>() : response.Users;
    }
}

public record AuthenticationResult(
    int Id,
    string Username,
    string Email,
    string FirstName,
    string LastName,
    string Gender,
    string Image,
    string Token);

public record UsersResponse(DummyUser[] Users);
public record DummyUser(string Username, string Password, string FirstName, string LastName)
{
    public string FullName => $"{FirstName} {LastName}";
}
