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
                new(false, actual.IsEU),
                new("🇺🇸", actual.CountryFlag.Emoji),
                new("U+1F1FA U+1F1F8", actual.CountryFlag.Unicode),
                new("USD", actual.CountryCurrency.Code),
                new("$", actual.CountryCurrency.Symbol),
                new("NA", actual.Continent.Code),
                new("North America", actual.Continent.Name),
                new("America/Los_Angeles", actual.Timezone),
                new("", actual.Privacy.Service),
                new(5, actual.Domains.Domains.Count),
            };
            Assert.All(expectations, pair => Assert.Equal(pair.Item1, pair.Item2));
            Assert.False(actual.Privacy.Proxy);
            Assert.False(actual.Privacy.Vpn);
            Assert.False(actual.Privacy.Tor);
            Assert.False(actual.Privacy.Relay);
            Assert.True(actual.Privacy.Hosting);            
        }

        [Fact]
        public void TestBogonIPV4()
        {
            string ip = "127.0.0.1";
            IPinfoClient client = new IPinfoClient.Builder()
                .AccessToken(Environment.GetEnvironmentVariable("IPINFO_TOKEN"))
                .Build();
            
            IPResponse actual = client.IPApi.GetDetails(ip);

            Assert.Equal("127.0.0.1", actual.IP);
            Assert.True(actual.Bogon);            
        }

        [Fact]
        public void TestBogonIPV6()
        {
            string ip = "2001:0:c000:200::0:255:1";
            IPinfoClient client = new IPinfoClient.Builder()
                .AccessToken(Environment.GetEnvironmentVariable("IPINFO_TOKEN"))
                .Build();
            
            IPResponse actual = client.IPApi.GetDetails(ip);

            Assert.Equal("2001:0:c000:200::0:255:1", actual.IP);
            Assert.True(actual.Bogon);            
        }

        [Fact]
        public void TestNonBogonIPV4()
        {
            string ip = "1.1.1.1";
            IPinfoClient client = new IPinfoClient.Builder()
                .AccessToken(Environment.GetEnvironmentVariable("IPINFO_TOKEN"))
                .Build();
            
            IPResponse actual = client.IPApi.GetDetails(ip);

            Assert.Equal("1.1.1.1", actual.IP);
            Assert.False(actual.Bogon);            
        }

        [Fact]
        public void TestNonBogonIPV6()
        {
            string ip = "2a03:2880:f10a:83:face:b00c:0:25de";
            IPinfoClient client = new IPinfoClient.Builder()
                .AccessToken(Environment.GetEnvironmentVariable("IPINFO_TOKEN"))
                .Build();
            
            IPResponse actual = client.IPApi.GetDetails(ip);

            Assert.Equal("2a03:2880:f10a:83:face:b00c:0:25de", actual.IP);
            Assert.False(actual.Bogon);            
        }
    }
}
