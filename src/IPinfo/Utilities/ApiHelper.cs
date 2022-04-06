using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace IPinfo.Utilities
{
    /// <summary>
    /// ApiHelper class contains a bunch of helper methods.
    /// </summary>
    internal static class ApiHelper
    {
        /// <summary>
        /// Runs asynchronous tasks synchronously and throws the first caught exception.
        /// </summary>
        /// <param name="t">The task to be run synchronously.</param>
        internal static void RunTaskSynchronously(Task t)
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
        internal static void AppendUrlWithTemplateParameters(StringBuilder queryBuilder, IEnumerable<KeyValuePair<string, object>> parameters)
        {
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

                // convert string to escaped representation, e.g. replace space with %20
                replaceValue = Uri.EscapeDataString(replaceValue);

                // find the template parameter and replace it with its value
                queryBuilder.Replace(string.Format("{{{0}}}", pair.Key), replaceValue);
            }
        }        
    }
}
