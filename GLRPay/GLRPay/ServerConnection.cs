using GLRPay_OplaadStation.Data;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace GLRPay_OplaadStation
{
    class ServerConnection
    {
        private const string URL = "http://www.nfc1.ict-lab.nl/index.php";

        public bool TransferMoney(string from, string to, float amount)
        {
            TransferData information;

            try {
                information = new TransferData(from, to, amount);
            }
            catch (ArgumentNullException) {
                System.Diagnostics.Debug.WriteLine("the from or to was null");
                return false;
            }

            System.Diagnostics.Debug.WriteLine(information.JSON);

            HttpWebRequest request = WebRequest.CreateHttp(URL);
            request.Method = "POST";
            request.ContentType = "application/json";

            byte[] data = Encoding.Default.GetBytes(information.JSON);

            using (Stream webstream = request.GetRequestStream()) {
                webstream.Write(data, 0, data.Length);
            }

            try {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                using(StreamReader reader = new StreamReader(response.GetResponseStream())) {
                    System.Diagnostics.Debug.WriteLine(reader.ReadToEnd());
                }
                return true;
            }
            catch (WebException ex){
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
    }

}
