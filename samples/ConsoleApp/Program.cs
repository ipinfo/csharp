using IPinfo;
using IPinfo.Models;
using IPinfo.Cache;

namespace ConsoleApp
{
  class Program
  {
    static async Task Main(string[] args)
    {
        // TODO: Add sample code for using IPinfoClient
        Console.WriteLine("Proper sample needs to be added.");
        
        string? token = Environment.GetEnvironmentVariable("IPINFO_TOKEN");
        if(token is not null)
        {
          string ip = "209.85.231.104";
          IPinfoClient client = new IPinfoClient.Builder()
            .AccessToken(token)
            .Cache(new CacheWraper(
              new CacheConfigurations
              {
                CacheTTL = 5
              }
            ))
            .HttpClientConfig(config => config
              .HttpClientInstance(new HttpClient())
              .Timeout(TimeSpan.FromSeconds(2)))
            .Build();
          int quit = 0;

          Console.WriteLine("\nOptions:\n-Enter any number to continue\n-Enter 1 to quit");
          quit = Convert.ToInt32(Console.ReadLine());
          while(quit!=1)
          {
            IPResponse ipResponse = await client.IPApi.GetDetailsAsync(ip);
            Console.WriteLine($"IPResponse.City: {ipResponse.City}");
            Console.WriteLine($"IPResponse.Company.Name: {ipResponse.Company.Name}");
            Console.WriteLine($"IPResponse.Country: {ipResponse.Country}");
            Console.WriteLine($"IPResponse.CountryName: {ipResponse.CountryName}");
            quit = Convert.ToInt32(Console.ReadLine());
          }
        }
        else
        {
          Console.WriteLine("Add access to the IPINFO_TOKEN in environment variables in order to run this sample code. You can also set your token in the code manually.");
          return;
        }
    }
  }
}