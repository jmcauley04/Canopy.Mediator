using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Canopy.Provider.Models
{

    public abstract class BusinessObject
    {
        [JsonPropertyName("descr")]
        public string Description { get; set; }

        [MaxLength(100)]
        [JsonPropertyName("uuid")]
        public string UUID { get; set; }
    }
}
