using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Canopy.Provider.Models
{
    public class Product : BusinessObject
    {
        [MaxLength(100)]
        [JsonPropertyName("size")]
        public string Size { get; set; }


        [JsonPropertyName("themeTags")]
        public string ThemeTags { get; set; }


        [MaxLength(100)]
        [JsonPropertyName("dimension")]
        public string Dimension { get; set; }


        [MaxLength(1)]
        [JsonPropertyName("canopyPublic")]
        public string CanopyPublic { get; set; }


        [MaxLength(100)]
        [JsonPropertyName("legalCode")]
        public string LegalCode { get; set; }


        [JsonPropertyName("heroMedia")]
        public Media HeroMedia { get; set; }


        [MaxLength(100)]
        [JsonPropertyName("colour")]
        public string Color { get; set; }


        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        [JsonPropertyName("code")]
        public string Code { get; set; }


        [MaxLength(100)]
        [JsonPropertyName("rrp")]
        public string RRP { get; set; }


        [MaxLength(100)]
        [JsonPropertyName("legalCodeName")]
        public string LegalCodeName { get; set; }


        [JsonPropertyName("status")]
        public Status Status { get; set; }


        [JsonPropertyName("story")]
        public string Story { get; set; }


        [JsonPropertyName("eolDate")]
        public DateTime? EoLDate { get; set; }


        [MaxLength(100)]
        [JsonPropertyName("weight")]
        public string Weight { get; set; }


        [MaxLength(100)]
        [JsonPropertyName("volume")]
        public string Volume { get; set; }


        [MaxLength(100)]
        [JsonPropertyName("gtin")]
        public string Gtin { get; set; }


        [JsonPropertyName("exposureDate")]
        public DateTime? ExposureDate { get; set; }


        [MaxLength(100)]
        [JsonPropertyName("style")]
        public string Style { get; set; }


        [JsonPropertyName("dateLastModified")]
        public DateTime? LastModifiedOn { get; set; }


        [JsonPropertyName("applicableMedia")]
        public Media[] ApplicableMedia { get; set; }
    }
}
