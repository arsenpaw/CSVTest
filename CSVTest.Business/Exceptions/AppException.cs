using System.Net;

namespace CSVTest.Exceptions;

public class AppException : ApplicationException
{
    public HttpStatusCode StatusCode { get; }
    public string ErrorCode { get; }

    public AppException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        : base(message)
    {
        StatusCode = statusCode;
    }
    public AppException(ExceptionMessage msg) : base(msg.Message)
    {
        StatusCode = msg.StatusCode;

    }
}