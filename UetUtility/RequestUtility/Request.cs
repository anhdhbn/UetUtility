using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace UetGrade.RequestUtility
{
    public abstract class Request
    {
        protected static int TimeoutRequest = 5 * 60 * 1000;
        protected RestClient client;

        protected IRestResponse ExcuteRequest(IRestRequest request)
        {
            try
            {
                var response = client.Execute(request);
                response.Content = WebUtility.HtmlDecode(response.Content);
                return response;
            }
            catch
            {
                return null;
            }
        }

        protected IRestRequest CreateRequest(string Link, Method method = Method.GET, PostType postType = PostType.json, Dictionary<string, string> parameters = null, object body = null)
        {
            RestRequest request = new RestRequest(Link, method);
            if (method == Method.POST)
            {
                request.Method = Method.POST;
                
                
                if(postType == PostType.json)
                {
                    if(body != null)
                    {
                        request.RequestFormat = DataFormat.Json;
                        request.AddParameter("application/json", body, ParameterType.RequestBody);
                    }
                    else
                    {
                        request.AddHeader("Content-Type", GetTypePost(postType));
                    }
                }
                else
                {
                    request.AddHeader("Content-Type", GetTypePost(postType));
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            request.AddParameter(param.Key, param.Value, ParameterType.GetOrPost);
                        }
                    }
                }
            }
            return request;
        }

        protected string GetTypePost(PostType postType)
        {
            if (postType == PostType.form) return "application/x-www-form-urlencoded";
            if (postType == PostType.json) return "application/json";
            return string.Empty;
        }
    }

    public enum PostType
    {
        form = 0,
        json = 1
    }
}
