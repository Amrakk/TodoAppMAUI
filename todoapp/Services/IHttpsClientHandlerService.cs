namespace todoapp.Services
{
    public interface IHttpsClientHandlerService 
    { 
        HttpMessageHandler GetPlatformMessageHandler();
    }
}
