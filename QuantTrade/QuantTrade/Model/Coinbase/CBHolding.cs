namespace QuantTrade.Model
{
    public class CBHolding : BaseHolding
    {
        public string WalletId { get; set; }
        public CBHolding(string walletId, decimal amount, string currency, decimal currentPrice) : base(amount, currency, currentPrice)
        {
            WalletId = walletId;
        }
    }
}