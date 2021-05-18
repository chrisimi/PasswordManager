using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Public
{
    internal class RestHelper
    {
        internal static async Task<T> PostData<T>(string url, IDictionary<string, string> values)
        {
            try
            { 
                int index = 0;

                if (values != null)
                    foreach (KeyValuePair<string, string> value in values)
                    {
                        if (index == 0)
                            url += $"?{value.Key}={value.Value}";
                        else
                            url += $"&?{value.Key}={value.Value}";

                        index++;
                    }

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("User-Agent", "MusicPlatform REST Client");

                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
                //requestMessage.Content = new ByteArrayContent(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(objBody)));
                //requestMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage msg = await client.SendAsync(requestMessage);

                string content = await msg.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(content);

                //var stringTask = client.GetStringAsync(url);
                //string data = await stringTask;

                //T obj = JsonSerializer.Deserialize<T>(data, new JsonSerializerOptions()
                //{
                //    PropertyNameCaseInsensitive = true
                //});

                //return obj;
            }
            catch (Exception e)
            {
                Debug.WriteLine("ERROR: " + e.Message);
                Debug.WriteLine($"Error happended when trying to reach {url}");
                return default;
            }
        }

        internal static async Task<T> GetData<T>(string url, IDictionary<string, string> values)
        {
            try
            {
                int index = 0;
                if (values != null)
                    foreach (KeyValuePair<string, string> value in values)
                    {
                        if (index == 0)
                            url += $"?{value.Key}={value.Value}";
                        else
                            url += $"&?{value.Key}={value.Value}";

                        index++;
                    }

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("User-Agent", "MusicPlatform REST Client");

                var stringTask = client.GetStringAsync(url);
                string data = await stringTask;

                Debug.WriteLine("data: " + data);

                if (string.IsNullOrWhiteSpace(data))
                    return default;

                T obj = JsonConvert.DeserializeObject<T>(data);

                return obj;
            }
            catch (Exception e)
            {
                Debug.WriteLine("ERROR: " + e.Message);
                Debug.WriteLine($"Error happended when trying to reach {url}");
                return default;
            }
        }
    }
}
