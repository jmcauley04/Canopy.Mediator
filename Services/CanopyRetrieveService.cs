using Canopy.Provider.Abstractions;
using Canopy.Provider.Enums;
using Canopy.Provider.Interfaces;
using Canopy.Provider.Models;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Canopy.Provider.Services
{
    public class CanopyRetrieveService<T> : CanopyClient<T>,
        ICanopyRetrieveService<T>,
        IGetStage<T>
         where T : BusinessObject
    {
        private readonly CanopySearchService<Product> searchService;

        private string _searchCriteria;
        private searchType _searchType;

        private enum searchType
        {
            Code,
            Gtin,
            UUID
        }

        protected override string _baseEndpoint => $"api/erp/{typeof(T).Name}";

        public CanopyRetrieveService(HttpClient client, Account account, CanopySearchService<Product> searchService) : base(client, account)
        {
            this.searchService = searchService;
        }

        public ICanopyRetrieveService<T> GetService()
        {
            return this;
        }

        public IGetStage<T> WithCode(int code)
        {
            _searchCriteria = code.ToString();
            _searchType = searchType.Code;

            return this;
        }

        public IGetStage<T> WithGtin(string gtin)
        {
            _searchCriteria = gtin;
            _searchType = searchType.Gtin;

            return this;
        }

        public IGetStage<T> WithUUID(string uuid)
        {
            _searchCriteria = uuid;
            _searchType = searchType.UUID;

            return this;
        }

        public async Task<T> Get()
        {
            var product = _searchType switch
            {
                searchType.Code => (await searchService
                                        .WithBatchsize(1)
                                        .GetBatchNumber(0)
                                        .Where(x => x.Code, Comparator.Like, _searchCriteria)
                                        .Get())
                                    .SingleOrDefault(),

                searchType.Gtin => (await searchService
                                        .WithBatchsize(1)
                                        .GetBatchNumber(0)
                                        .Where(x => x.Gtin, Comparator.Like, _searchCriteria)
                                        .Get())
                                    .SingleOrDefault(),

                searchType.UUID => new Product() { UUID = _searchCriteria },

                _ => throw new NotImplementedException()
            };

            if (product == null)
                throw new ArgumentException($"Product with {_searchType} = '{_searchCriteria}' not found");

            return await base.Get(product.UUID);
        }
    }
}
