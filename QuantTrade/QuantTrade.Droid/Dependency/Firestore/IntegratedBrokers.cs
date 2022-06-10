using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Gms.Extensions;
using Android.Gms.Tasks;
using Task = Android.Gms.Tasks.Task;
using Firebase.Firestore;
using QuantTrade.Interface;
using Xamarin.Forms;
using QuantTrade.Model;

[assembly: Dependency(typeof(QuantTrade.Droid.Dependency.Firestore.IntegratedBrokers))]
namespace QuantTrade.Droid.Dependency.Firestore
{
    public class IntegratedBrokers : Java.Lang.Object, IIntegratedBrokers, IOnCompleteListener
    {
        private List<Broker> integratedBrokers;
        private bool hasReadIntegratedBrokers = false;
    
        public IntegratedBrokers()
        {
            integratedBrokers = new List<Broker>();
        }
        
        public void OnComplete(Task task)
        {
            if(task.IsSuccessful)
            {
                var documents = (QuerySnapshot)task.Result;
    
                integratedBrokers.Clear();
                foreach(var doc in documents.Documents)
                {
                    Broker newBroker = new Broker()
                    {
                        Name = doc.Get("name").ToString()
                    };
    
                    integratedBrokers.Add(newBroker);
                }
            }
            else
            {
                integratedBrokers.Clear();
            }
    
            hasReadIntegratedBrokers = true;
        }
        
        public async Task<List<Broker>> Read()
        {
            try
            {
                hasReadIntegratedBrokers = false;
                var collection = FirebaseFirestore.Instance.Collection("IntegratedBrokers");
                await collection.Get().AddOnCompleteListener(this);
    
                for (int i = 0; i < 30; i++)
                {
                    if (hasReadIntegratedBrokers)
                    {
                        break;
                    }
    
                    await System.Threading.Tasks.Task.Delay(100);
                }
    
                return integratedBrokers;
            }
            catch (Exception ex)
            {
                return integratedBrokers;
            }
        }
    }
}