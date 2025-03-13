using Dj.Models;
using System.Net.Http.Json;
using System.Text.Json; // for JsonSerializer
using System.Text.Json.Serialization;

namespace Dj.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public async Task<LoginResponse?> LoginAsync(string username, string password)
        {
            var payload = new { username, password };

            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/auth/login", payload);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("[AuthService] Login successful.");
                    Console.WriteLine("[AuthService] JSON response: " + json);

                    var loginResponse = JsonSerializer.Deserialize<LoginResponse>(json);

                    return loginResponse;
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("[AuthService] Login failed with status code: " + response.StatusCode);
                    Console.WriteLine("[AuthService] Error response: " + error);
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[AuthService] Exception occurred during login:");
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
