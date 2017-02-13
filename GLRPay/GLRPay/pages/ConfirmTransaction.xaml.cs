using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GLRPay_OplaadStation.pages
{
    /// <summary>
    /// Interaction logic for ConfirmTransaction.xaml
    /// </summary>
    public partial class ConfirmTransaction : Page
    {
        private ServerConnection connection;
        GLRPayCard reciever;
        GLRPayCard payer;
        Product product;

        public ConfirmTransaction(Product Product,GLRPayCard Reciever, GLRPayCard Payer) : base()
        {
            if (Product.Name != null) 
                product = Product;
            else 
                throw new ArgumentNullException("the argument given is null");

            if (!Reciever.CardValid) 
                throw new ArgumentException("the reciever card is not valid!");

            if (!Payer.CardValid)
                throw new NullReferenceException("the payer card object is not valid!");

            connection = new ServerConnection();

            reciever = Reciever;
            payer = Payer;

            InitializeComponent();
        }

        private void Button_Yes(object sender, RoutedEventArgs e)
        {
            if (connection.TransferMoney(payer.Identifier, reciever.Identifier, product.Price)) {
                Application.Current.Dispatcher.Invoke(new Action(() => {
                    Frame ViewFrame = (Frame)Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive).FindName("PageViewer");
                    ViewFrame.Content = new PinPage(product, reciever);
                }));
            }
            else {

            }
        }

        private void Button_No(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(new Action(() => {
                Frame ViewFrame = (Frame)Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive).FindName("PageViewer");
                ViewFrame.Content = new PinPage(product, reciever);
            }));
        }
    }
}
