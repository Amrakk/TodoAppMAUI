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
        

    }
}
