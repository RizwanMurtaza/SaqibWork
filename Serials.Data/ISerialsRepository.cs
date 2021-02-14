using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Serials.Core;

namespace Serials.Data
{
    public interface ISerialsRepository
    {
        Task<List<Core.Serials>> All(string paginationToken = "");
        Task Add(Core.Serials entity);
        Task<IEnumerable<Core.Serials>> Find(SearchRequest searchReq);
        Task Remove(Guid readerId);
        Task<Core.Serials> Single(string readerId);
        Task Update(Core.Serials entity);
    }
}