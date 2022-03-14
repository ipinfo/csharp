using System.Text;
using System.Text.Json;
using System.Collections;
using System.Globalization;
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

        /// <summary>
        /// Replaces template parameters in the given url.
        /// </summary>
        /// <param name="queryBuilder">The queryBuilder to replace the template parameters.</param>
        /// <param name="parameters">The parameters to replace in the url.</param>
        public static void AppendUrlWithTemplateParameters(StringBuilder queryBuilder, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            // TODO: May need to trim down this function.
            
            // perform parameter validation
            if (queryBuilder == null)
            {
                throw new ArgumentNullException("queryBuilder");
            }

            if (parameters == null)
            {
                return;
            }

            // iterate and replace parameters
            foreach (KeyValuePair<string, object> pair in parameters)
            {
                string replaceValue = string.Empty;

                // load element value as string
                if (pair.Value == null)
                {
                    replaceValue = string.Empty;
                }
                else
                {
                    replaceValue = pair.Value.ToString();
                }

                replaceValue = Uri.EscapeDataString(replaceValue);

                // find the template parameter and replace it with its value
                queryBuilder.Replace(string.Format("{{{0}}}", pair.Key), replaceValue);
            }
        }        
    }
}