using NFC7;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GLRPay.pages
{
    /// <summary>
    /// Interaction logic for PinPage.xaml
    /// </summary>
    public partial class PinPage : Page
    {
        private NFC7handler NFCReader;
        private Product CurrentProduct;
        private GLRPayCard CurrentReciever;


        public PinPage(Product pr, GLRPayCard Cr) : base()
        {
            NFCReader = new NFC7handler();
            
            NFCReader.CardInsertEvent += NFCReader_CardInsertEvent;
            
            CurrentProduct = pr;
            CurrentReciever = Cr;
            InitializeComponent();
            txtProductName.Text = "product: " + pr.Name;
            txtProductPrice.Text = "Price: " + pr.Price;

        }

        private void NFCReader_CardInsertEvent(object sender, EventArgs args)
        {
            Application.Current.Dispatcher.Invoke(new Action(() => {
                Frame ViewFrame = (Frame)Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive).FindName("PageViewer");
                ViewFrame.Content = new ConfirmTransaction(CurrentProduct,CurrentReciever);
            }));
        }
    }
}
