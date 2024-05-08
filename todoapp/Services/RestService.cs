using System.Net;

namespace todoapp.Services
{
    public class RestService
    {
        private HttpClient _client;
        IHttpsClientHandlerService _httpsClientHandlerService;

        public RestService(IHttpsClientHandlerService service)
        {
#if DEBUG
            _httpsClientHandlerService = service;
            HttpMessageHandler handler = _httpsClientHandlerService.GetPlatformMessageHandler();

            if (handler != null) _client = new HttpClient(handler);
            else _client = new HttpClient();
#else
            _client = new HttpClient();
#endif
            _client.BaseAddress = new Uri("https://todoapi-uxe5.onrender.com/api/v2/");
        }

        public HttpClient GetClient => _client;

        public static void SaveCookies(HttpResponseMessage response)
        {
            if(response.Headers.TryGetValues("Set-Cookie", out var values))
            {
                foreach (string value in values)
                {
                    string[] parts = value.Split(";");
                    string token = parts[0].Split("=")[1];
                    if (value.Contains("ref_token"))
                        Preferences.Set("ref_token", token);
                    else if (value.Contains("access_token"))
                        Preferences.Set("access_token", token);
                }
            }
        }

        public static string GetCookies()
        {
            Uri uri = new Uri("https://todoapi-uxe5.onrender.com/api/v2/");

            CookieContainer cookies = new CookieContainer();
            cookies.Add(uri, new Cookie("ref_token", Preferences.Get("ref_token", "")));
            cookies.Add(uri, new Cookie("access_token", Preferences.Get("access_token", "")));
            return cookies.GetCookieHeader(uri);
        }
    }
}
