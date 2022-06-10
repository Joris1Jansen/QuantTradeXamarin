using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Gms.Tasks;
using Android.Runtime;
using Firebase.Firestore;
using Java.Util;
using QuantTrade.Firestore.Interface;
using QuantTrade.Model;
using Xamarin.Forms;
using ArrayList = Java.Util.ArrayList;
using Object = Java.Lang.Object;
using Task = Android.Gms.Tasks.Task;

[assembly: Dependency(typeof(QuantTrade.Droid.Dependency.Firestore.FireCBAccount))]
namespace QuantTrade.Droid.Dependency.Firestore
{
    public class FireCBAccount : Object, IFireCBAccount, IOnCompleteListener
    {
        List<CBAccount> cbAccounts;
        private bool hasReadCbAccounts = false;
        
        public FireCBAccount()
        {
            cbAccounts = new List<CBAccount>();
        }
        
        public bool Insert(CBAccount cbAccount)
        {
            try
            {
                ArrayList holdings = new ArrayList();
                
                foreach (CBHolding holding in cbAccount.Holdings)
                {
                    HashMap holdingDoc = new HashMap();
                    holdingDoc.Put("amount", holding.Amount.ToString());
                    holdingDoc.Put("currency", holding.Currency);
                    holdingDoc.Put("currentPrice", holding.CurrentPrice.ToString());
                    holdingDoc.Put("walletId", holding.WalletId);
                    holdings.Add(holdingDoc);
                }
                
                var cbDoc = new Dictionary<string, Object>
                {
                    { "userId", Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid },
                    { "username", cbAccount.Username },
                    { "holdings", holdings},
                    { "description", cbAccount.Description },
                    { "publicKey", cbAccount.PublicKey },
                    { "privateKey", cbAccount.PrivateKey },
                    { "updatedAt", cbAccount.UpdatedAt.ToString() },
                    { "createdAt", cbAccount.CreatedAt.ToString() },
                    { "broker", cbAccount.Broker.ToString() },
                };
                
                var collection = FirebaseFirestore.Instance.Collection("Coinbase");
                collection.Add(new HashMap(cbDoc));

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public Task<bool> Delete(CBAccount cbAccount)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> Update(CBAccount cbAccount)
        {
            try
            {
                ArrayList holdings = new ArrayList();
                
                foreach (CBHolding holding in cbAccount.Holdings)
                {
                    HashMap holdingDoc = new HashMap();
                    holdingDoc.Put("amount", holding.Amount.ToString());
                    holdingDoc.Put("currency", holding.Currency);
                    holdingDoc.Put("currentPrice", holding.CurrentPrice.ToString());
                    holdingDoc.Put("walletId", holding.WalletId);
                    holdings.Add(holdingDoc);
                }
                
                // FirebaseFirestore.Instance.Collection("Coinbase").Document(cbAccount.Id).Update("holdings", holdings);

                var coinbaseDoc = new Dictionary<string, Object>
                {
                    { "userId", Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid },
                    { "description", cbAccount.Description },
                    { "publicKey", cbAccount.PublicKey },
                    { "privateKey", cbAccount.PrivateKey },
                    { "updatedAt", cbAccount.UpdatedAt.ToString()},
                    { "holdings", holdings }
                };
                var collection = FirebaseFirestore.Instance.Collection("Coinbase");
                collection.Document(cbAccount.Id).Update(coinbaseDoc);
                
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<CBAccount>> Read()
        {
            try
            {
                var collection = FirebaseFirestore.Instance.Collection("Coinbase");
                var query = collection.WhereEqualTo("userId", Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid);
                query.Get().AddOnCompleteListener(this);

                for (int i = 0; i < 30; i++)
                {
                    if (hasReadCbAccounts)
                    {
                        break;
                    }

                    await System.Threading.Tasks.Task.Delay(100);
                }

                return cbAccounts;
            }
            catch (Exception ex)
            {
                return cbAccounts;
            }
        }

        public void OnComplete(Task task)
        {
            if(task.IsSuccessful)
            {
                cbAccounts.Clear();
                var documents = (QuerySnapshot)task.Result;

                foreach (var doc in documents.Documents)
                {
                    CBAccount newAccount = new CBAccount(
                        doc.Get("username").ToString(),
                        doc.Get("description").ToString(),
                        doc.Get("publicKey").ToString(),
                        doc.Get("privateKey").ToString(),
                        DateTime.Parse(doc.Get("createdAt").ToString()),
                        Enum.Parse<BrokersEnum>(doc.Get("broker").ToString()))
                    {
                        UserId = doc.Get("userId").ToString(),
                        Id = doc.Id,
                    };
                    var holdings = doc.Get("holdings") != null ? doc.Get("holdings") : null;

                    if (holdings != null)
                    {
                        var holdingsMap = new JavaList<Object>(holdings.Handle,
                            JniHandleOwnership.DoNotRegister);
                        foreach (var holding in holdingsMap)
                        {
                            var holdingDict = new JavaDictionary<string, string>(holding.Handle, JniHandleOwnership.DoNotRegister);
                            CBHolding newHolding = new CBHolding(holdingDict["walletId"], Convert.ToDecimal(holdingDict["amount"]),
                                holdingDict["currency"], Convert.ToDecimal(holdingDict["currentPrice"]));

                            newAccount.Holdings.Add(newHolding);
                        }
                    }
                    cbAccounts.Add(newAccount);
                }
            }
            else
            {
                cbAccounts.Clear();
            }
            hasReadCbAccounts = true;
        }
    }
}