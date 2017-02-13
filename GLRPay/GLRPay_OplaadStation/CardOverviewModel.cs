using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GLRPay_OplaadStation
{
    class CardOverviewModel : ObservableObject
    {
        private GLRPayCard card;
        private ServerConnection conn;

        private ObservableCollection<TransactionRow> transactions = new ObservableCollection<TransactionRow>();
        public ObservableCollection<TransactionRow> Transactions {
            get {
                return transactions;
            }

            set {
                transactions = value;
                RaisePropertychanged();
            }
        } 
        private float amount = 0.0f;
        public float Amount {
            get { return amount; }
            set {
                amount = value;
                System.Diagnostics.Debug.WriteLine("Amount Updated");
                RaisePropertychanged();
            }
        }

        private string id;
        public string Id {
            get {
                return id;
            }

            set {
                id = value;
                System.Diagnostics.Debug.WriteLine("Id Updated");
                RaisePropertychanged();
            }
        }

        public ICommand AddToCard { get; set; }

        public CardOverviewModel()
        {
            AddToCard = new DelegateCommand(ChargeCard,CardIsPresent);

            card = new GLRPayCard();
            card.CardInserted += Card_WasCardInserted;
            card.CardRemoved += Card_WasCardRemoved;
            if (card.IsPresent) {
                Id = card.Identifier;
                Amount = card.Amount;
            }
            conn = new ServerConnection();
        }

        private void ChargeCard(object param)
        {
            float result;
            if(float.TryParse(param as string,out result)) {
                if (conn.LoadCard(card.Identifier, result)) {
                    if (card.AddAmount(result)) {
                        Amount = card.Amount;
                    }
                }
            }
        }

        private bool CardIsPresent(object param)
        {
            if (card.IsPresent)
                return true;
            return false;
        }

        private void Card_WasCardRemoved(object sender, EventArgs e)
        {
            Id = "";
            Amount = 0.0f;
            Transactions = new ObservableCollection<TransactionRow>();
            AddToCard.CanExecute(null);
        }

        private void Card_WasCardInserted(object sender, EventArgs e)
        {
            Id = card.Identifier;
            Amount = card.Amount;
            Transactions = conn.GetUserTransactions(card.Identifier);
            AddToCard.CanExecute(null);
        }
    }
}
