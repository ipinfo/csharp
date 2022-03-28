using System;
using System.Collections.Generic;
using Xunit;

using IPinfo.Models;

namespace IPinfo.Tests
{
    public class IPApiTest
    {
        [Fact]
        public void TestGetDetails()
        {
            string ip = "8.8.8.8";
            IPinfoClient client = new IPinfoClient.Builder()
            .AccessToken(Environment.GetEnvironmentVariable("IPINFO_TOKEN"))
            .Build();
            
            IPResponse actual = client.IPApi.GetDetails(ip);
            
            var expectations = new List<Tuple<object, object>>()
            {
                new("8.8.8.8", actual.IP),
                new("dns.google", actual.Hostname),
                new("Mountain View", actual.City),
                new("California", actual.Region),
                new("US", actual.Country),
                new("United States", actual.CountryName),
                new("America/Los_Angeles", actual.Timezone),
                new("", actual.Privacy.Service),
                new(5, actual.Domains.Domains.Count),
            };
            Assert.All(expectations, pair => Assert.Equal(pair.Item1, pair.Item2));
            Assert.False(actual.Privacy.Proxy);
            Assert.False(actual.Privacy.Vpn);
            Assert.False(actual.Privacy.Tor);
            Assert.False(actual.Privacy.Relay);
            Assert.False(actual.Privacy.Hosting);            
        }
    }
}