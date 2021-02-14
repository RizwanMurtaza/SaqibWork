using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Serials.Core;

namespace Serials.Data
{
    public class SerialsRepository : ISerialsRepository
    {
        private readonly AmazonDynamoDBClient _client;
        private readonly DynamoDBContext _context;

        public SerialsRepository()
        {
            var config = new AmazonDynamoDBConfig
            {
                ServiceURL = "https://dynamodb.eu-west-2.amazonaws.com",
            };
            _client = new AmazonDynamoDBClient("AKIARX7E3AWWPF6UOGIF", "z5mEtwxFWBsIrhMxTM4IyWb2HhcTAVt0yi2YH2KL", config);
            _context = new DynamoDBContext(_client);
        }
        public async Task<List<Serials.Core.Serials>> All(string paginationToken = "")
        {
            var table = _context.GetTargetTable<Core.Serials>();

            var scanOps = new ScanOperationConfig();

            if (!string.IsNullOrEmpty(paginationToken))
            {
                scanOps.PaginationToken = paginationToken;
            }

            var results = table.Scan(scanOps);

            List<Document> data = await results.GetNextSetAsync();

            var result  = _context.FromDocuments<Serials.Core.Serials>(data);

            return result.ToList();
        }


        public async Task Add(Serials.Core.Serials entity)
        {
            await _context.SaveAsync<Serials.Core.Serials>(entity);
        }

        public async Task<IEnumerable<Core.Serials>> Find(SearchRequest searchReq)
        {
            var scanConditions = new List<ScanCondition>();
            if (!string.IsNullOrEmpty(searchReq.SerialNumber))
                scanConditions.Add(new ScanCondition("serialnumber", ScanOperator.Contains, searchReq.SerialNumber));
            if (!string.IsNullOrEmpty(searchReq.EmailAddress))
                scanConditions.Add(new ScanCondition("email", ScanOperator.Equal, searchReq.EmailAddress));
            if (!string.IsNullOrEmpty(searchReq.FirstName))
                scanConditions.Add(new ScanCondition("firstname", ScanOperator.Equal, searchReq.FirstName));
            if (!string.IsNullOrEmpty(searchReq.FirstName))
                scanConditions.Add(new ScanCondition("lastname", ScanOperator.Equal, searchReq.FirstName));
            return await _context.ScanAsync<Core.Serials>(scanConditions, null).GetRemainingAsync();
        }

        public async Task Remove(Guid readerId)
        {
            await _context.DeleteAsync<Serials.Core.Serials>(readerId);
        }

        public async Task<Serials.Core.Serials> Single(string readerId)
        {
            return await _context.LoadAsync<Serials.Core.Serials>(readerId); //.QueryAsync<Reader>(readerId.ToString()).GetRemainingAsync();
        }

        public async Task Update(Serials.Core.Serials entity)
        {
            await _context.SaveAsync<Serials.Core.Serials>(entity);
        }
    }
}
