using NFC7;

namespace GLRPay
{
    public class GLRPayCard
    {
        const string ValidGeneralId = "0a8a35a8fe49788a";

        private NFC7handler NFCreader;

        public string Identifier { get; private set; }

        public GLRPayCard()
        {
            NFCreader = new NFC7handler();
            Identifier = NFCreader.UID;
        }

        public bool CheckValid()
        {
            string cardId = StringConverter(NFCreader.ReadBlockText(1, 0));
            if (string.IsNullOrWhiteSpace(cardId)) {
                return false;
            }

            return Validate(cardId);
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

        private string ByteConverter(byte[] RawData)
        {
            return System.Text.Encoding.UTF8.GetString(RawData);
        }

        // function to check if card is supported
        private bool Validate(string GeneralID)
        {
            if (GeneralID == ValidGeneralId) {
                return true;
            }
            else { return false; }
        }
    }
}