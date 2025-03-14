using Microsoft.AspNetCore.Components;

namespace DJ.Pages
{
    public partial class DjDashboard : ComponentBase
    {
        private List<string> Events { get; set; } = new List<string> { "Event A", "Event B", "Event C" };

        private string _selectedEvent;
        private string SelectedEvent
        {
            get => _selectedEvent;
            set
            {
                if (_selectedEvent != value)
                {
                    _selectedEvent = value;
                    OnEventChanged();
                }
            }
        }

        public class EventData
        {
            public List<Song> Songs { get; set; } = new List<Song>();
        }

        public class Song
        {
            public string Name { get; set; }
            public string Artist { get; set; }
            public int Points { get; set; }
            public bool IsSelected { get; set; }
        }

        private Dictionary<string, EventData> EventDataDict { get; set; } = new Dictionary<string, EventData>();

        public EventData CurrentEventData { get; private set; } = new EventData();

        private string SearchTerm { get; set; } = "";
        private List<Song> AvailableSongs { get; set; } = new List<Song>
        {
            new Song { Name = "Comfortably Numb", Artist = "Pink Floyd" },
            new Song { Name = "Lose Yourself", Artist = "Eminem" },
            new Song { Name = "Rolling in the Deep", Artist = "Adele" },
            new Song { Name = "Purple Rain", Artist = "Prince" },
            new Song { Name = "Back in Black", Artist = "AC/DC" }
        };

        private IEnumerable<Song> FilteredSongs => string.IsNullOrWhiteSpace(SearchTerm)
            ? Enumerable.Empty<Song>()
            : AvailableSongs.Where(s =>
                   s.Name.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)
                || s.Artist.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase));

        private const int PageSize = 15;
        private int CurrentPage { get; set; } = 1;

        private int TotalPages =>
            (int)Math.Ceiling((decimal)(CurrentEventData?.Songs.Count / (double)PageSize));

        private IEnumerable<Song> PaginatedSongs =>
            CurrentEventData?.Songs
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize) ?? Enumerable.Empty<Song>();

        protected override void OnInitialized()
        {
            _selectedEvent = Events.First();
            InitializeEventData();
            CurrentEventData = EventDataDict[SelectedEvent];
        }

        private void OnEventChanged()
        {
            if (EventDataDict.TryGetValue(SelectedEvent, out var data))
            {
                CurrentEventData = data;
            }
            else
            {
                CurrentEventData = new EventData();
            }

            CurrentPage = 1;
            StateHasChanged();
        }

        private IEnumerable<Song> SortedLeaderboard => CurrentEventData?.Songs?
            .OrderByDescending(s => s.Points)
            .Take(10) ?? Enumerable.Empty<Song>();

        private void DeleteSelectedSongs()
        {
            CurrentEventData.Songs = CurrentEventData.Songs
                .Where(s => !s.IsSelected)
                .ToList();

            CurrentPage = 1;
            StateHasChanged();
        }

        private void OnSearchInput(ChangeEventArgs e)
        {
            SearchTerm = e.Value?.ToString() ?? string.Empty;
            StateHasChanged();
        }

        private void AddSongFromSearch(Song song)
        {
            if (!CurrentEventData.Songs.Any(s => s.Name == song.Name && s.Artist == song.Artist))
            {
                CurrentEventData.Songs.Add(new Song
                {
                    Name = song.Name,
                    Artist = song.Artist,
                    Points = 0
                });
            }
            SearchTerm = "";
            StateHasChanged();
        }

        private void GoToPage(int page)
        {
            if (page >= 1 && page <= TotalPages)
            {
                CurrentPage = page;
                StateHasChanged();
            }
        }

        private void InitializeEventData()
        {
            var eventAData = new EventData
            {
                Songs = new List<Song>
                {
                    new Song { Name = "Bohemian Rhapsody",      Artist = "Queen",            Points = 250 },
                    new Song { Name = "Stairway to Heaven",      Artist = "Led Zeppelin",     Points = 240 },
                    new Song { Name = "Hotel California",        Artist = "Eagles",           Points = 230 },
                    new Song { Name = "Imagine",                 Artist = "John Lennon",      Points = 220 },
                    new Song { Name = "Sweet Child O' Mine",     Artist = "Guns N' Roses",    Points = 210 },
                    new Song { Name = "Hey Jude",                Artist = "The Beatles",      Points = 200 },
                    new Song { Name = "Smells Like Teen Spirit", Artist = "Nirvana",          Points = 190 },
                    new Song { Name = "Billie Jean",             Artist = "Michael Jackson",  Points = 180 },
                    new Song { Name = "Like a Rolling Stone",    Artist = "Bob Dylan",        Points = 170 },
                    new Song { Name = "I Will Always Love You",  Artist = "Whitney Houston",  Points = 160 },
                    new Song { Name = "Wonderwall",              Artist = "Oasis",            Points = 150 },
                    new Song { Name = "Lose Yourself",           Artist = "Eminem",           Points = 140 },
                    new Song { Name = "Thriller",                Artist = "Michael Jackson",  Points = 130 },
                    new Song { Name = "Yesterday",               Artist = "The Beatles",      Points = 120 },
                    new Song { Name = "Sultans of Swing",        Artist = "Dire Straits",     Points = 110 }
                }
            };

            for (int i = 16; i <= 32; i++)
            {
                eventAData.Songs.Add(new Song
                {
                    Name = $"Extra Song {i}",
                    Artist = $"Extra Artist {i}",
                    Points = 100 - i // Just for variation
                });
            }

            EventDataDict["Event A"] = eventAData;

            var eventBData = new EventData
            {
                Songs = new List<Song>
                {
                    new Song { Name = "Song B1", Artist = "Artist B1", Points = 50 },
                    new Song { Name = "Song B2", Artist = "Artist B2", Points = 60 },
                    new Song { Name = "Song B3", Artist = "Artist B3", Points = 70 }
                }
            };
            EventDataDict["Event B"] = eventBData;

            var eventCData = new EventData
            {
                Songs = new List<Song>
                {
                    new Song { Name = "Song C1", Artist = "Artist C1", Points = 80 },
                    new Song { Name = "Song C2", Artist = "Artist C2", Points = 90 },
                    new Song { Name = "Song C3", Artist = "Artist C3", Points = 100 }
                }
            };
            EventDataDict["Event C"] = eventCData;
        }
    }
}
