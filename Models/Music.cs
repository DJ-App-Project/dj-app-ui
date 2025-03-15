using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dj.Models
{
    public class Music
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

        [JsonPropertyName("votersIDs")]
        public List<string> VotersIDs { get; set; } = new List<string>();

        [JsonPropertyName("isUserRecommendation")]
        public bool IsUserRecommendation { get; set; }

        [JsonPropertyName("recommenderID")]
        public string? RecommenderID { get; set; }

        public Music(string? objectId, string? musicName, string? musicArtist, string? musicGenre, bool visible, int votes, List<string> votersIDs, bool isUserRecommendation, string? recommenderID)
        {
            ObjectId = objectId;
            MusicName = musicName;
            MusicArtist = musicArtist;
            MusicGenre = musicGenre;
            Visible = visible;
            Votes = votes;
            VotersIDs = votersIDs;
            IsUserRecommendation = isUserRecommendation;
            RecommenderID = recommenderID;
        }

        public Music(string? musicName, string? musicArtist, string? musicGenre)
        {
            MusicName = musicName;
            MusicArtist = musicArtist;
            MusicGenre = musicGenre;
        }
    }
}
