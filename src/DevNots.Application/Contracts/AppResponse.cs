namespace DevNots.Application.Contracts
{
    public class AppResponse
    {
        public RequestError Error { get; internal set; }
    }

    public class AppResponse<TResult>: AppResponse
    {
        public TResult Result { get; internal set; }
    }

    public class RequestError
    {
        public int StatusCode { get; internal set; }
        public string Message { get; internal set; }
    }
}
