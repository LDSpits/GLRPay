using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLRPayMachine
{
    class GLRPayFunctions
    {
        Stationmode DeviceMode;
        private DeviceData Data;

        const string ValidGeneralId = "0a8a35a8fe49788a9c3d738411090ac0";
        bool validate = false;

        // function to check if card is supported
        private bool Validate(string[] RawGeneralID)
        {
            if (StringConverter(RawGeneralID) == ValidGeneralId)
            {
                return true;
            }
            else { return false; }
        }

        // function to make sure te connection gets established
        private void Handshake(string MyUid, int MyStation, int ThereStation, string ThereUid)
        {

        }
        // function to receive currency
        private void Load()
        {

        }
        // function to send currency
        private void Pay()
        {

        }
        // function to upload to the database
        private void UploadDatabase()
        {

        }

        // convert a stringarray to a single string
        private string StringConverter(string[] StringArray)
        {
            string FullString = "";
            foreach (string element in StringArray)
            {
                FullString += element;
            }
            return FullString;
        }
    }

    public enum Stationmode
    {
        PayStation,
        LoadStation,
    }
    public enum IntentType
    {

        Pay,
        Charge
    }

    public enum DeviceType
    {
        Card,
        Smartphone
    }
}

