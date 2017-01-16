using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for InitPinPage.xaml
    /// </summary>
    public partial class InitPinPage : Page
    {
        NFC7handler reader = new NFC7handler();

        public InitPinPage()
        {
            InitializeComponent();
            reader.CardInsertEvent += Reader_CardInsertEvent;
        }

        private void Reader_CardInsertEvent(object sender, EventArgs args)
        {
            Application.Current.Dispatcher.Invoke(new Action(() => {
                Frame ViewFrame = (Frame)Window.GetWindow(this).FindName("PageViewer");
                ViewFrame.Content = new PinPage();
            }));
            
        }

        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }

        private void TextBoxPasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String))) {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsTextAllowed(text)) {
                    e.CancelCommand();
                }
            }
            else {
                e.CancelCommand();
            }
        }


    }
}
