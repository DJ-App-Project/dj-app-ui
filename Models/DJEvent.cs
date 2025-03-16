using DJ.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Text.Json.Serialization;

namespace Dj.Models
{
    public class DJEvent
    {
        [JsonPropertyName("objectId")]
        public string? ObjectId { get; set; }

        [JsonPropertyName("id")]
        public string? UserID { get; set; }

        [JsonPropertyName("djId")]
        public string? DJID { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("location")]
        public string? Location { get; set; }

        [JsonPropertyName("qrCodeText")]
        public string? QRCode { get; set; }

        [JsonPropertyName("active")]
        public bool Active { get; set; } = true;

        [JsonPropertyName("musicConfig")]
        public MusicConfig? MusicConfig { get; set; }

        public DJEvent() { }

        public DJEvent(string objectId, string userId, string djId, string name, string description, DateTime date, string location, string qrCode, bool active, MusicConfig musicConfig)
        {
            ObjectId = objectId;
            UserID = userId;
            DJID = djId;
            Name = name;
            Description = description;
            Date = date;
            Location = location;
            QRCode = qrCode;
            Active = active;
            MusicConfig = musicConfig;
        }

        public DJEvent(string name, string description, DateTime date, string location)
        {
            Name = name;
            Description = description;
            Date = date;
            Location = location;
        }
    }

}
