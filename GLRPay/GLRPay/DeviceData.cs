using System;
using System.Text;

namespace GLRPayMachine
{
    public class DeviceData
    {
        public string UniqueID { get; private set; }
        public DeviceType Device { get; private set; } = DeviceType.Card;
        public IntentType Intent { get; private set; }

        public DeviceData(byte[] RawDeviceType, byte[] RawStationMode,string UID)
        {
            UniqueID = UID;
            int Devicenum = 0;
            if(int.TryParse(Encoding.UTF8.GetString(RawDeviceType),out Devicenum))
            {
                Device = (DeviceType)Devicenum;
            }else
            {
                throw new InvalidCastException("the device type was not a number!");
            }
            

            if(Device == DeviceType.Smartphone)
            {
                int intentnum = 0;
                if(int.TryParse(Encoding.UTF8.GetString(RawDeviceType), out intentnum))
                {
                    Intent = (IntentType)intentnum;
                }else
                {
                    throw new InvalidCastException("the intent was not a number!");
                }
                
            }
        }
    }
}
