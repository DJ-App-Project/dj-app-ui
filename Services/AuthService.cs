using Dj.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Net.WebRequestMethods;

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

        public async Task<HttpResponseMessage> RegisterAsync(string username, string email, string name, string familyName, string password, string imageUrl)
        {
            var payload = new { name, familyName, imageUrl, username, email, password };

            Console.WriteLine("Sending POST request to create event...");
            var response = await _httpClient.PostAsJsonAsync("/api/auth/Register", payload);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Registered successfully.");
            }
            else
            {
                Console.WriteLine($"Failed to Register user. Status code: {response.StatusCode}");
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error content: {errorContent}");
            }

            return response;
        }
    }
}
