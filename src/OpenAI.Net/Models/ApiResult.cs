using System;
using System.Collections.Generic;
using System.Text;

namespace OpenAI.Net.Models
{
    public class ApiResult
    {
        public string Object { get; set; }

        public List<Model> Data { get; set; }
    }
}
