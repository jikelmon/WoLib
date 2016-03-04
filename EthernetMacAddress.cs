using System;

namespace WoLib
{
    public class EthernetMacAddress
    {
        public byte[] mac_address
        {
            get; private set;
        }

        public EthernetMacAddress(byte a, byte b, byte c, byte d, byte e, byte f)
        {
            mac_address = new byte[6];
            mac_address[0] = a;
            mac_address[1] = b;
            mac_address[2] = c;
            mac_address[3] = d;
            mac_address[4] = e;
            mac_address[5] = f;            
        }

        public EthernetMacAddress(byte[] hex_address)
        {
            if (hex_address.Length > 6)
            {
                throw new Exception("Hex address too long! Has to be six bytes!");
            }else if (hex_address.Length < 6)
            {
                throw new Exception("Hex address too short! Has to be six bytes!");
            }

            mac_address = hex_address;
        }

        public override string ToString()
        {
            string info = "";

            foreach (byte b in mac_address)
            {
                info += b.ToString("x") + ":";
            }

            info = info.Remove(info.Length-1);

            return info;
        }
    }
}
