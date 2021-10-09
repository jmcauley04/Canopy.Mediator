using Canopy.Provider.Models;
using System.Threading.Tasks;

namespace Canopy.Provider.Interfaces
{
    #region fluent interfaces

    public interface ICanopyRetrieveService<T> where T : BusinessObject
    {
        IGetStage<T> WithCode(int code);
        IGetStage<T> WithGtin(string gtin);
        IGetStage<T> WithUUID(string uuid);
    }

    public interface IGetStage<T> where T : BusinessObject
    {
        Task<T> Get();
    }

    #endregion
}
