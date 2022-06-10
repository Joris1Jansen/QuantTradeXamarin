using System.Collections.ObjectModel;
using System.ComponentModel;
using QuantTrade.Model;
using QuantTrade.Utils;
using SQLite;

namespace QuantTrade.ViewModel
{
    public class AddBrokerVM : INotifyPropertyChanged
    {
        public ObservableCollection<Broker> Brokers { get; set; }
        
        public event PropertyChangedEventHandler PropertyChanged;

        private bool isCoinbase;
        public bool IsCoinbase
        {
            get { return isCoinbase; }
            set
            {
                isCoinbase = value;
                OnPropertyChanged("IsCoinbase");
            }
        }
        
        private bool isBinance;
        public bool IsBinance
        {
            get { return isBinance; }
            set
            {
                isBinance = value;
                OnPropertyChanged("IsBinance");
            }
        }
        
        private Broker selectedBroker;

        public Broker SelectedBroker
        {
            get { return selectedBroker; }
            set
            {
                selectedBroker = value;
                IsCoinbase = FormValidationUtil.WhichBroker("Coinbase", SelectedBroker.Name);
                IsBinance = FormValidationUtil.WhichBroker("Binance", SelectedBroker.Name);
            }
        }

        public AddBrokerVM()
        {
            Brokers = new ObservableCollection<Broker>();
        }
        
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public async void GetBrokers()
        {
            Brokers.Clear();
            var brokers = await IntegratedBrokers.Read();

            foreach (var broker in brokers)
            {
                Brokers.Add(broker);
            }
        }
    }
    
}