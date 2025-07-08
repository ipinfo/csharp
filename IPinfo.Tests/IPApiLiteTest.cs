using System;
using System.Collections.Generic;
using Xunit;

using IPinfo.Models;

namespace IPinfo.Tests
{
    public class IPApiLiteTest
    {
        [Fact]
        public void TestGetDetails()
        {
            string ip = "8.8.8.8";
            IPinfoClientLite client = new IPinfoClientLite.Builder()
                .AccessToken(Environment.GetEnvironmentVariable("IPINFO_TOKEN"))
                .Build();

            IPResponseLite actual = client.IPApi.GetDetails(ip);

            var expectations = new List<Tuple<object, object>>()
            {
                new("8.8.8.8", actual.IP),
                new("AS15169", actual.Asn),
                new("Google LLC", actual.AsName),
                new("google.com", actual.AsDomain),
                new("US", actual.CountryCode),
                new("United States", actual.Country),
                new("United States", actual.CountryName),
                new(false, actual.IsEU),
                new("🇺🇸", actual.CountryFlag.Emoji),
                new("U+1F1FA U+1F1F8", actual.CountryFlag.Unicode),
                new("https://cdn.ipinfo.io/static/images/countries-flags/US.svg", actual.CountryFlagURL),
                new("USD", actual.CountryCurrency.Code),
                new("$", actual.CountryCurrency.Symbol),
                new("NA", actual.Continent.Code),
                new("North America", actual.Continent.Name),
            };
            Assert.All(expectations, pair => Assert.Equal(pair.Item1, pair.Item2));
        }

        [Fact]
        public void TestGetDetailsIPV6()
        {
            string ip = "2001:4860:4860::8888";
            IPinfoClientLite client = new IPinfoClientLite.Builder()
                .AccessToken(Environment.GetEnvironmentVariable("IPINFO_TOKEN"))
                .Build();

            IPResponseLite actual = client.IPApi.GetDetails(ip);

            var expectations = new List<Tuple<object, object>>()
            {
                new("2001:4860:4860::8888", actual.IP),
                new("AS15169", actual.Asn),
                new("Google LLC", actual.AsName),
                new("google.com", actual.AsDomain),
                new("US", actual.CountryCode),
                new("United States", actual.Country),
                new("United States", actual.CountryName),
                new(false, actual.IsEU),
                new("🇺🇸", actual.CountryFlag.Emoji),
                new("U+1F1FA U+1F1F8", actual.CountryFlag.Unicode),
                new("https://cdn.ipinfo.io/static/images/countries-flags/US.svg", actual.CountryFlagURL),
                new("USD", actual.CountryCurrency.Code),
                new("$", actual.CountryCurrency.Symbol),
                new("NA", actual.Continent.Code),
                new("North America", actual.Continent.Name),
            };
            Assert.All(expectations, pair => Assert.Equal(pair.Item1, pair.Item2));
        }

        [Fact]
        public void TestGetDetailsCurrentIP()
        {
            IPinfoClientLite client = new IPinfoClientLite.Builder()
                .AccessToken(Environment.GetEnvironmentVariable("IPINFO_TOKEN"))
                .Build();

            IPResponseLite actual = client.IPApi.GetDetails();

            Assert.NotNull(actual.IP);
            Assert.NotNull(actual.Asn);
            Assert.NotNull(actual.AsName);
            Assert.NotNull(actual.AsDomain);
            Assert.NotNull(actual.CountryCode);
            Assert.NotNull(actual.Country);
            Assert.NotNull(actual.CountryName);
            Assert.NotNull(actual.CountryFlag);
            Assert.NotNull(actual.CountryFlag.Emoji);
            Assert.NotNull(actual.CountryFlag.Unicode);
            Assert.NotNull(actual.CountryFlagURL);
            Assert.NotNull(actual.CountryCurrency);
            Assert.NotNull(actual.Continent);
        }

        [Fact]
        public void TestBogonIPV4()
        {
            string ip = "127.0.0.1";
            IPinfoClientLite client = new IPinfoClientLite.Builder()
                .AccessToken(Environment.GetEnvironmentVariable("IPINFO_TOKEN"))
                .Build();

            IPResponseLite actual = client.IPApi.GetDetails(ip);

            Assert.Equal("127.0.0.1", actual.IP);
            Assert.True(actual.Bogon);
        }

        [Fact]
        public void TestBogonIPV6()
        {
            string ip = "2001:0:c000:200::0:255:1";
            IPinfoClientLite client = new IPinfoClientLite.Builder()
                .AccessToken(Environment.GetEnvironmentVariable("IPINFO_TOKEN"))
                .Build();

            IPResponseLite actual = client.IPApi.GetDetails(ip);

            Assert.Equal("2001:0:c000:200::0:255:1", actual.IP);
            Assert.True(actual.Bogon);
        }

        [Fact]
        public void TestNonBogonIPV4()
        {
            string ip = "1.1.1.1";
            IPinfoClientLite client = new IPinfoClientLite.Builder()
                .AccessToken(Environment.GetEnvironmentVariable("IPINFO_TOKEN"))
                .Build();

            IPResponseLite actual = client.IPApi.GetDetails(ip);

            Assert.Equal("1.1.1.1", actual.IP);
            Assert.False(actual.Bogon);
        }

        [Fact]
        public void TestNonBogonIPV6()
        {
            string ip = "2a03:2880:f10a:83:face:b00c:0:25de";
            IPinfoClientLite client = new IPinfoClientLite.Builder()
                .AccessToken(Environment.GetEnvironmentVariable("IPINFO_TOKEN"))
                .Build();

            IPResponseLite actual = client.IPApi.GetDetails(ip);

            Assert.Equal("2a03:2880:f10a:83:face:b00c:0:25de", actual.IP);
            Assert.False(actual.Bogon);
        }
    }
}
