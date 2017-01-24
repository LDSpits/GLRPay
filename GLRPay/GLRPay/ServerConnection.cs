using GLRPay.Data;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Text;

namespace GLRPay
{
    class ServerConnection
    {
        private const string URL = "http://www.nfc1.ict-lab.nl/index.php";

        public bool TransferMoney(string from, string to, float amount)
        {
            TransferData information = new TransferData(from,to,amount);

            System.Diagnostics.Debug.WriteLine(information.JSON);

            HttpWebRequest request = WebRequest.CreateHttp(URL);
            request.Method = "POST";
            request.ContentType = "application/json";

            byte[] data = Encoding.Default.GetBytes(information.JSON);

            using (Stream webstream = request.GetRequestStream()) {
                webstream.Write(data, 0, data.Length);
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if(response.StatusCode != HttpStatusCode.OK) {
                System.Diagnostics.Debug.WriteLine("the request failed");
                return false;
            }
            else {
                System.Diagnostics.Debug.WriteLine("the request was sucessfull");
                return true;
            }
        }
    }

}
