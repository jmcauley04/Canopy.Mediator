using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Canopy.Provider.Models
{
    public class Brand : BusinessObject
    {
        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        [JsonPropertyName("name")]
        public string Name { get; set; }


        [JsonPropertyName("productRanges")]
        public Range[] ProductRanges { get; set; }


        [MaxLength(1)]
        [JsonPropertyName("canopyPublic")]
        public string CanopyPublic { get; set; }


        [MaxLength(1)]
        [JsonPropertyName("isGenericBrand")]
        public string IsGenericBrand { get; set; }


        [MaxLength(1)]
        [JsonPropertyName("privateLabel")]
        public string PrivateLabel { get; set; }
    }
}
