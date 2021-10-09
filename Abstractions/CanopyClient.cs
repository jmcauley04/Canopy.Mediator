using Microsoft.AspNetCore.WebUtilities;
using Canopy.Provider.Extensions;
using Canopy.Provider.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Canopy.Provider.Abstractions
{
    public abstract class CanopyClient<T> : BaseHttpClient
        where T : BusinessObject
    {
        protected readonly Account _account;

        public CanopyClient(HttpClient client, Account account) : base(client)
        {
            _account = account;
        }

        public Task<T> Get(string entityId)
            => BaseGetAsync<T>(_account.AuthenticatedEndpoint($"{_baseEndpoint}/{entityId}.json"));

        public Task<List<T>> Get(int? batchSize = null, int? batch = null, string qualifier = null, string sort = null)
        {
            var queryParams = new Dictionary<string, string>();

            queryParams.TryAddParam(nameof(batchSize), batchSize);
            queryParams.TryAddParam(nameof(batch), batch);
            queryParams.TryAddParam(nameof(qualifier), qualifier);
            queryParams.TryAddParam(nameof(sort), sort);

            return BaseGetAsync<List<T>>(QueryHelpers.AddQueryString(_account.AuthenticatedEndpoint($"{_baseEndpoint}.json"), queryParams));
        }

        public Task<T> Create(T entity)
            => BasePostAsync<T, T>(_account.AuthenticatedEndpoint($"{_baseEndpoint}.json"), entity);

        public Task<T> Update(string entityId, T entity)
            => BasePutAsync<T, T>(_account.AuthenticatedEndpoint($"{_baseEndpoint}/{entityId}.json"), entity);

        public Task Archive(string entityId)
            => BaseDeleteAsync(_account.AuthenticatedEndpoint($"{_baseEndpoint}/{entityId}.json"));
    }
}
