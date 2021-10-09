using System.Text.Json.Serialization;

namespace Canopy.Provider.Models
{
    public class Status
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }


        [JsonPropertyName("type")]
        public string Type { get; set; }


        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
