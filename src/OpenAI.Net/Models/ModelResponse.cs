using System.Collections.Generic;

namespace OpenAI.Net.Models
{
    public class ModelResponse
    {
        public string Object { get; set; }

        public List<Model> Data { get; set; }
    }
}
