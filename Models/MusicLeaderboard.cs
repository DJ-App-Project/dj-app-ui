using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dj.Models
{
    public class MusicLeaderboard
    {
        [JsonPropertyName("rank")]
        public int? Rank { get; set; }

        [JsonPropertyName("musicName")]
        public string? MusicName { get; set; }

        [JsonPropertyName("artist")]
        public string? MusicArtist { get; set; }

        [JsonPropertyName("votes")]
        public int? Votes { get; set; }

        [JsonPropertyName("isUserRecommendation")]
        public bool IsUserRecommendation { get; set; }

        public MusicLeaderboard() { }

        public MusicLeaderboard(int rank, string? musicName, string? musicArtist, int votes)
        {
            Rank = rank;
            MusicName = musicName;
            MusicArtist = musicArtist;
            Votes = votes;
        }
    }
}
