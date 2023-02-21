using OpenAI.Net.JsonConverts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace OpenAI.Net.Models
{
    public class Permission
    {
        public string Id { get; set; }

        public string Object { get; set; }

        [JsonConverter(typeof(DateTimeOffsetJsonConverter))]
        public DateTimeOffset Created { get; set; }

        [JsonPropertyName("allow_create_engine")]
        public bool AllowCreateEngine { get; set; }

        [JsonPropertyName("allow_sampling")]
        public bool AllowSampling { get; set; }

        [JsonPropertyName("allow_logprobs")]
        public bool AllowLogprobs { get; set; }

        [JsonPropertyName("allow_search_indices")]
        public bool AllowSearchIndices { get; set; }

        [JsonPropertyName("allow_view")]
        public bool AllowView { get; set; }

        [JsonPropertyName("allow_fine_tuning")]
        public bool AllowFineTuning { get; set; }

        public string Organization { get; set; }

        public string? Group { get; set; }

        [JsonPropertyName("is_blocking")]
        public bool IsBlocking { get; set; }

    }
}
