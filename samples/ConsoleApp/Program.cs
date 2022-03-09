using System;
using System.Net;
using IPinfo.Http.Client;

namespace ConsoleApp
{
  class Program
  {
    static async Task Main(string[] args)
    {
        //TODO: Add sample code for using IPinfoClient
        Console.WriteLine("Proper sample needs to be added.");
        string? token = Environment.GetEnvironmentVariable("IPINFO_TOKEN");

        if(token is not null)
        {
          string ip = "209.85.231.104";
          HttpClientWrapper httpClient = new HttpClientWrapper(new HttpClient());
          await httpClient.sendRequest(token, ip);
        }
        else
        {
          Console.WriteLine("Add access to the IPINFO_TOKEN in environment variables in order to run this sample code. You can also set your token here manually.");
          return;
        }
    }
  }
}