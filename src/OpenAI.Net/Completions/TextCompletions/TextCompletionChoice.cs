namespace OpenAI.Net.Completions.TextCompletions
{
    public class TextCompletionChoice : BaseChoice
    {
        public string Text { get; set; }

        public object Logprobs { get; set; }
    }
}