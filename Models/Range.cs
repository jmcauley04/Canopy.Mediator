using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Canopy.Provider.Models
{
    public class Range : BusinessObject
    {
        [MaxLength(100)]
        [JsonPropertyName("department")]
        public string Department { get; set; }


        [JsonPropertyName("products")]
        public Product[] Products { get; set; }


        [JsonPropertyName("story")]
        public string Story { get; set; }


        [Required]
        [JsonPropertyName("dateCreated")]
        public DateTime CreatedOn { get; set; }


        [MaxLength(1)]
        [JsonPropertyName("canopyPublic")]
        public string CanopyPublic { get; set; }


        [MaxLength(100)]
        [JsonPropertyName("urlName")]
        public string UrlName { get; set; }


        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
