using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OpenAI.Net.Completions.ChatCompletions
{
    public class ChatCompletionRequest : BaseRequest
    {
        protected override string EndPoint => "/v1/chat/completions";

        /// <summary>
        /// The messages to generate chat completions for, in the <see href="https://platform.openai.com/docs/guides/chat/introduction">chat format</see>.
        /// </summary>
        public List<Message> Messages { get; set; } = new List<Message>();
    }
}