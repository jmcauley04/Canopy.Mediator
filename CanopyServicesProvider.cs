using Canopy.Provider.Models;
using Canopy.Provider.Services;
using System;
using System.Net;
using System.Net.Http;

namespace Canopy.Provider
{
    public class CanopyServicesProvider
    {
        /// <summary>
        /// Gets the search Canopy service
        /// </summary>
        /// <typeparam name="TInterface">A Canopy service interface (Canopy.Provider.Interfaces)</typeparam>
        /// <typeparam name="TImplementation">A Canopy service implementation (Canopy.Provider.Services)</typeparam>
        /// <param name="apiBaseUrl">Canopy base URL</param>
        /// <param name="account">Account object with UUID and SHA 256 encoding key</param>
        /// <param name="proxy">(optional) Configure a proxy if required</param>
        public static TInterface GetSearchService<TInterface, TImplementation>(Uri apiBaseUrl, Account account, WebProxy proxy = null)
               where TInterface : class where TImplementation : class, TInterface
        {
            var httpClient = GetHttpClient(apiBaseUrl, proxy);

            return (TInterface)typeof(TImplementation)
                    .GetConstructor(new Type[] { typeof(HttpClient), typeof(Account) })
                    .Invoke(new object[] { httpClient, account });
        }

        /// <summary>
        /// Gets the retrieve Canopy service
        /// </summary>
        /// <typeparam name="TInterface">A Canopy service interface (Canopy.Provider.Interfaces)</typeparam>
        /// <typeparam name="TImplementation">A Canopy service implementation (Canopy.Provider.Services)</typeparam>
        /// <param name="apiBaseUrl">Canopy base URL</param>
        /// <param name="account">Account object with UUID and SHA 256 encoding key</param>
        /// <param name="proxy">(optional) Configure a proxy if required</param>
        public static TInterface GetRetrieveService<TInterface, TImplementation>(Uri apiBaseUrl, Account account, WebProxy proxy = null)
               where TInterface : class where TImplementation : class, TInterface
        {
            var httpClient = GetHttpClient(apiBaseUrl, proxy);

            CanopySearchService<Product> productSearch = new(httpClient, account);

            return (TInterface)typeof(TImplementation)
                    .GetConstructor(new Type[] { typeof(HttpClient), typeof(Account), typeof(CanopySearchService<Product>) })
                    .Invoke(new object[] { httpClient, account, productSearch });
        }


        private static HttpClient GetHttpClient(Uri apiBaseUrl, WebProxy proxy = null)
        {
            var httpClientHandler = new HttpClientHandler()
            {
                Proxy = proxy,
                UseProxy = true,
                AllowAutoRedirect = true,
            };

            return new HttpClient(httpClientHandler)
            {
                BaseAddress = apiBaseUrl
            };
        }
    }
}
