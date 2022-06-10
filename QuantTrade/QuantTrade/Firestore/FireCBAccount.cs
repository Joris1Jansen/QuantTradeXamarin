using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuantTrade.Firestore.Interface;
using QuantTrade.Model;
using Xamarin.Forms;

namespace QuantTrade.Firestore
{
    public class FireCBAccount
    {
        private static IFireCBAccount fireCbAccount = DependencyService.Get<IFireCBAccount>();

        public static bool Insert(CBAccount cbAccount)
        {
            return fireCbAccount.Insert(cbAccount);
        }
        
        public static async Task<bool> Update(CBAccount cbAccount)
        {
            return await fireCbAccount.Update(cbAccount);
        }
        
        public static async Task<bool> Delete(CBAccount cbAccount)
        {
            return await fireCbAccount.Delete(cbAccount);
        }
        
        public static async Task<List<CBAccount>> Read()
        {
            return await fireCbAccount.Read();
        }
    }
}