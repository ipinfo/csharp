# [<img src="https://ipinfo.io/static/ipinfo-small.svg" alt="IPinfo" width="24"/>](https://ipinfo.io/) IPinfo C# Client Library

TODO: Intro needs update
This is supposed to be the new C# client library for the IPinfo.io IP address API. Existing sdk can be accessed at [ipinfo/csharp](https://github.com/ipinfo/csharp).

This allows you to lookup your own IP address, or get any of the following details for an IP:

 - [IP geolocation / geoIP data](https://ipinfo.io/ip-geolocation-api) (city, region, country, postal code, latitude and longitude)
 - [ASN details](https://ipinfo.io/asn-api) (ISP or network operator, associated domain name, and type, such as business, hosting or company)
 - [Firmographics data](https://ipinfo.io/ip-company-api) (the name and domain of the business that uses the IP address)
 - [Carrier information](https://ipinfo.io/ip-carrier-api) (the name of the mobile carrier and MNC and MCC for that carrier if the IP is used exclusively for mobile traffic)

## Getting Started

You'll need an IPinfo API access token, which you can get by singing up for a free account at [https://ipinfo.io/signup](https://ipinfo.io/signup).

The free plan is limited to 50,000 requests per month, and doesn't include some of the data fields such as IP type and company data. To enable all the data fields and additional request volumes see [https://ipinfo.io/pricing](https://ipinfo.io/pricing)

## TODO: Nuget


### Installation

This package can be installed using Nuget. TODO: Add updated details below.

```bash
dotnet add package IPinfo
```

### Quick Start

```csharp
// namespace
using IPinfo;
using IPinfo.Models;
```

```csharp
// initializing IPinfo client
string token = "your_token_string";
IPinfoClient client = new IPinfoClient.Builder()
    .AccessToken(token)
    .Build();
```

## TODO: Usage

```csharp
// making API call
string ip = "216.239.36.21";
IPResponse ipResponse = await client.IPApi.GetDetailsAsync(ip);
```

```csharp
// accessing details
Console.WriteLine($"IPResponse.IP: {ipResponse.IP}");
Console.WriteLine($"IPResponse.City: {ipResponse.City}");
Console.WriteLine($"IPResponse.Company.Name: {ipResponse.Company.Name}");
Console.WriteLine($"IPResponse.Country: {ipResponse.Country}");
Console.WriteLine($"IPResponse.CountryName: {ipResponse.CountryName}");
```

## TODO: Batch API


## TODO: Privacy Detection API


## TODO: Live Example


## Other Libraries

There are official [IPinfo client libraries](https://ipinfo.io/developers/libraries) available for many languages including PHP, Go, Java, Ruby, and many popular frameworks such as Django, Rails and Laravel. There are also many third party libraries and integrations available for our API.

## About IPinfo

Founded in 2013, IPinfo prides itself on being the most reliable, accurate, and in-depth source of IP address data available anywhere. We process terabytes of data to produce our custom IP geolocation, company, carrier, VPN detection, hosted domains, and IP type data sets. Our API handles over 40 billion requests a month for 100,000 businesses and developers.

[![image](https://avatars3.githubusercontent.com/u/15721521?s=128&u=7bb7dde5c4991335fb234e68a30971944abc6bf3&v=4)](https://ipinfo.io/)
