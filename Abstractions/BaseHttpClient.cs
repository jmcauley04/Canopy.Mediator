using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Canopy.Provider.Abstractions
{
    public abstract class BaseHttpClient
    {
        protected HttpClient _client;

        protected abstract string _baseEndpoint { get; }

        public BaseHttpClient(HttpClient client)
        {
            _client = client;
        }

        #region get

        /// <summary>
        /// Use to hit an endpoint without getting items back
        /// </summary>
        public async Task BaseGetAsync(string endpoint, CancellationToken ct = default)
        {
            var response = await _client.GetAsync(endpoint, ct);

            if (response.IsSuccessStatusCode)
                return;

            throw new System.Exception($"{response.StatusCode}: {await response.Content.ReadAsStringAsync()}");
        }

        /// <summary>
        /// Use to get item(s) from an api endpoint
        /// </summary>
        public async Task<TReturn> BaseGetAsync<TReturn>(string endpoint, CancellationToken ct = default)
        {
            var response = await _client.GetAsync(endpoint, ct);

            var targetUrl = _client.BaseAddress + endpoint;

            System.Console.WriteLine($"Target URL: {targetUrl}");

            if (response.IsSuccessStatusCode)
            {
                System.Console.WriteLine($"Raw: {await response.Content.ReadAsStringAsync()}");
                return System.Text.Json.JsonSerializer.Deserialize<TReturn>(await response.Content.ReadAsStringAsync());
                //return await response.Content.ReadFromJsonAsync<TReturn>();
            }

            throw new System.Exception($"{response.StatusCode}: {await response.Content.ReadAsStringAsync()}");
        }

        #endregion

        #region put

        /// <summary>
        /// Use to updating an existing item and getting an item returned from an api endpoint
        /// </summary>
        public async Task<TReturn> BasePutAsync<TSend, TReturn>(string endpoint, TSend item, CancellationToken ct = default)
        {
            var response = await _client.PutAsJsonAsync(endpoint, item, ct);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<TReturn>();

            throw new System.Exception($"{response.StatusCode}: {await response.Content.ReadAsStringAsync()}");
        }

        /// <summary>
        /// Use to updating an existing item from an api endpoint
        /// </summary>
        public async Task BasePutAsync<TSend>(string endpoint, TSend item, CancellationToken ct = default)
        {
            var response = await _client.PutAsJsonAsync(endpoint, item, ct);

            if (response.IsSuccessStatusCode)
                return;

            throw new System.Exception($"{response.StatusCode}: {await response.Content.ReadAsStringAsync()}");
        }

        #endregion

        #region post

        /// <summary>
        /// Use to create a new item and getting an item returned from an api endpoint
        /// </summary>
        public async Task<TReturn> BasePostAsync<TSend, TReturn>(string endpoint, TSend item, CancellationToken ct = default)
        {
            var response = await _client.PostAsJsonAsync(endpoint, item, ct);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<TReturn>();

            throw new System.Exception($"{response.StatusCode}: {await response.Content.ReadAsStringAsync()}");
        }

        /// <summary>
        /// Use to create a new item from an api endpoint
        /// </summary>
        public async Task BasePostAsync<TSend>(string endpoint, TSend item, CancellationToken ct = default)
        {
            var response = await _client.PostAsJsonAsync(endpoint, item, ct);

            if (response.IsSuccessStatusCode)
                return;

            throw new System.Exception($"{response.StatusCode}: {await response.Content.ReadAsStringAsync()}");
        }

        #endregion

        #region post

        /// <summary>
        /// Use to delete an item and getting an item returned from an api endpoint
        /// </summary>
        public async Task<TReturn> BaseDeleteAsync<TSend, TReturn>(string endpoint, TSend item, CancellationToken ct = default)
        {
            var response = await _client.DeleteAsync(endpoint, ct);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<TReturn>();

            throw new System.Exception($"{response.StatusCode}: {await response.Content.ReadAsStringAsync()}");
        }

        /// <summary>
        /// Use to delete an item from an api endpoint
        /// </summary>
        public async Task BaseDeleteAsync<TSend>(string endpoint, TSend item, CancellationToken ct = default)
        {
            var response = await _client.DeleteAsync(endpoint, ct);

            if (response.IsSuccessStatusCode)
                return;

            throw new System.Exception($"{response.StatusCode}: {await response.Content.ReadAsStringAsync()}");
        }

        /// <summary>
        /// Use to delete an item from an api endpoint
        /// </summary>
        public async Task BaseDeleteAsync(string endpoint, CancellationToken ct = default)
        {
            var response = await _client.DeleteAsync(endpoint, ct);

            if (response.IsSuccessStatusCode)
                return;

            throw new System.Exception($"{response.StatusCode}: {await response.Content.ReadAsStringAsync()}");
        }

        #endregion
    }
}

