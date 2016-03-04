# WoLib
A simple library for wakeing machines over LAN written in C#

# How to use
At first you have to know your devices ip and mac address. With these information you can create an WoLWake object to wake your device over LAN.

  string ip = "192.168.0.9";

  EthernetMacAddress mac_address = new EthernetMacAddress(0x00, 0x00, 0x00, 0x00, 0x00, 0x00);

  WoLWake device_one = new WoLWake(ip, mac_address);

  device_one.Wake();
  
your device schould have been turned on. If not check your PCI-E settings.

# More information
More information about "Wake on LAN" can be found in the corresponding wiki:

https://en.wikipedia.org/wiki/Wake-on-LAN //English

https://de.wikipedia.org/wiki/Wake_On_LAN //German
