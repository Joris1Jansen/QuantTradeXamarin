using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using QuantTrade.Model.Interface;
using SQLite;

namespace QuantTrade.Model
{
    public class BaseAccount
    {
        [PrimaryKey, AutoIncrement]
        public string Id { get; set; }
        
        public BrokersEnum Broker { get; set; }
        
        public string Username { get; set; }
        
        public string Description { get; set; }
        
        public string UserId { get; set; }
        
        public string PublicKey { get; set; }
        
        public string PrivateKey { get; set; }
        
        public ObservableCollection<BaseHolding> Holdings { get; set; }
        
        public DateTime UpdatedAt { get; set; }
        
        public DateTime CreatedAt { get; set;  }
        
        public int AmountOfHoldings
        {
            get
            {
                return Holdings.Count;
            }
        }

        public decimal CurrentValue
        {
            get
            {
                var currVal = (decimal)0;
                foreach (var holding in Holdings)
                {
                    currVal += holding.Worth;
                }

                return currVal;
            }
        }
        
        public BaseAccount(string username, string description, string publicKey, string privateKey, DateTime createdAt, BrokersEnum broker)
        {
            Username = username;
            Description = description;
            PublicKey = publicKey;
            PrivateKey = privateKey;
            Holdings = new ObservableCollection<BaseHolding>();
            UpdatedAt = DateTime.Now;
            CreatedAt = createdAt;
            Broker = broker;
        }
        
        public virtual void UpdateHoldings()
        {
            return;
        }
    }
}