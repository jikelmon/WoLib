using System;
using System.Net.Sockets;

namespace WoLib
{
    public class WoLWake
    {
        public EthernetMacAddress device_mac_address { get; private set; }
        public byte[] magic_packet {get; private set;}     
        public string ip { get; set; }  
        public string broadcast_address { get; private set; }

        //Constructor with EthernetMacAddress object
        public WoLWake(string ip, EthernetMacAddress device_mac_address)
        {
            this.device_mac_address = device_mac_address;
            this.ip = ip;
            broadcast_address = IpToBraodcastAddress(this.ip);

            magic_packet = new byte[102];

            //Fill in six times 0xFF
            for (int i = 0; i < 6; i++)
            {
                magic_packet[i] = 0xff;
            }

            //Fill in 16x ethernet mac address
            for (int i = 0; i < 16; i++)
            {
                magic_packet[i * 6 + 6] = device_mac_address.mac_address[0];
                magic_packet[i * 6 + 7] = device_mac_address.mac_address[1];
                magic_packet[i * 6 + 8] = device_mac_address.mac_address[2];
                magic_packet[i * 6 + 9] = device_mac_address.mac_address[3];
                magic_packet[i * 6 + 10] = device_mac_address.mac_address[4];
                magic_packet[i * 6 + 11] = device_mac_address.mac_address[5];
            }            
        }

        //Constructor with mac address as byte array
        public WoLWake(string ip, byte[] hex_address)
        {
            if (hex_address.Length > 6)
            {
                throw new Exception("Hex address too long! Has to be six bytes!");
            }
            else if (hex_address.Length < 6)
            {
                throw new Exception("Hex address too short! Has to be six bytes!");
            }

            device_mac_address = new EthernetMacAddress(hex_address);
            this.ip = ip;
            broadcast_address = IpToBraodcastAddress(this.ip);

            magic_packet = new byte[102];

            //Fill in six times 0xFF
            for (int i = 0; i < 6; i++)
            {
                magic_packet[i] = 0xff;
            }

            //Fill in 16x ethernet mac address
            for (int i = 0; i < 16; i++)
            {
                magic_packet[i * 6 + 6] = hex_address[0];
                magic_packet[i * 6 + 7] = hex_address[1];
                magic_packet[i * 6 + 8] = hex_address[2];
                magic_packet[i * 6 + 9] = hex_address[3];
                magic_packet[i * 6 + 10] = hex_address[4];
                magic_packet[i * 6 + 11] = hex_address[5];
            }
        }

        //Constructor with mac address as single bytes
        public WoLWake(string ip, byte a, byte b, byte c, byte d, byte e, byte f)
        {
            device_mac_address = new EthernetMacAddress(a,b,c,d,e,f);
            this.ip = ip;
            broadcast_address = IpToBraodcastAddress(this.ip);

            magic_packet = new byte[102];

            //Fill in six times 0xFF
            for (int i = 0; i < 6; i++)
            {
                magic_packet[i] = 0xff;
            }

            //Fill in 16x ethernet mac address
            for (int i = 0; i < 16; i++)
            {
                magic_packet[i * 6 + 6] = a;
                magic_packet[i * 6 + 7] = b;
                magic_packet[i * 6 + 8] = c;
                magic_packet[i * 6 + 9] = d;
                magic_packet[i * 6 + 10] = e;
                magic_packet[i * 6 + 11] = f;
            }
        }

        public void Wake()
        {
            UdpClient s = new UdpClient(broadcast_address, 49153);
            s.Send(magic_packet, magic_packet.Length);
            s.Close();
        }

        private string IpToBraodcastAddress(string ip)
        {
            string broadcast_address = "";
            int last_dot = ip.LastIndexOf(".");
            broadcast_address = ip.Remove(last_dot + 1) + "255";
            return broadcast_address;
        }
    }
}
