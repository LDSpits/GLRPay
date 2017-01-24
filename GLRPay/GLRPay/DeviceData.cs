using System;
using System.Text;

namespace GLRPayMachine
{
    public class DeviceData
    {
        public string UniqueID { get; private set; }
        public int Balance { get; set; } = 0;

        public DeviceData(string UID)
        {
            UniqueID = UID;
        }
    }
}
