using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serials.Core;
using Serials.Data;

namespace Serials.Services
{
    public class SerialsAccessService : ISerialsAccessService
    {

        private readonly ISerialsRepository _repo;
        public SerialsAccessService(ISerialsRepository repo)
        {
            _repo = repo;
        }
        public async Task<List<SerialViewModel>> Find(SearchRequest searchReq)
        {
            bool findResult = !string.IsNullOrEmpty(searchReq.SerialNumber) || !string.IsNullOrEmpty(searchReq.EmailAddress) || !string.IsNullOrEmpty(searchReq.FirstName) || !string.IsNullOrEmpty(searchReq.FirstName);

            if (findResult)
            {
                var queryResult = await _repo.Find(searchReq);
                return queryResult.Select(SerialViewModel.ToViewModel).ToList();
            }
            var allResult = await _repo.All();
            return allResult.Select(SerialViewModel.ToViewModel).ToList();

        }
        public async Task Add(SerialViewModel model)
        {
            var entity = SerialViewModel.ToEntity(model);
            await _repo.Add(entity);
        }
        public async Task Update(SerialViewModel model)
        {
            var entity = SerialViewModel.ToEntity(model);
            await _repo.Update(entity);
        }
        public async Task Delete(SerialViewModel model)
        {
            var entity = SerialViewModel.ToEntity(model);
            await _repo.Update(entity);
        }
        public async Task<SerialViewModel> Single(string id)
        {
            var result = await _repo.Single(id);
            return SerialViewModel.ToViewModel(result);
        }




    }
}