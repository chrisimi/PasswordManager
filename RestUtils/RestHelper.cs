using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RestUtils
{
    public class RestHelper
    {
        /// <summary>
        /// post data to a REST-API-Endpoint
        /// </summary>
        /// <typeparam name="TResult">the object type of the result</typeparam>
        /// <typeparam name="TRequest">the object type of the request, can be the same as result</typeparam>
        /// <param name="url">the basic url to the endpoint</param>
        /// <param name="values">values of the url as dictionary -> { "id", 1 }</param>
        /// <param name="request">the object to send as a request, can be null</param>
        /// <returns></returns>
        public static async Task<TResult> PostData<TResult, TRequest>(string url, IDictionary<string, string> values, TRequest request)
        {
            try
            {
                url = UrlUtils.CreateUrl(url, values);

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("User-Agent", "MusicPlatform REST Client");

                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, url);

                if(request != null)
                {
                    requestMessage.Content = new ByteArrayContent(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(request)));
                    requestMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                }

                HttpResponseMessage msg = await client.SendAsync(requestMessage);

                string content = await msg.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<TResult>(content);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// get some data from a REST-API-Endpoint
        /// </summary>
        /// <typeparam name="TResult">the type of the result object</typeparam>
        /// <param name="url">the url of the end point</param>
        /// <param name="values">values of the url as dictionary -> { "id", 1 }</param>
        /// <returns></returns>
        public static async Task<TResult> GetData<TResult>(string url, IDictionary<string, string> values)
        {
            try
            {
                url = UrlUtils.CreateUrl(url, values);

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("User-Agent", "MusicPlatform REST Client");

                var stringTask = client.GetStringAsync(url);
                string data = await stringTask;

                if (string.IsNullOrWhiteSpace(data))
                    return default;

                return JsonConvert.DeserializeObject<TResult>(data);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
