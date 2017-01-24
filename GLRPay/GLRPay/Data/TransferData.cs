using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace GLRPay.Data
{
    public class TransferData
    {
        public TransferData(string From, string To, float Amount)
        {
            JObject jsonObject = new JObject();
            jsonObject.Add("op", "transaction");
            jsonObject.Add("data", new JArray());

            JArray array = new JArray();
            array.Add(To);
            array.Add(From);
            array.Add(Amount);

            jsonObject.Add("Data",array);
            JSON = jsonObject.ToString();
        }

        public string op = "transaction";
        public string JSON { get; private set; }
    }
}
