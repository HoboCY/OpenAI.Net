using System.Text.Json.Serialization;

namespace OpenAI.Net.Completions
{
    public abstract class BaseChoice
    {
        public int Index { get; set; }

        [JsonPropertyName("finish_reason")]
        public string FinishReason { get; set; }
    }
}