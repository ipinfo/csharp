using System.Text.Json;
using IPinfo.Models;

namespace IPinfo.Utilities
{
    
    public static class ApiHelper
    {
        
        // TODO: May need to optimize
        public static void RunTaskSynchronously(Task t)
        {
            try
            {
                t.Wait();
            }
            catch (AggregateException e)
            {
                if (e.InnerExceptions.Count > 0)
                {
                    throw e.InnerExceptions[0];
                }
                else
                {
                    throw;
                }
            }
        }        
    }
}