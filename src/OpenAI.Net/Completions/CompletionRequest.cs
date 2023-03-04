﻿using System;
using System.Numerics;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace OpenAI.Net.Completions
{
    /// <summary>
    /// Creates a completion for the provided prompt and parameters
    /// <see href="https://platform.openai.com/docs/api-reference/completions/create"/>
    /// </summary>
    public class CompletionRequest
    {
        /// <summary>
        /// ID of the model to use. You can use the <see href="https://platform.openai.com/docs/api-reference/models/list">List models</see> API to see all of your available models, or see our 
        /// <see href="https://platform.openai.com/docs/models/overview">Model overview</see> for descriptions of them.
        /// </summary>
        public string Model { get; set; } = "text-davinci-003";

        /// <summary>
        /// The prompt(s) to generate completions for, encoded as a string, array of strings, array of tokens, or array of token arrays.
        /// Note that endoftext is the document separator that the model sees during training,
        /// so if a prompt is not specified the model will generate as if from the beginning of a new document.
        /// </summary>
        public string Prompt { get; set; } = string.Empty;

        /// <summary>
        /// The suffix that comes after a completion of inserted text.
        /// </summary>
        public string? Suffix { get; set; }

        /// <summary>
        /// The maximum number of <see href="https://platform.openai.com/tokenizer">tokens</see> to generate in the completion.
        /// The token count of your prompt plus max_tokens cannot exceed the model's context length.
        /// Most models have a context length of 2048 tokens (except for the newest models, which support 4096).
        /// </summary>
        [JsonPropertyName("max_tokens")]
        public int MaxTokens { get; set; } = 16;

        /// <summary>
        /// What sampling temperature to use, between 0 and 2. Higher values like 0.8 will make the output more random,
        /// while lower values like 0.2 will make it more focused and deterministic.
        /// We generally recommend altering this or <see cref="TopP">top_p</see> but not both.
        /// </summary>
        public int Temperature { get; set; } = 1;

        /// <summary>
        /// An alternative to sampling with temperature, called nucleus sampling,
        /// where the model considers the results of the tokens with top_p probability mass.
        /// So 0.1 means only the tokens comprising the top 10% probability mass are considered.
        /// We generally recommend altering this or <see cref="Temperature">temperature</see> but not both.
        /// </summary>
        [JsonPropertyName("top_p")]
        public int TopP { get; set; } = 1;

        /// <summary>
        /// How many completions to generate for each prompt.
        /// Because this parameter generates many completions, it can quickly consume your token quota. 
        /// Use carefully and ensure that you have reasonable settings for <see cref="MaxTokens">max_tokens</see> and <see cref="Stop">stop</see>.
        /// </summary>
        public int N { get; set; } = 1;

        /// <summary>
        /// Whether to stream back partial progress. If set,
        /// tokens will be sent as data-only <see href="https://developer.mozilla.org/en-US/docs/Web/API/Server-sent_events/Using_server-sent_events#event_stream_format">server-sent events</see> as they become available,
        /// with the stream terminated by a data: [DONE] message.
        /// </summary>
        public bool Stream { get; set; }

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
        /// Up to 4 sequences where the API will stop generating further tokens. The returned text will not contain the stop sequence.
        /// </summary>
        public string? Stop { get; set; }

        /// <summary>
        /// Number between -2.0 and 2.0. Positive values penalize new tokens based on whether they appear in the text so far,
        /// increasing the model's likelihood to talk about new topics.
        /// <see href="https://platform.openai.com/docs/api-reference/parameter-details">See more information about frequency and presence penalties.</see>
        /// </summary>
        [JsonPropertyName("presence_penalty")]
        public int PresencePenalty { get; set; }

        /// <summary>
        /// Number between -2.0 and 2.0. Positive values penalize new tokens based on their existing frequency in the text so far,
        /// decreasing the model's likelihood to repeat the same line verbatim.
        /// <see href="https://platform.openai.com/docs/api-reference/parameter-details">See more information about frequency and presence penalties.</see>
        /// </summary>
        [JsonPropertyName("frequency_penalty")]
        public int FrequencyPenalty { get; set; }

        /// <summary>
        /// Generates <see cref="BestOf">best_of</see> completions server-side and returns the "best" (the one with the highest log probability per token).
        /// Results cannot be streamed.
        /// When used with <see cref="N">n</see>, best_of controls the number of candidate completions and n specifies how many to return – best_of must be greater than n.
        /// Note: Because this parameter generates many completions, it can quickly consume your token quota. 
        /// Use carefully and ensure that you have reasonable settings for <see cref="MaxTokens">max_tokens</see> and <see cref="Stop">stop</see>.
        /// </summary>
        [JsonPropertyName("best_of")]
        public int BestOf { get; set; } = 1;

        /// <summary>
        /// Modify the likelihood of specified tokens appearing in the completion.
        /// Accepts a json object that maps tokens (specified by their token ID in the GPT tokenizer) to an associated bias value from -100 to 100.
        /// You can use this <see href="https://platform.openai.com/tokenizer?view=bpe">tokenizer tool</see> (which works for both GPT-2 and GPT-3) to convert text to token IDs.
        /// Mathematically, the bias is added to the logits generated by the model prior to sampling.
        /// The exact effect will vary per model, but values between -1 and 1 should decrease or increase likelihood of selection;
        /// values like -100 or 100 should result in a ban or exclusive selection of the relevant token.
        /// </summary>
        [JsonPropertyName("logit_bias")]
        public JsonObject? LogitBias { get; set; }

        /// <summary>
        /// A unique identifier representing your end-user, which can help OpenAI to monitor and detect abuse. <see href="https://platform.openai.com/docs/guides/safety-best-practices/end-user-ids">Learn more</see>.
        /// </summary>
        public string User { get; set; } = string.Empty;
    }
}
