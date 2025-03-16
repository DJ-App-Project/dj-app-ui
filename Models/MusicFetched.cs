using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dj.Models
{
    public class MusicFetched
    {
        [JsonPropertyName("objectId")]
        public string? ObjectId { get; set; }

        [JsonPropertyName("musicName")]
        public string? MusicName { get; set; }

        [JsonPropertyName("musicArtist")]
        public string? MusicArtist { get; set; }

        [JsonPropertyName("musicGenre")]
        public string? MusicGenre { get; set; }

        [JsonPropertyName("visible")]
        public bool Visible { get; set; } = true;

        [JsonPropertyName("votes")]
        public int Votes { get; set; }


        [JsonPropertyName("isUserRecommendation")]
        public bool IsUserRecommendation { get; set; }

        public bool IsSelected { get; set; }


        public MusicFetched()
        {
            IsSelected = false;
        }


        public MusicFetched(string? objectId, string? musicName, string? musicArtist, string? musicGenre)
        {
            ObjectId = objectId;
            MusicName = musicName;
            MusicArtist = musicArtist;
            MusicGenre = musicGenre;
        }
    }
}
