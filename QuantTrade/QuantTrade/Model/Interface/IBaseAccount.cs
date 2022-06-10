using System.Threading.Tasks;

namespace QuantTrade.Model.Interface
{
    public interface IBaseAccount
    {
        void UpdateHoldings();
        Task<bool> UpdateAccounts();
    }
}