using System.Collections.Generic;
using System.Threading.Tasks;
using QuantTrade.Model;

namespace QuantTrade.Firestore.Interface
{
    public interface IFireCBAccount
    {
        bool Insert(CBAccount cbAccount);
        Task<bool> Delete(CBAccount cbAccount);
        Task<bool> Update(CBAccount cbAccount);
        Task<List<CBAccount>> Read();
    }
}