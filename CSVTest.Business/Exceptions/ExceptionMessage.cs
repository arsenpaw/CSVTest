using System.Net;

namespace CSVTest.Exceptions
{
    public class ExceptionMessage
    {
        public string Message { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public ExceptionMessage(string message, HttpStatusCode? statusCode = null)
        {
            Message = message;
            StatusCode = statusCode ?? HttpStatusCode.BadRequest;
        }
    }
    public static class ExceptionMessages
    {
        public static readonly ExceptionMessage NotFound =
            new ExceptionMessage("The requested resource does not found.", HttpStatusCode.NotFound);

        public static readonly ExceptionMessage ArgumentNull =
            new ExceptionMessage("A required argument was null.", HttpStatusCode.BadRequest);

        public static readonly ExceptionMessage UnexpectedError =
            new ExceptionMessage("An unexpected error occurred.", HttpStatusCode.InternalServerError);
        
    }
}
