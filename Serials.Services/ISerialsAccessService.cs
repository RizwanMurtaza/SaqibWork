using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Serials.Core;

namespace Serials.Services
{
    public interface ISerialsAccessService
    {
        Task<List<SerialViewModel>> Find(SearchRequest searchReq);
        Task Add(SerialViewModel model);
        Task Update(SerialViewModel model);
        Task Delete(SerialViewModel model);
        Task<SerialViewModel> Single(string id);
    }
}
