using OpenAI.Net.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OpenAI.Net.Interfaces
{
    public interface IModelService
    {
        Task<Model> GetModelAsync(string id);
        Task<List<Model>> GetModelsAsync();
    }
}
