using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UetGrade.RequestUtility
{
    public class BitlyRequest : Request
    {
        private const string token = "683d6f32483a3ee3cc8a142580eb2144288d767d";
        public BitlyRequest()
        {
            client = new RestClient("https://api-ssl.bitly.com/")
            {
                Timeout = TimeoutRequest,
                ReadWriteTimeout = TimeoutRequest,
                Encoding = Encoding.UTF8,
                CookieContainer = new CookieContainer()
            };
        }

        public static string Get(string link)
        {
            BitlyRequest request = new BitlyRequest();
            return request.GetLink(link);
        }
        private string GetLink(string link)
        {
            var path = GetPath(link);
            var req = CreateRequest(path, Method.GET);
            var res = ExcuteRequest(req);
            if (res == null) return string.Empty;
            return res.Content;
        }

        private string GetPath(string link)
        {
            return $"https://api-ssl.bitly.com/v3/shorten?format=txt&access_token={token}&longUrl={link}";
        }
    }
}
