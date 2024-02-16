namespace NET_CYBER_API.API.Tools.Errors
{
    public class ErrorResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }

        public ErrorResponse(int code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}
