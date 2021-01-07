using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Application.Exceptions
{
    public class RestException : Exception
    {
        public HttpStatusCode Code { get; set; }
        public object Errors { get; set; }
        public RestException(HttpStatusCode code, object err)
        {
            Code = code;
            Errors = err;
        }
    }
}
