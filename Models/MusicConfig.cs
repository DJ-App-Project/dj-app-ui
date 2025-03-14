using System.Text.Json.Serialization;

namespace DJ.Models
{
    public class MusicConfig
    {
        [JsonPropertyName("musicPlaylist")]
        public List<string> MusicPlaylist { get; set; } = new List<string>();

        [JsonPropertyName("enableUserRecommendation")]
        public bool EnableUserRecommendation { get; set; } = false;
    }
}
