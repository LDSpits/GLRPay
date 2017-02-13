using NFC7;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GLRPay_OplaadStation.pages
{
    /// <summary>
    /// Interaction logic for PinPage.xaml
    /// </summary>
    public partial class PinPage : Page
    {
        private Product currentProduct;
        private GLRPayCard currentReciever;
        private GLRPayCard currentPayer;


        public PinPage(Product Product, GLRPayCard Reciever) : base()
        {
            currentPayer = new GLRPayCard();
            currentPayer.HasCardInserted += CurrentPayer_HasCardInserted;
            currentProduct = Product;
            currentReciever = Reciever;

            InitializeComponent();
            txtProductName.Text = "product: " + Product.Name;
            txtProductPrice.Text = "Price: " + Product.Price;

        }

        private void CurrentPayer_HasCardInserted(object sender, EventArgs e)
        {
            if (currentPayer.CardValid) {
                Application.Current.Dispatcher.Invoke(new Action(() => {
                    Frame ViewFrame = (Frame)Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive).FindName("PageViewer");
                    ViewFrame.Content = new ConfirmTransaction(currentProduct, currentReciever, currentPayer);
                }));
            } 
        }
    }
}
