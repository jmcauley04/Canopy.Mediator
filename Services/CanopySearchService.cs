using Canopy.Provider.Abstractions;
using Canopy.Provider.Enums;
using Canopy.Provider.Interfaces;
using Canopy.Provider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Canopy.Provider.Services
{
    public class CanopySearchService<T> : CanopyClient<T>,
        ICanopySearchService<T>,
        IBatchSelectionStage<T>,
        IFilterSelectionStage<T>,
        ISortSelectionStage<T>
         where T : BusinessObject
    {
        private int? batchSize;
        private int? batch; // Starts at 0
        private IList<string> filters = new List<string>();
        private IList<string> sorts = new List<string>();

        protected override string _baseEndpoint => $"api/erp/{typeof(T).Name}";

        public CanopySearchService(HttpClient client, Account account) : base(client, account)
        {
        }

        public ICanopySearchService<T> GetService()
        {
            return this;
        }

        public IBatchSelectionStage<T> WithBatchsize(int batchSize)
        {
            this.batchSize = batchSize;
            return this;
        }

        public IFilterSelectionStage<T> GetBatchNumber(int batch)
        {
            this.batch = batch;
            return this;
        }

        /// <summary>
        /// Filter the results by the specified filter parameters.
        /// </summary>
        public IFilterSelectionStage<T> Where<TKey>(Expression<Func<T, TKey>> keySelector, Comparator comparator, string value)
        {
            var propertyName = getPropertyName(keySelector);

            var compareTxt = comparator switch
            {
                Comparator.GreaterThan => ">",
                Comparator.GreaterThanOrEqualTo => ">=",
                Comparator.LessThan => "<",
                Comparator.LessThanOrEqualTo => "<=",
                Comparator.Like => "like",
                _ => throw new NotImplementedException()
            };

            if (comparator == Comparator.Like)
                value = value.Replace('%', '*');

            this.filters.Add($"{propertyName} {compareTxt} '{value}'");

            return this;
        }

        /// <summary>
        /// Filter the results by the specified filter parameters.
        /// </summary>
        public IFilterSelectionStage<T> Where<TKey>(Expression<Func<T, TKey>> keySelector, Comparator comparator, int value)
            => this.Where(keySelector, comparator, value.ToString());

        /// <summary>
        /// Filter the results by the specified filter parameters.
        /// </summary>
        public IFilterSelectionStage<T> Where<TKey>(Expression<Func<T, TKey>> keySelector, Comparator comparator, decimal value)
            => this.Where(keySelector, comparator, value.ToString());

        /// <summary>
        /// Filter the results by the specified filter parameters.
        /// </summary>
        public IFilterSelectionStage<T> Where<TKey>(Expression<Func<T, TKey>> keySelector, Comparator comparator, DateTime value)
            => this.Where(keySelector, comparator, value.ToString("O"));

        /// <summary>
        /// Sort ascending the results by the specified property.
        /// </summary>
        public ISortSelectionStage<T> OrderBy<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            var propertyName = getPropertyName(keySelector);

            sorts.Add($"{propertyName}|asc");

            return this;
        }

        /// <summary>
        /// Sort descending the results by the specified property.
        /// </summary>
        public ISortSelectionStage<T> OrderByDescending<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            var propertyName = getPropertyName(keySelector);

            sorts.Add($"{propertyName}|desc");

            return this;
        }

        /// <summary>
        /// Evaluate the request and return the results.
        /// </summary>
        public Task<List<T>> Get()
        {
            string qualifier = null;
            string sorter = null;

            if (filters.Count > 0)
                qualifier = filters.Aggregate((a, b) => $"{a},{b}");

            if (sorts.Count > 0)
                sorter = sorts.Aggregate((a, b) => $"{a},{b}");

            return base.Get(batchSize, batch, qualifier, sorter);
        }

        private string getPropertyName<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            var memberInfo = ((MemberExpression)keySelector.Body).Member;

            return memberInfo.CustomAttributes
                .SingleOrDefault(x => x.AttributeType == typeof(JsonPropertyNameAttribute))?.ConstructorArguments
                .FirstOrDefault().Value.ToString() ?? memberInfo.Name;
        }
    }

}
