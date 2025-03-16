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


        public async Task<List<DJEvent>> GetEventsAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("token");

            if (!string.IsNullOrEmpty(token))
            {
                _http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            else
            {
                Console.WriteLine("No token found in LocalStorage.");
                return new List<DJEvent>(); // Return empty list if no token
            }

            Console.WriteLine("Sending GET request to fetch events...");
            var response = await _http.GetAsync("api/event/EventsFromUser");

            if (response.IsSuccessStatusCode)
            {
                var events = await response.Content.ReadFromJsonAsync<List<DJEvent>>();
                Console.WriteLine($"Fetched {events?.Count ?? 0} events.");
                return events ?? new List<DJEvent>(); // Return events or an empty list
            }
            else
            {
                Console.WriteLine($"Failed to fetch events. Status code: {response.StatusCode}");
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error content: {errorContent}");
                return new List<DJEvent>(); // Return an empty list on failure
            }
        }

        public async Task<List<Music>> GetAllSongsAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("token");
            if (!string.IsNullOrEmpty(token))
            {
                _http.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _http.GetAsync("api/songs?page=1&pageSize=1000000");
            if (response.IsSuccessStatusCode)
            {
                var songs = await response.Content.ReadFromJsonAsync<List<Music>>();

                return songs ?? new List<Music>();
            }
            else
            {
                return new List<Music>();
            }
        }

        public async Task<bool> AddMusicToEventAsync(string eventId, Music music)
        {
            var token = await _localStorage.GetItemAsync<string>("token");
            if (!string.IsNullOrEmpty(token))
            {
                _http.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            Console.WriteLine("ID eventa kam se doda glasba: " + eventId);
            var requestBody = new
            {
                EvendId = eventId,
                musicName = music.MusicName,
                musicArtist = music.MusicArtist,
                musicGenre = music.MusicGenre,
                visible = true
            };

            var response = await _http.PostAsJsonAsync("AddMusicToEvent", requestBody);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Failed to add music to event. Status: {response.StatusCode}, Error: {errorContent}");
                return false;
            }
        }

        public async Task<List<MusicFetched>> GetEventMusicAsync(string eventId)
        {
            var token = await _localStorage.GetItemAsync<string>("token");
            if (!string.IsNullOrEmpty(token))
            {
                _http.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _http.GetAsync($"Music/{eventId}");
            if (response.IsSuccessStatusCode)
            {
                var musicList = await response.Content.ReadFromJsonAsync<List<MusicFetched>>();
                return musicList ?? new List<MusicFetched>();
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Failed to get event music. Status: {response.StatusCode}, Error: {error}");
                return new List<MusicFetched>();
            }
        }

        public async Task<List<MusicLeaderboard>> GetLeaderboardAsync(string eventId)
        {
            var token = await _localStorage.GetItemAsync<string>("token");
            if (!string.IsNullOrEmpty(token))
            {
                _http.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _http.GetAsync($"api/event/{eventId}/leaderboard");
            if (response.IsSuccessStatusCode)
            {
                var leaderboard = await response.Content.ReadFromJsonAsync<List<MusicLeaderboard>>();
                return leaderboard ?? new List<MusicLeaderboard>();
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Failed to get leaderboard. Status: {response.StatusCode}, Error: {error}");
                return new List<MusicLeaderboard>();
            }
        }

        public async Task<bool> SetEnableUserRecommendationAsync(string eventId, bool enableUserRecommendation)
        {
            var token = await _localStorage.GetItemAsync<string>("token");
            if (!string.IsNullOrEmpty(token))
            {
                _http.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            var requestBody = new
            {
                eventId = eventId,
                enableUserRecommendation = enableUserRecommendation
            };

            var response = await _http.PostAsJsonAsync("SetEnableUserRecommendation", requestBody);
            Console.WriteLine("Response from seting user recc: " + response.IsSuccessStatusCode);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveMusicFromEventAsync(string eventId, string musicName, string musicArtist)
        {
            var token = await _localStorage.GetItemAsync<string>("token");
            if (!string.IsNullOrEmpty(token))
            {
                _http.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            var requestBody = new
            {
                musicName = musicName,
                musicArtist = musicArtist
            };

            var response = await _http.PostAsJsonAsync(
                $"api/event/RemoveMusicFromEvent/{eventId}",
                requestBody
            );

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Failed to remove music. Status: {response.StatusCode}, Error: {error}");
                return false;
            }
        }

        public async Task<bool> VoteForEventSongAsync(string eventId, string musicName, string musicArtist)
        {
            var token = await _localStorage.GetItemAsync<string>("token");
            if (!string.IsNullOrEmpty(token))
            {
                _http.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            var requestBody = new
            {
                musicName = musicName,
                musicArtist = musicArtist
            };

            var response = await _http.PostAsJsonAsync($"api/event/VoteForEventSong/{eventId}", requestBody);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Failed to vote for song. Status: {response.StatusCode}, Error: {error}");
                return false;
            }
        }
    }

}
