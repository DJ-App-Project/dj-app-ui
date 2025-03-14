using System.Text.Json.Serialization;

namespace Dj.Models
{
    public class LoginResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; } = string.Empty;

        [JsonPropertyName("user")]
        public LoginUser? User { get; set; }
    }

    public class LoginUser
    {
        [JsonPropertyName("objectId")]
        public string ObjectId { get; set; } = string.Empty;

        [JsonPropertyName("username")]
        public string Username { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("familyName")]
        public string FamilyName { get; set; } = string.Empty;

        [JsonPropertyName("imageUrl")]
        public string ImageUrl { get; set; } = string.Empty;
    }
}
