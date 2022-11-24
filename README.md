# <a href="https://ipinfo.io/"><img src="https://raw.githubusercontent.com/ipinfo/csharp/main/src/IPinfo/icon.png" alt="IPinfo" width="24" /></a> IPinfo C# .NET SDK

[![License](http://img.shields.io/:license-apache-blue.svg)](LICENSE)
[![NuGet](https://img.shields.io/nuget/dt/IPinfo.svg?style=flat-square&label=IPinfo)](https://www.nuget.org/packages/IPinfo/)

This is the official C# .NET SDK for the [IPinfo.io](https://ipinfo.io) IP address API, allowing you to lookup your own IP address, or get any of the following details for other IP addresses:

 - [IP geolocation](https://ipinfo.io/ip-geolocation-api) (city, region, country, postal code, latitude and longitude)
 - [ASN details](https://ipinfo.io/asn-api) (ISP or network operator, associated domain name, and type, such as business, hosting or company)
 - [Firmographics data](https://ipinfo.io/ip-company-api) (the name and domain of the business that uses the IP address)
 - [Carrier information](https://ipinfo.io/ip-carrier-api) (the name of the mobile carrier and MNC and MCC for that carrier if the IP is used exclusively for mobile traffic)

## Getting Started

You'll need an IPinfo API access token, which you can get by singing up for a free account at [https://ipinfo.io/signup](https://ipinfo.io/signup).

The free plan is limited to 50,000 requests per month, and doesn't include some of the data fields such as IP type and company data. To enable all the data fields and additional request volumes see [https://ipinfo.io/pricing](https://ipinfo.io/pricing)

### Installation

This package can be installed from NuGet.

##### Install using Package Manager

```bash
Install-Package IPinfo
```

##### Install using the dotnet CLI

```bash
dotnet add package IPinfo
```

##### Install with NuGet.exe

```bash
nuget install IPinfo
```

### Quick Start

```csharp
// namespace
using IPinfo;
using IPinfo.Models;
```

```csharp
// initializing IPinfo client
string token = "MY_TOKEN";
IPinfoClient client = new IPinfoClient.Builder()
    .AccessToken(token)
    .Build();
```

### Usage

```csharp
// making API call
string ip = "216.239.36.21";
IPResponse ipResponse = await client.IPApi.GetDetailsAsync(ip);
```

```csharp
// accessing location details from response
Console.WriteLine($"IPResponse.IP: {ipResponse.IP}");
Console.WriteLine($"IPResponse.City: {ipResponse.City}");
Console.WriteLine($"IPResponse.Company.Name: {ipResponse.Company.Name}");
Console.WriteLine($"IPResponse.Country: {ipResponse.Country}");
Console.WriteLine($"IPResponse.CountryName: {ipResponse.CountryName}");
```

### Synchronous

```csharp
// making synchronous API call
string ip = "216.239.36.21";
IPResponse ipResponse = client.IPApi.GetDetails(ip);
```

### Country Name Lookup

`ipResponse.CountryName` will return the country name, whereas `ipResponse.Country` can be used to fetch the country code.

Additionally `ipResponse.IsEU` will return `true` if the country is a member of the European Union (EU), `response.CountryFlag` 
will return emoji and unicode of country's flag, `response.CountryCurrency` will return code and symbol of country's currency 
and `response.Continent` will return code and name of the continent.

```csharp
string ip = "1.1.1.1";

// making API call
IPResponse ipResponse = await client.IPApi.GetDetailsAsync(ip);

// country code, e.g. 'US'
Console.WriteLine($"IPResponse.Country: {ipResponse.Country}");

// country name, e.g. 'United States'
Console.WriteLine($"IPResponse.CountryName: {ipResponse.CountryName}");

// whether part of the EU, e.g. false
Console.WriteLine($"IPResponse.isEU: {ipResponse.isEU}");

// country flag emoji, e.g. "US" -> "🇺🇸"
Console.WriteLine($"IPResponse.CountryFlag.Emoji: {ipResponse.CountryFlag.Emoji}");

// country flag unicode, e.g. "US" -> "U+1F1FA U+1F1F8"
Console.WriteLine($"IPResponse.CountryFlag.Unicode: {ipResponse.CountryFlag.Unicode}");

// currency code, e.g. "US" -> "USD"
Console.WriteLine($"IPResponse.CountryCurrency.Code: {ipResponse.CountryCurrency.Code}");

// currency symbol, e.g. "US" -> "$"
Console.WriteLine($"IPResponse.CountryCurrency.Symbol: {ipResponse.CountryCurrency.Symbol}");

// continent code, e.g. "US" -> "NA"
Console.WriteLine($"IPResponse.Continent.Code: {ipResponse.Continent.Code}");

// continent name, e.g. "US" -> "North America"
Console.WriteLine($"IPResponse.Continent.Name: {ipResponse.Continent.Name}");
```

### Caching

In-memory caching of data is provided by default. Custom implementation of the cache can also be provided by implementing the `ICache` interface.

#### Modifying cache options

```csharp
// namespace
using IPinfo;
using IPinfo.Cache;
```

```csharp
long cacheEntryTimeToLiveInSeconds = 2*60*60*24; // 2 days
int cacheSizeMbs = 2;
IPinfoClient client = new IPinfoClient.Builder()
    .AccessToken(token) // pass your token string
    .Cache(new CacheWrapper(cacheConfig => cacheConfig
        .CacheMaxMbs(cacheSizeMbs) // pass cache size in mbs
        .CacheTtl(cacheEntryTimeToLiveInSeconds))) // pass time to live in seconds for cache entry
    .Build();
```

### Bogon Filtering

The `Bogon` property of the `IPResponse` object can be used to check if an IP address is a bogon.

```csharp
// namespace
using IPinfo;
using IPinfo.Models;
```

```csharp
string ip = "127.0.0.1";
IPResponse ipResponse = await client.IPApi.GetDetailsAsync(ip);
if (ipResponse.Bogon)
{
    Console.WriteLine($"{ipResponse.IP} is a bogon.");   
}
else
{
    // display ip details
    Console.WriteLine($"IPResponse.IP: {ipResponse.IP}");
    Console.WriteLine($"IPResponse.City: {ipResponse.City}");
    Console.WriteLine($"IPResponse.CountryName: {ipResponse.CountryName}");
}
```

### Thread Safety

This library is thread safe when using default components.

If you decide to replace the cache implementation with your own, you must guarantee thread safety within that library in regards to cache manipulations.

### Samples

[Sample codes](https://github.com/ipinfo/csharp/tree/main/samples) are also available.

## Other Libraries

There are official [IPinfo client libraries](https://ipinfo.io/developers/libraries) available for many languages including PHP, Go, Java, Ruby, and many popular frameworks such as Django, Rails and Laravel. There are also many third party libraries and integrations available for our API.

## About IPinfo

Founded in 2013, IPinfo prides itself on being the most reliable, accurate, and in-depth source of IP address data available anywhere. We process terabytes of data to produce our custom IP geolocation, company, carrier, VPN detection, hosted domains, and IP type data sets. Our API handles over 40 billion requests a month for 100,000 businesses and developers.

[![image](https://avatars3.githubusercontent.com/u/15721521?s=128&u=7bb7dde5c4991335fb234e68a30971944abc6bf3&v=4)](https://ipinfo.io/)
