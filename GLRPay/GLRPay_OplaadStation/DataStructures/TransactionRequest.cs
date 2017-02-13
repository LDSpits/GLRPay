using System;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;

namespace GLRPay_OplaadStation
{
    public class ListTransactionRequest
    {
        public string JSON { get; private set; }

        public ListTransactionRequest(string of)
        {
            if (string.IsNullOrWhiteSpace(of))
                throw new ArgumentNullException("the owners specified were null");

            JObject jsonObject = new JObject();
            jsonObject.Add("op", (int)ServerOperation.LogRequest);

            JArray array = new JArray();
            array.Add(GetSHA256Hash(of));

            jsonObject.Add("data", array);
            JSON = jsonObject.ToString();
        }

        private string GetSHA256Hash(string Data)
        {
            SHA256Managed crypt = new SHA256Managed();
            StringBuilder hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(Data), 0, Encoding.UTF8.GetByteCount(Data));
            foreach (byte theByte in crypto) {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
