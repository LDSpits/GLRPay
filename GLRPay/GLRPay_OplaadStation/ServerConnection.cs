using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json.Linq;

namespace GLRPay_OplaadStation
{
    class ServerConnection
    {
        private const string URL = "http://www.nfc1.ict-lab.nl/index.php";

        public bool LoadCard(string to, float amount)
        {
            ChargeData information;

            try {
                information = new ChargeData(to, amount);
            }
            catch (ArgumentNullException) {
                System.Diagnostics.Debug.WriteLine("the from or to was null");
                return false;
            }

            HttpWebRequest request = WebRequest.CreateHttp(URL);
            request.Method = "POST";
            request.ContentType = "application/json";

            using (StreamWriter writer = new StreamWriter(request.GetRequestStream())) {
                writer.WriteLine(information.JSON);
            }

            try {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                using(StreamReader reader = new StreamReader(response.GetResponseStream())) {
                    System.Diagnostics.Debug.WriteLine(reader.ReadToEnd());
                }
                return true;
            }
            catch(WebException ex) {
#if DEBUG
                System.Diagnostics.Debug.WriteLine("begin writing response");

                using (StreamReader reader = new StreamReader(ex.Response.GetResponseStream())) {
                    System.Diagnostics.Debug.WriteLine(reader.ReadToEnd());
                }

                System.Diagnostics.Debug.WriteLine("end writing response");
#endif
                return false;
            }
            
        }

        //pas de functie aan om de server endpoinjt te gebruiken
        public ObservableCollection<TransactionRow> GetUserTransactions(string User)
        {
            ListTransactionRequest opdracht;

            ObservableCollection<TransactionRow> returndata = new ObservableCollection<TransactionRow>();

            try {
                opdracht = new ListTransactionRequest(User);
            }
            catch {
                throw;
            }

            HttpWebRequest request = WebRequest.CreateHttp(URL);
            request.Method = "POST";
            request.ContentType = "application/json";

            System.Diagnostics.Debug.WriteLine(opdracht.JSON);

            using (StreamWriter writer = new StreamWriter(request.GetRequestStream())) {
                writer.WriteLine(opdracht.JSON);
            }

            string RawJsonData;
            try {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                using (StreamReader reader = new StreamReader(response.GetResponseStream())) {
                    RawJsonData = reader.ReadToEnd();
                    System.Diagnostics.Debug.WriteLine(RawJsonData);
                }
            }
            catch (WebException ex) {
#if DEBUG
                using (StreamReader reader = new StreamReader(ex.Response.GetResponseStream())) {
                    System.Diagnostics.Debug.WriteLine(reader.ReadToEnd());
                }
#endif
                throw ex;
            }


            JArray JsonArray = JArray.Parse(RawJsonData);

            foreach(JToken row in JsonArray) {
                System.Diagnostics.Debug.WriteLine(row.ToString());
                returndata.Add(new TransactionRow(DateTime.Now, float.Parse(row["0"].ToString())/100));
            }

            return returndata;

        }
    }

}
