using System;
using System.Collections.Generic;
using System.Text;

namespace RestUtils
{
    public class UrlUtils
    {
        public static string CreateUrl(string basicurl, IDictionary<string, string> values)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(basicurl);
            int index = 0;

            if (values != null)
                foreach (KeyValuePair<string, string> value in values)
                {
                    if (index == 0)
                        sb.Append($"?{value.Key}={value.Value}");
                    else
                        sb.Append($"&?{value.Key}={value.Value}");

                    index++;
                }

            return sb.ToString();
        }
    }
}
