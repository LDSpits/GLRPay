using System.ComponentModel;

namespace GLRPay
{
    public class Product : INotifyPropertyChanged
    {
        public float Price { get; private set; }
        public string Name { get; private set; }

        public Product(float ProductPrice, string ProductName)
        {
            Price = ProductPrice;
            Name = ProductName;
        }

        // Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
