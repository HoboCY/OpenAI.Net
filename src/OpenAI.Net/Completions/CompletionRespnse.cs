using OpenAI.Net.JsonConverts;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OpenAI.Net.Completions
{
    public class CompletionRespnse
    {
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("_object")]
        public string Object { get; set; } = string.Empty;

        [JsonConverter(typeof(DateTimeOffsetJsonConverter))]
        public DateTimeOffset Created { get; set; }

        public string Model { get; set; } = string.Empty;

        public List<Choice> Choices { get; set; } = new List<Choice>();

        public Usage Usage { get; set; } = new Usage();
    }
}
