using System.Collections.Generic;
using System.Threading.Tasks;
using QuantTrade.Model;

namespace QuantTrade.Interface
{
    public interface IIntegratedBrokers
    {
        Task<List<Broker>> Read();
    }
}