using Dj.Models;
using System.Text.Json.Serialization;

namespace DJ.Models
{
    public class MusicConfig
    {
        [JsonPropertyName("musicPlaylist")]
        public List<Music> MusicPlaylist { get; set; } = new List<Music>();

        [JsonPropertyName("enableUserRecommendation")]
        public bool EnableUserRecommendation { get; set; } = false;
    }
}
