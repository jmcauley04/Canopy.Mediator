using Canopy.Provider.Interfaces;
using Canopy.Provider.Models;
using Canopy.Provider.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;

namespace Canopy.Provider.Extensions
{
    public static class CanopyServicesInjection
    {
        public static void AddCanopyServices(this IServiceCollection services, Uri apiBaseUrl, Account account, WebProxy proxy = null)
        {
            #region Add ProviderService Logic

            void AddProviderSearchService<TClient, TImplementation>()
                where TClient : class where TImplementation : class, TClient
            {
                services.AddTransient<TClient>(cfg => CanopyServicesProvider.GetSearchService<TClient, TImplementation>(apiBaseUrl, account, proxy));
            }

            void AddProviderRetrieveService<TClient, TImplementation>()
                where TClient : class where TImplementation : class, TClient
            {
                services.AddTransient<TClient>(cfg => CanopyServicesProvider.GetRetrieveService<TClient, TImplementation>(apiBaseUrl, account, proxy));
            }

            #endregion

            AddProviderSearchService<ICanopySearchService<Media>, CanopySearchService<Media>>();
            AddProviderSearchService<ICanopySearchService<Product>, CanopySearchService<Product>>();

            AddProviderRetrieveService<ICanopyRetrieveService<Media>, CanopyRetrieveService<Media>>();
            AddProviderRetrieveService<ICanopyRetrieveService<Product>, CanopyRetrieveService<Product>>();
        }
    }
}
