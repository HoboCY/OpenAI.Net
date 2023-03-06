namespace OpenAI.Net.Models
{
    public class Error
    {
        public string Message { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public string Param { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;
    }
}
