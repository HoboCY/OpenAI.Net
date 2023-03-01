using System.Text.Json.Serialization;

namespace OpenAI.Net.Completions
{
    public class Choice
    {
        public string Text { get; set; } = string.Empty;
        public int Index { get; set; }

        public object Logprobs { get; set; }

        [JsonPropertyName("finish_reason")]
        public string FinishReason { get; set; }
    }
}
