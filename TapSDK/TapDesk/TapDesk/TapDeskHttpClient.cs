using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TapTap.Desk
{
    public class TapDeskHttpClient
    {
        private HttpClient _httpClient;

        private string _server;

        private static TapDeskHttpClient _sInstance;

        private static readonly object Locker = new object();

        public static TapDeskHttpClient GetInstance()
        {
            lock (Locker)
            {
                if (_sInstance == null)
                {
                    _sInstance = new TapDeskHttpClient();
                }
            }

            return _sInstance;
        }

        public void Init(string serverUrl)
        {
            _httpClient = new HttpClient();
            _server = serverUrl;
            var product =
                new ProductHeaderValue("TapDesk", Assembly.GetExecutingAssembly().GetName().Version.ToString());
            _httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(product));
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public Task<string> Get(string path,
            Dictionary<string, object> headers = null,
            Dictionary<string, object> queryParams = null)
        {
            return Request(path, HttpMethod.Get, headers, null, queryParams);
        }

        public Task<string> Post(string path,
            Dictionary<string, object> headers = null,
            object data = null,
            Dictionary<string, object> queryParams = null)
        {
            return Request(path, HttpMethod.Post, headers, data, queryParams);
        }

        async Task<string> Request(string path,
            HttpMethod method,
            Dictionary<string, object> headers = null,
            object data = null,
            Dictionary<string, object> queryParams = null)
        {
            string url = BuildUrl(path, queryParams);

            HttpRequestMessage request = new HttpRequestMessage
            {
                RequestUri = new Uri(url),
                Method = method,
            };

            FillHeaders(request.Headers, headers);

            string content = null;
            if (data != null)
            {
                content = Json.Serialize(data);
                StringContent requestContent = new StringContent(content);
                requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                request.Content = requestContent;
            }

            PrintRequest(_httpClient, request, content);
            HttpResponseMessage response =
                await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            request.Dispose();

            string resultString = await response.Content.ReadAsStringAsync();
            response.Dispose();
            PrintResponse(response, resultString);

            if (response.IsSuccessStatusCode)
            {
                return resultString;
            }

            throw HandleErrorResponse(response.StatusCode, resultString);
        }

        TapDeskException HandleErrorResponse(HttpStatusCode statusCode, string responseContent)
        {
            int code = (int) statusCode;
            string message = responseContent;
            try
            {
                if (Json.Deserialize(
                    responseContent) is Dictionary<string, object> error)
                {
                    message = error["message"].ToString();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return new TapDeskException(code, message);
        }

        string BuildUrl(string path, Dictionary<string, object> queryParams = null)
        {
            var url = $"{_server}/{path}";
            if (queryParams == null) return url;
            var queryPairs = queryParams.Select(kv => $"{kv.Key}={kv.Value}");
            var queries = string.Join("&", queryPairs);
            url = $"{url}?{queries}";
            return url;
        }

        void FillHeaders(HttpRequestHeaders headers, Dictionary<string, object> reqHeaders = null)
        {
            if (reqHeaders != null)
            {
                foreach (var kv in reqHeaders)
                {
                    headers.Add(kv.Key, kv.Value.ToString());
                }
            }
        }

        public static void PrintRequest(HttpClient client, HttpRequestMessage request, string content = null)
        {
            if (client == null)
            {
                return;
            }

            if (request == null)
            {
                return;
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("=== HTTP Request Start ===");
            sb.AppendLine($"URL: {request.RequestUri}");
            sb.AppendLine($"Method: {request.Method}");
            sb.AppendLine($"Headers: ");
            foreach (var header in client.DefaultRequestHeaders)
            {
                sb.AppendLine($"\t{header.Key}: {string.Join(",", header.Value.ToArray())}");
            }

            foreach (var header in request.Headers)
            {
                sb.AppendLine($"\t{header.Key}: {string.Join(",", header.Value.ToArray())}");
            }

            if (request.Content != null)
            {
                foreach (var header in request.Content.Headers)
                {
                    sb.AppendLine($"\t{header.Key}: {string.Join(",", header.Value.ToArray())}");
                }
            }

            if (!string.IsNullOrEmpty(content))
            {
                sb.AppendLine($"Content: {content}");
            }

            sb.AppendLine("=== HTTP Request End ===");
            Console.WriteLine(sb.ToString());
            Debug.Log(sb.ToString());
        }

        public static void PrintResponse(HttpResponseMessage response, string content = null)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("=== HTTP Response Start ===");
            sb.AppendLine($"URL: {response.RequestMessage.RequestUri}");
            sb.AppendLine($"Status Code: {response.StatusCode}");
            if (!string.IsNullOrEmpty(content))
            {
                sb.AppendLine($"Content: {content}");
            }

            sb.AppendLine("=== HTTP Response End ===");
            Console.WriteLine(sb.ToString());
            Debug.Log(sb.ToString());
        }
    }
}