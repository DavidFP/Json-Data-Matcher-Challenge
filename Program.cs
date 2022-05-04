using System;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json.Linq;

namespace JsonDataMatcherChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            string endPoint = "https://coderbyte.com/api/challenges/json/age-counting";

            var request = WebRequest.Create(endPoint);
            var response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {

                Stream stream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(stream, System.Text.Encoding.UTF8);
                var jObject = JObject.Parse(streamReader.ReadToEnd());
                var data = jObject["data"].ToString();

                var ages = data.Replace(" ","").Split(",").Where(d=>d.Contains("age=",StringComparison.InvariantCultureIgnoreCase))
                .Select(s=> int.Parse(s.Replace("age=","")));

                int greater50 = ages.Count(age => age>50);

                Console.WriteLine($"OriginalJson: {jObject}");

                Console.WriteLine($"There are {greater50} greater than 50");
            }
            response.Close();
        }
    }
}
