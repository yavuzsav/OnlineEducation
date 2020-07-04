﻿using System;
using System.Net;

namespace OnlineEducation.Core.ErrorHelpers
{
    public class RestException : Exception
    {
        public RestException(HttpStatusCode httpStatusCode, object errors = null)
        {
            HttpStatusCode = httpStatusCode;
            Errors = errors;
        }

        public RestException(HttpStatusCode httpStatusCode, string error = null)
        {
            HttpStatusCode = httpStatusCode;
            Errors = error;
        }

        public HttpStatusCode HttpStatusCode { get; }

        public object Errors { get; }
    }
}
