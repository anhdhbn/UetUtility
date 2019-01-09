using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UetGrade.RequestUtility
{
    public class GradeRequest : Request
    {
        public GradeRequest()
        {
            client = new RestClient("http://112.137.129.30/viewgrade/")
            {
                Timeout = TimeoutRequest,
                ReadWriteTimeout = TimeoutRequest,
                Encoding = Encoding.UTF8,
                CookieContainer = new CookieContainer()
            };
        }
        public List<List<string>> GetListGrade()
        {
            var req = CreateRequest("http://112.137.129.30/viewgrade/", Method.GET);
            var res = ExcuteRequest(req);
            if (res == null) return null;
            string token = Regex.Match(res.Content, @"_token"".*?""(.*?)""").Groups[1].Value;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("_token", token);
            dic.Add("username", "17020565");
            dic.Add("password", "Honganh99");
            var req2 = CreateRequest("http://112.137.129.30/viewgrade/submitLoginForm", Method.POST, PostType.form, dic);
            var res2 = ExcuteRequest(req2);
            if (res2 == null) return null;

            Dictionary<string, string> dic2 = new Dictionary<string, string>();
            dic2.Add("_token", token);
            dic2.Add("term", "66");
            dic2.Add("type_education", "0");
            var req3 = CreateRequest("http://112.137.129.30/viewgrade/home/getListSubjectOfTerm", Method.POST, PostType.form, dic2);
            var res3 = ExcuteRequest(req3);
            if (res3 == null) return null;
            string html = res3.Content.Substring(1);
            List<object> result;
            try
            {
                result = JsonConvert.DeserializeObject<List<object>>(res3.Content);
                string json = JsonConvert.SerializeObject(result[0]);
                List<List<string>> result2 = JsonConvert.DeserializeObject<List<List<string>>>(json);
                return result2;
            }
            catch
            {
                return new List<List<string>>();
            }
            
        }

    }

}
