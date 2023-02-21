using OpenAI.Net.JsonConverts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace OpenAI.Net.Models
{
    public class Model
    {
        public string Id { get; set; }

        public string Object { get; set; }

        [JsonConverter(typeof(DateTimeOffsetJsonConverter))]
        public DateTimeOffset Created { get; set; }

        [JsonPropertyName("owned_by")]
        public string OwnedBy { get; set; }

        public List<Permission> Permission { get; set; } = new List<Permission>();

        public string Root { get; set; }

        public string? Parent { get; set; }
    }
}
