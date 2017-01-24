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

namespace GLRPay.pages
{
    /// <summary>
    /// Interaction logic for ConfirmTransaction.xaml
    /// </summary>
    public partial class ConfirmTransaction : Page
    {
        private ServerConnection connection;
        GLRPayCard reciever;
        Product product;

        public ConfirmTransaction(Product pr,GLRPayCard reciever) : base()
        {
            connection = new ServerConnection();
            InitializeComponent();

            product = pr;
            this.reciever = reciever;
        }

        private void Button_Yes(object sender, RoutedEventArgs e)
        {
            GLRPayCard payerCard = new GLRPayCard();
            connection.TransferMoney(payerCard.Identifier, reciever.Identifier, product.Price);
        }

        private void Button_No(object sender, RoutedEventArgs e)
        {

        }
    }
}
