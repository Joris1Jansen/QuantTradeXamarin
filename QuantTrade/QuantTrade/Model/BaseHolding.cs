using SQLite;

namespace QuantTrade.Model
{
    public class BaseHolding
    {
        [PrimaryKey, AutoIncrement]
        public string Id { get; set; }
        
        public decimal Amount { get; set; }
        
        public string Currency { get; set; }
        
        public decimal CurrentPrice { get; set; }

        public decimal Worth
        {
            get
            {
                return Amount * CurrentPrice;
            }
        }
            
        public BaseHolding(decimal amount, string currency, decimal currentPrice)
        {
            Amount = amount;
            Currency = currency;
            CurrentPrice = currentPrice;
        }
    }
}