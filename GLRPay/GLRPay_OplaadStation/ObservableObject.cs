using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GLRPay_OplaadStation
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertychanged([CallerMemberName]string Name = null)
        {
            if (Name == null) {
                System.Diagnostics.Debug.WriteLine("name was null");
                return;
            }

            if(PropertyChanged == null) {
                System.Diagnostics.Debug.WriteLine("propertychanged event was null");
            }
                

            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(Name));
        }
    }
}
