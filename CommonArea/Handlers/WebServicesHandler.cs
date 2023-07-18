using CommonArea.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CommonArea.Handlers
{
    public static class WebServicesHandler
    {
        public static async Task<string> CallWebService(Uri requestUri, 
            string jsonRequest,
            WebServiceMethod webServiceMethod,
            Dictionary<string, object>? extraHeaders = null
            )
        {
            try
            {
                var webRequest = WebRequest.Create(requestUri);
                webRequest.Method = webServiceMethod.ToString();
                webRequest.ContentType = "application/json";

                if (extraHeaders != null)
                {
                    foreach (var header in extraHeaders)
                    {
                        webRequest.Headers.Add(header.Key.ToString(), header.Value.ToString());
                    }
                }
                
                using (var writer = new StreamWriter(webRequest.GetRequestStream()))
                {
                    writer.WriteLine(jsonRequest);
                }
                var webResponse = webRequest.GetResponse();
                var responseString = new StreamReader(webResponse.GetResponseStream()).ReadToEnd();

                return await Task.FromResult(responseString.ToString());
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return null;
            }

        }
    }
}
