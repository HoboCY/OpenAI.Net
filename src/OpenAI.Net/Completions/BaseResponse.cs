using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using OpenAI.Net.JsonConverts;

namespace OpenAI.Net.Completions
{
    public abstract class BaseResponse<T> where T : BaseChoice
    {
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("_object")]
        public string Object { get; set; } = string.Empty;

        [JsonConverter(typeof(DateTimeOffsetJsonConverter))]
        public DateTimeOffset Created { get; set; }

        public string Model { get; set; }

        public List<T> Choices { get; set; } = new List<T>();
        
        public Usage Usage { get; set; }
    }
}