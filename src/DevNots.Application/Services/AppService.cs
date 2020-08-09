using DevNots.Application.Contracts;

namespace DevNots.Application.Services
{
    public abstract class AppService
    {
        protected AppResponse<TResult> ErrorResponse<TResult>(string errorMessage, int statusCode, AppResponse<TResult> response)
        {
            response.Error = new RequestError()
            {
                StatusCode = statusCode,
                Message = errorMessage,
            };

            return (AppResponse<TResult>) response;
        }
    }
}
