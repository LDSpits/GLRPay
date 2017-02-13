using NFC7;
using System;

namespace GLRPay_OplaadStation
{
    public class GLRPayCard 
    {
        const string ValidGeneralId = "0a8a35a8fe49788a";

        private NFC7handler NFCreader;

        public string Identifier { get; private set; } = "";
        public float Amount { get; private set; } = 0.0f;
        public bool CardValid { get; private set; } = false;
        public bool IsPresent { get; private set; } = false;

        public event EventHandler CardInserted;
        public event EventHandler CardRemoved;

        private void CardWasRemoved(EventArgs arg)
        {
            CardRemoved?.Invoke(this, arg);
        }

        private void CardWasInserted(EventArgs arg)
        {
            CardInserted?.Invoke(this, arg);
        }

        public GLRPayCard()
        {
            NFCreader = new NFC7handler();
            NFCreader.CardInsertEvent += NFCreader_CardInsertEvent;
            NFCreader.CardRemoveEvent += NFCreader_CardRemoveEvent;
        }

        private void NFCreader_CardRemoveEvent(object sender, EventArgs args)
        {
            IsPresent = false;
            CardWasRemoved(EventArgs.Empty);
        }

        private void NFCreader_CardInsertEvent(object sender, EventArgs args)
        {
            IsPresent = true;
            //er is een nieuwe kaart gevonden
            //check of de kaart correct is
            CardValid = CheckValid();
            //zet de unique identifier van deze kaart
            Identifier = NFCreader.UID;
            //laat iedereen weten dat deze kaart gevuld is
            if (CardValid) {
                Amount = RetrieveAmount();
            }
            CardWasInserted(EventArgs.Empty);
        }

        public bool CheckValid()
        {
            string cardId = StringConverter(NFCreader.ReadBlockText(1, 0));
            if (string.IsNullOrWhiteSpace(cardId)) {
                return false;
            }
            if (cardId == ValidGeneralId) {
                return true;
            }
            else { return false; }
        }

        public bool AddAmount(float Amount)
        {
            if (!CardValid || !IsPresent) return false;

            if(NFCreader.WriteBlockText(1, 2, (this.Amount + Amount).ToString())) {
                this.Amount += Amount;
                return true;
            }
            return false;
        }

        private float RetrieveAmount()
        {
            string h = StringConverter(NFCreader.ReadBlockText(1, 2));
            return float.Parse(h);
        }

        // convert a stringarray to a single string
        private string StringConverter(string[] StringArray)
        {
            string FullString = "";
            foreach (string element in StringArray) {
                FullString += element;
            }
            return FullString;
        }
    }
}