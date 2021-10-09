using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Canopy.Provider.Models
{
    public class Media : BusinessObject
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }


        [JsonPropertyName("status")]
        public Status? Status { get; set; }


        [JsonPropertyName("thumb")]
        public string ImgThumbURL { get; set; }


        [JsonPropertyName("smallPreview")]
        public string ImgSmallPreviewURL { get; set; }


        [JsonPropertyName("preview")]
        public string ImgPreviewURL { get; set; }


        [JsonPropertyName("full")]
        public string ImgFullURL { get; set; }


        [JsonPropertyName("svg")]
        public string ImgSVGURL { get; set; }


        [JsonPropertyName("original")]
        public string ImgOriginalURL { get; set; }


        [MaxLength(100)]
        [JsonPropertyName("originalFilename")]
        public string OriginalFilename { get; set; }


        [Required]
        [JsonPropertyName("dateCreated")]
        public DateTime? CreatedOn { get; set; }


        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        [JsonPropertyName("code")]
        public string Code { get; set; }


        [JsonPropertyName("dateLastModified")]
        public DateTime? LastModified { get; set; }


        [Required]
        [MinLength(1)]
        [MaxLength(1)]
        [JsonPropertyName("attchmentStatus")]
        public string AttachmentStatus { get; set; }


        [JsonPropertyName("themeTags")]
        public string ThemeTags { get; set; }


        [MaxLength(100)]
        [JsonPropertyName("imageCodeA")]
        public string ImageCodeA { get; set; }


        [MaxLength(100)]
        [JsonPropertyName("imageCodeB")]
        public string ImageCodeB { get; set; }


        [JsonPropertyName("extraTags")]
        public string ExtraTags { get; set; }


        [JsonPropertyName("dateShoot")]
        public DateTime? ShootDate { get; set; }


        [JsonPropertyName("exposureDate")]
        public DateTime? ExposureDate { get; set; }


        [JsonPropertyName("nodeReference")]
        public string NodeReference { get; set; }


        [JsonPropertyName("originalDistributionPermit")]
        public string OriginalDistributionPermit { get; set; }
    }
}