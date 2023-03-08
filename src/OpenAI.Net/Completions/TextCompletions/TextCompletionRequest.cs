using System;
using System.Text.Json.Serialization;

namespace OpenAI.Net.Completions.TextCompletions
{
    /// <summary>
    /// Creates a completion for the provided prompt and parameters
    /// <see href="https://platform.openai.com/docs/api-reference/completions/create"/>
    /// </summary>
    public class TextCompletionRequest : BaseRequest
    {
        protected override string EndPoint => "/v1/completions";

        /// <summary>
        /// The prompt(s) to generate completions for, encoded as a string, array of strings, array of tokens, or array of token arrays.
        /// Note that endoftext is the document separator that the model sees during training,
        /// so if a prompt is not specified the model will generate as if from the beginning of a new document.
        /// </summary>
        public string[] Prompt { get; set; } = Array.Empty<string>();

        /// <summary>
        /// The suffix that comes after a completion of inserted text.
        /// </summary>
        public string Suffix { get; set; }
        
        /// <summary>
        /// Include the log probabilities on the logprobs most likely tokens, as well the chosen tokens. For example,
        /// if logprobs is 5, the API will return a list of the 5 most likely tokens.
        /// The API will always return the logprob of the sampled token, so there may be up to logprobs+1 elements in the response.
        /// </summary>
        public int? Logprobs { get; set; }

        /// <summary>
        /// Echo back the prompt in addition to the completion.
        /// </summary>
        public bool Echo { get; set; }

        /// <summary>
        /// Generates <see cref="BestOf">best_of</see> completions server-side and returns the "best" (the one with the highest log probability per token).
        /// Results cannot be streamed.
        /// When used with <see cref="N">n</see>, best_of controls the number of candidate completions and n specifies how many to return – best_of must be greater than n.
        /// Note: Because this parameter generates many completions, it can quickly consume your token quota. 
        /// Use carefully and ensure that you have reasonable settings for <see cref="MaxTokens">max_tokens</see> and <see cref="Stop">stop</see>.
        /// </summary>
        [JsonPropertyName("best_of")]
        public int BestOf { get; set; } = 1;
    }
}
