using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UetGrade.RequestUtility
{
    public class ChatfuelRequest : Request
    {
        private const string botId = "5be006d00ecd9f0a1bfbc06a";
        private const string token = "mELtlMAHYqR0BvgEiMq8zVek3uYUK3OJMbtyrdNPTrQB9ndV0fM7lWTFZbM4MZvD";
        public ChatfuelRequest()
        {
            client = new RestClient("https://api.chatfuel.com/")
            {
                Timeout = TimeoutRequest,
                ReadWriteTimeout = TimeoutRequest,
                Encoding = Encoding.UTF8,
                CookieContainer = new CookieContainer()
            };
        }

        public static bool Post( string messageId, string block, string content = null, params Atrribute[] atrributes)
        {
            ChatfuelRequest request = new ChatfuelRequest();
            return request.PostRequest(botId, messageId, token, block, MakeContent(content == null ? string.Empty : content), atrributes);
        }

        private static string MakeContent(string content)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(new { Content = content });
        }

        private bool PostRequest(string botId, string messageId, string token, string block, string json = null, params Atrribute[] atrributes)
        {
            string path = GetPath(botId, messageId, token, block, atrributes);
            var req = CreateRequest($"https://api.chatfuel.com/{path}", Method.POST, PostType.json,body: json);
            var res = ExcuteRequest(req);
            if (res == null) return false;
            if (res.Content.Contains("ok") && res.Content.Contains("true"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        

        private static string GetPath(string botId, string messageId, string token, string block, params Atrribute[] atrributes)
        {
            string result = string.Format($"bots/{botId}/users/{messageId}/send?chatfuel_token={token}&chatfuel_block_name={block}");
            if (atrributes == null) return result;
            foreach (var item in atrributes)
            {
                result += $"&{item.Name}={item.Value}";
            }
            return result;
        }
    }

    public class Atrribute
    {
        public string Name { get; set; }
        public string Value { get; set; }

    }
}
