using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace IPinfo.Http.Client
{
    
    public class HttpClientWrapper
    {

        private HttpClient client;

        public HttpClientWrapper(HttpClient httpClient)
        {
            this.client = httpClient;
        }

        public async Task sendRequest()
        {
            //TODO: just making plain request, need to change it

            try	
            {
                HttpResponseMessage response = await client.GetAsync("http://ipinfo.io/json");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                
                Console.WriteLine(responseBody);
            }
            catch(HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");	
                Console.WriteLine("Message :{0} ",e.Message);
            }   
        }
    }
}