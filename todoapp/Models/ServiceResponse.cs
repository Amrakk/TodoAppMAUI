namespace todoapp.Models
{
    public enum Status
    {
        Success = 1,
        Failed = 0,
        Error = -1
    }

    public class ServiceResponse
    {
        public Status Status { get; set; }
        public string Message { get; set; } = "";
    }
}
