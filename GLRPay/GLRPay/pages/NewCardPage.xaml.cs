using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NFC7;

namespace GLRPay.pages
{
    /// <summary>
    /// Interaction logic for NewCardPage.xaml
    /// </summary>
    public partial class NewCardPage : Page
    {
        private NFC7handler NFCReader;

        public NewCardPage()
        {
            InitializeComponent();
            NFCReader = new NFC7handler();
            NFCReader.debug = true;
        }

        private void Button_Yes(object sender, RoutedEventArgs e)
        {
            //if(NFCReader.Status == "connected")
            if(NFCReader.WriteBlockText(1, 0, "0a8a35a8fe49788a")) {
                System.Diagnostics.Debug.WriteLine("writing id failed!");
            }
            NFCReader.WriteBlockText(1, 2, "0.00");
            Application.Current.Dispatcher.Invoke(new Action(() => {
                Frame ViewFrame = (Frame)Window.GetWindow(this).FindName("PageViewer");
                ViewFrame.Content = new SetupPage();
            }));
        }

        private void Button_No(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(new Action(() => {
                Frame ViewFrame = (Frame)Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive).FindName("PageViewer");
                ViewFrame.Content = new SetupPage();
            }));
        }
    }
}
