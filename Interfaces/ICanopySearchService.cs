using Canopy.Provider.Enums;
using Canopy.Provider.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Canopy.Provider.Interfaces
{
    #region fluent interfaces

    public interface ICanopySearchService<T> where T : BusinessObject
    {
        /// <summary>
        /// Specify the number of objects to return. Default = 25.
        /// </summary>
        /// <param name="batchSize">The number of objects to return</param>
        IBatchSelectionStage<T> WithBatchsize(int batchSize);
        IFilterSelectionStage<T> Where<TKey>(Expression<Func<T, TKey>> keySelector, Comparator comparator, string value);
        IFilterSelectionStage<T> Where<TKey>(Expression<Func<T, TKey>> keySelector, Comparator comparator, decimal value);
        IFilterSelectionStage<T> Where<TKey>(Expression<Func<T, TKey>> keySelector, Comparator comparator, int value);
        IFilterSelectionStage<T> Where<TKey>(Expression<Func<T, TKey>> keySelector, Comparator comparator, DateTime value);
        Task<List<T>> Get();
    }

    public interface IBatchSelectionStage<T> where T : BusinessObject
    {
        /// <summary>
        /// Specify the requested batch number.  Starts at batch = 0.
        /// </summary>
        /// <param name="batch">The first batch is batch 0.</param>
        IFilterSelectionStage<T> GetBatchNumber(int batch);
    }

    public interface IFilterSelectionStage<T> where T : BusinessObject
    {
        IFilterSelectionStage<T> Where<TKey>(Expression<Func<T, TKey>> keySelector, Comparator comparator, string value);
        IFilterSelectionStage<T> Where<TKey>(Expression<Func<T, TKey>> keySelector, Comparator comparator, decimal value);
        IFilterSelectionStage<T> Where<TKey>(Expression<Func<T, TKey>> keySelector, Comparator comparator, int value);
        IFilterSelectionStage<T> Where<TKey>(Expression<Func<T, TKey>> keySelector, Comparator comparator, DateTime value);
        ISortSelectionStage<T> OrderBy<TKey>(Expression<Func<T, TKey>> keySelector);
        ISortSelectionStage<T> OrderByDescending<TKey>(Expression<Func<T, TKey>> keySelector);
        Task<List<T>> Get();
    }

    public interface ISortSelectionStage<T> where T : BusinessObject
    {
        ISortSelectionStage<T> OrderBy<TKey>(Expression<Func<T, TKey>> keySelector);
        ISortSelectionStage<T> OrderByDescending<TKey>(Expression<Func<T, TKey>> keySelector);
        Task<List<T>> Get();
    }

    #endregion

}
