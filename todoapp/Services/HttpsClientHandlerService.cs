namespace todoapp.Services
{
    public class HttpsClientHandlerService : IHttpsClientHandlerService
    {
        public HttpMessageHandler GetPlatformMessageHandler()
        {
#if ANDROID
            var handler = new Xamarin.Android.Net.AndroidMessageHandler();

            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert != null && cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
#elif IOS
            var handler = new NSUrlSessionHandler();
            return handler;
#elif WINDOWS || MACCATALYST
            return null!;
#else
            throw new PlatformNotSupportedException("Only Android, iOS, MacCatalyst, and Windows supported.");
#endif
        }
    }
}
