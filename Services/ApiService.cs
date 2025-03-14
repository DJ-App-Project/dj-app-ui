using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Dj.Models;

namespace Dj.Services
{
    public class ApiService
    {
        private readonly HttpClient _http;
        private readonly ILocalStorageService _localStorage;


        public ApiService(IHttpClientFactory httpClientFactory, ILocalStorageService localStorage)
        {
            _http = httpClientFactory.CreateClient("ApiClient");
            _localStorage = localStorage;
        }

        public async Task<HttpResponseMessage> CreateEventAsync(DJEvent newEvent)
        {
            var token = await _localStorage.GetItemAsync<string>("token");
            if (!string.IsNullOrEmpty(token))
            {
                Console.WriteLine($"Token retrieved: {token}");
                _http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            else
            {
                Console.WriteLine("No token found in LocalStorage.");
            }

            Console.WriteLine("Sending POST request to create event...");
            var response = await _http.PostAsJsonAsync("CreateEvent", newEvent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Event created successfully.");
            }
            else
            {
                Console.WriteLine($"Failed to create event. Status code: {response.StatusCode}");
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error content: {errorContent}");
            }

            return response;
        }

    }

}
