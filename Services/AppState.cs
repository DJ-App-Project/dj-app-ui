using Dj.Models;
using System;
using System.Threading.Tasks;
using Blazored.LocalStorage;

namespace Dj.Services
{
    public class AppState
    {
        private readonly ILocalStorageService _localStorage;

        public AppState(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public bool IsLoggedIn { get; private set; }

        public LoginUser? CurrentUser { get; private set; }

        public event Action? OnChange;

        public async Task InitializeAsync()
        {
            IsLoggedIn = await _localStorage.GetItemAsync<bool>("IsLoggedIn");
            CurrentUser = await _localStorage.GetItemAsync<LoginUser>("CurrentUser");
            NotifyStateChanged();
        }

        public async Task SetLoggedInAsync(bool value, LoginUser? user = null)
        {
            IsLoggedIn = value;
            CurrentUser = user;

            await _localStorage.SetItemAsync("IsLoggedIn", IsLoggedIn);
            await _localStorage.SetItemAsync("CurrentUser", CurrentUser);

            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}