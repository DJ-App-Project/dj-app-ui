using Dj.Models;
using System;

namespace Dj.Services
{
    public class AppState
    {
        public bool IsLoggedIn { get; private set; }

        public LoginUser? CurrentUser { get; private set; }

        public event Action? OnChange;

        public void SetLoggedIn(bool value, LoginUser? user = null)
        {
            IsLoggedIn = value;
            CurrentUser = user;
            Console.WriteLine($"[AppState] SetLoggedIn: {IsLoggedIn}, User: {user?.Username}");

            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
