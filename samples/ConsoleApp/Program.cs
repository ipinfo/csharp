﻿using System;
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


        string ip = "110.39.13.197";
        HttpClientWrapper httpClient = new HttpClientWrapper(new HttpClient());
        await httpClient.sendRequest(ip);
    }
  }
}