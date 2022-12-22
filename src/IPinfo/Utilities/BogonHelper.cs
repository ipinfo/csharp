using System;
using System.Net;

namespace IPinfo.Utilities
{
    /// <summary>
    /// BogonHelper class contains bogon helper methods.
    /// </summary>
    internal static class BogonHelper
    {
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
