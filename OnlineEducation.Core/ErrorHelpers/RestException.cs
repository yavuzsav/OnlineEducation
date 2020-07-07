using System;
using System.Net;

namespace OnlineEducation.Core.ErrorHelpers
{
    public class RestException : Exception
    {
        public RestException(HttpStatusCode httpStatusCode, object errors = null)
        {
            HttpStatusCode = httpStatusCode;

            if (errors == null)
            {
                switch (HttpStatusCode)
                {
                    case HttpStatusCode.NotFound:
                        Errors = "Not Found";
                        break;
                    case HttpStatusCode.Unauthorized:
                        Errors = "Unauthorized";
                        break;
                }
            }
            else
            {
                Errors = errors;
            }
        }

        public HttpStatusCode HttpStatusCode { get; }

        public object Errors { get; }
    }
}
