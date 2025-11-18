using System;
using System.Collections.Generic;
using Xunit;

using IPinfo.Models;

namespace IPinfo.Tests
{
    public class IPApiPlusTest
    {
        [Fact]
        public void TestGetDetailsIPV4()
        {
            string ip = "8.8.8.8";
            IPinfoClientPlus client = new IPinfoClientPlus.Builder()
                .AccessToken(Environment.GetEnvironmentVariable("IPINFO_TOKEN"))
                .Build();

            IPResponsePlus actual = client.IPApi.GetDetails(ip);

            Assert.Equal("8.8.8.8", actual.IP);
            Assert.Equal("dns.google", actual.Hostname);
            Assert.False(actual.Bogon);

            // Geo assertions
            Assert.NotNull(actual.Geo);
            Assert.NotNull(actual.Geo.City);
            Assert.NotNull(actual.Geo.Region);
            Assert.NotNull(actual.Geo.RegionCode);
            Assert.Equal("US", actual.Geo.CountryCode);
            Assert.Equal("United States", actual.Geo.Country);
            Assert.Equal("United States", actual.Geo.CountryName);
            Assert.False(actual.Geo.IsEU);
            Assert.NotNull(actual.Geo.Continent);
            Assert.NotNull(actual.Geo.ContinentCode);
            Assert.NotEqual(0, actual.Geo.Latitude);
            Assert.NotEqual(0, actual.Geo.Longitude);
            Assert.NotNull(actual.Geo.Timezone);
            Assert.NotNull(actual.Geo.PostalCode);
            Assert.Equal("🇺🇸", actual.Geo.CountryFlag.Emoji);
            Assert.Equal("U+1F1FA U+1F1F8", actual.Geo.CountryFlag.Unicode);
            Assert.Equal("https://cdn.ipinfo.io/static/images/countries-flags/US.svg", actual.Geo.CountryFlagURL);
            Assert.Equal("USD", actual.Geo.CountryCurrency.Code);
            Assert.Equal("$", actual.Geo.CountryCurrency.Symbol);
            Assert.Equal("NA", actual.Geo.ContinentInfo.Code);
            Assert.Equal("North America", actual.Geo.ContinentInfo.Name);

            // AS assertions
            Assert.NotNull(actual.As);
            Assert.Equal("AS15169", actual.As.Asn);
            Assert.NotNull(actual.As.Name);
            Assert.NotNull(actual.As.Domain);
            Assert.NotNull(actual.As.Type);

            // Network flags
            Assert.False(actual.IsAnonymous);
            Assert.True(actual.IsAnycast);
            Assert.True(actual.IsHosting);
            Assert.False(actual.IsMobile);
            Assert.False(actual.IsSatellite);

            // Plus-specific fields (may be present based on token tier)
            // These fields exist in the response structure
        }

        [Fact]
        public void TestGetDetailsIPV6()
        {
            string ip = "2001:4860:4860::8888";
            IPinfoClientPlus client = new IPinfoClientPlus.Builder()
                .AccessToken(Environment.GetEnvironmentVariable("IPINFO_TOKEN"))
                .Build();

            IPResponsePlus actual = client.IPApi.GetDetails(ip);

            Assert.Equal("2001:4860:4860::8888", actual.IP);

            // Geo assertions
            Assert.NotNull(actual.Geo);
            Assert.Equal("US", actual.Geo.CountryCode);
            Assert.Equal("United States", actual.Geo.Country);
            Assert.NotNull(actual.Geo.City);
            Assert.NotNull(actual.Geo.Region);

            // AS assertions
            Assert.NotNull(actual.As);
            Assert.NotNull(actual.As.Asn);
            Assert.NotNull(actual.As.Name);
            Assert.NotNull(actual.As.Domain);

            // Network flags
            Assert.False(actual.IsAnonymous);
            Assert.False(actual.IsMobile);
            Assert.False(actual.IsSatellite);
        }

        [Fact]
        public void TestBogonIPV4()
        {
            string ip = "127.0.0.1";
            IPinfoClientPlus client = new IPinfoClientPlus.Builder()
                .AccessToken(Environment.GetEnvironmentVariable("IPINFO_TOKEN"))
                .Build();

            IPResponsePlus actual = client.IPApi.GetDetails(ip);

            Assert.Equal("127.0.0.1", actual.IP);
            Assert.True(actual.Bogon);
        }

        [Fact]
        public void TestBogonIPV6()
        {
            string ip = "2001:0:c000:200::0:255:1";
            IPinfoClientPlus client = new IPinfoClientPlus.Builder()
                .AccessToken(Environment.GetEnvironmentVariable("IPINFO_TOKEN"))
                .Build();

            IPResponsePlus actual = client.IPApi.GetDetails(ip);

            Assert.Equal("2001:0:c000:200::0:255:1", actual.IP);
            Assert.True(actual.Bogon);
        }
    }
}
