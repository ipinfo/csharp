using System;
using System.Net;

namespace IPinfo.Utilities
{
    /// <summary>
    /// BogonHelper class contains bogon helper methods.
    /// </summary>
    internal static class BogonHelper
    {
        private static readonly IPNetwork[] s_bogonNetworks =
        {
            // IPv4
            new IPNetwork("0.0.0.0/8"),
            new IPNetwork("10.0.0.0/8"),
            new IPNetwork("100.64.0.0/10"),
            new IPNetwork("127.0.0.0/8"),
            new IPNetwork("169.254.0.0/16"),
            new IPNetwork("172.16.0.0/12"),
            new IPNetwork("192.0.0.0/24"),
            new IPNetwork("192.0.2.0/24"),
            new IPNetwork("192.168.0.0/16"),
            new IPNetwork("198.18.0.0/15"),
            new IPNetwork("198.51.100.0/24"),
            new IPNetwork("203.0.113.0/24"),
            new IPNetwork("224.0.0.0/4"),
            new IPNetwork("240.0.0.0/4"),
            new IPNetwork("255.255.255.255/32"),
            // IPv6
            new IPNetwork("::/128"),
            new IPNetwork("::1/128"),
            new IPNetwork("::ffff:0:0/96"),
            new IPNetwork("::/96"),
            new IPNetwork("100::/64"),
            new IPNetwork("2001:10::/28"),
            new IPNetwork("2001:db8::/32"),
            new IPNetwork("fc00::/7"),
            new IPNetwork("fe80::/10"),
            new IPNetwork("fec0::/10"),
            new IPNetwork("ff00::/8"),
            // 6to4
            new IPNetwork("2002::/24"),
            new IPNetwork("2002:a00::/24"),
            new IPNetwork("2002:7f00::/24"),
            new IPNetwork("2002:a9fe::/32"),
            new IPNetwork("2002:ac10::/28"),
            new IPNetwork("2002:c000::/40"),
            new IPNetwork("2002:c000:200::/40"),
            new IPNetwork("2002:c0a8::/32"),
            new IPNetwork("2002:c612::/31"),
            new IPNetwork("2002:c633:6400::/40"),
            new IPNetwork("2002:cb00:7100::/40"),
            new IPNetwork("2002:e000::/20"),
            new IPNetwork("2002:f000::/20"),
            new IPNetwork("2002:ffff:ffff::/48"),
            // Teredo
            new IPNetwork("2001::/40"),
            new IPNetwork("2001:0:a00::/40"),
            new IPNetwork("2001:0:7f00::/40"),
            new IPNetwork("2001:0:a9fe::/48"),
            new IPNetwork("2001:0:ac10::/44"),
            new IPNetwork("2001:0:c000::/56"),
            new IPNetwork("2001:0:c000:200::/56"),
            new IPNetwork("2001:0:c0a8::/48"),
            new IPNetwork("2001:0:c612::/47"),
            new IPNetwork("2001:0:c633:6400::/56"),
            new IPNetwork("2001:0:cb00:7100::/56"),
            new IPNetwork("2001:0:e000::/36"),
            new IPNetwork("2001:0:f000::/36"),
            new IPNetwork("2001:0:ffff:ffff::/64")
        };

        internal static bool IsBogon(String ip)  {
            for (int i = 0; i < s_bogonNetworks.Length; i++)
            {
                IPNetwork bogonNetwork = s_bogonNetworks[i];
                if (bogonNetwork.Contains(ip))
                {
                    return true;
                }
            }
            return false;
        }

        private class IPNetwork
        {
            private int nMaskBits;
            private IPAddress netAddress;

            public IPNetwork(String ipAddress)
            {
                if (ipAddress.IndexOf('/') > 0)
                {
                    String[] addressAndMask = ipAddress.Split('/');
                    ipAddress = addressAndMask[0];
                    nMaskBits = Int32.Parse(addressAndMask[1]);
                }
                else {
                    nMaskBits = -1;
                }
                netAddress = IPAddress.Parse(ipAddress);
            }

            public bool Contains(String ipAddress)
            {
                try
                {
                    return Contains(IPAddress.Parse(ipAddress));
                }
                catch(Exception e)
                {
                    return false;
                }
            }

            public bool Contains(IPAddress ipAddress)
            {
                if (ipAddress == null || ipAddress.AddressFamily != netAddress.AddressFamily)
                {
                    return false;
                }

                if (nMaskBits < 0)
                {
                    return ipAddress.Equals(netAddress);
                }

                byte[] remAddr = ipAddress.GetAddressBytes();
                byte[] reqAddr = netAddress.GetAddressBytes();

                int nMaskFullBytes = nMaskBits / 8;
                byte finalByte = (byte) (0xFF00 >> (nMaskBits & 0x07));

                for (int i = 0; i < nMaskFullBytes; i++)
                {
                    if (remAddr[i] != reqAddr[i])
                    {
                        return false;
                    }
                }

                if (finalByte != 0)
                {
                    return (remAddr[nMaskFullBytes] & finalByte) == (reqAddr[nMaskFullBytes] & finalByte);
                }

                return true;
            }
        }
    }
}
