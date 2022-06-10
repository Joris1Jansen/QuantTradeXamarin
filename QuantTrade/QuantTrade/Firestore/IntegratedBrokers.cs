using System.Collections.Generic;
using System.Threading.Tasks;
using QuantTrade.Interface;
using QuantTrade.Model;
using Xamarin.Forms;

namespace QuantTrade
{
    public class IntegratedBrokers
    {
        private static IIntegratedBrokers integratedBrokers = DependencyService.Get<IIntegratedBrokers>();
        
        public static async Task<List<Broker>> Read()
        {
            return await integratedBrokers.Read();
        }
    }
}