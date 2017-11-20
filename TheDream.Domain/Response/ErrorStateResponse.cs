using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace TheDream.Domain.Response
{
    public class ErrorStateResponse
    {
        public string Message { get; set; }
        public IDictionary<string, string[]> ModelState { get; set; }
    }
    public class ApiException : Exception
    {
        public ApiException(HttpStatusCode statusCode, string jsonData)
        {
            StatusCode = statusCode;
            JsonData = jsonData;
        }

        public HttpStatusCode StatusCode { get; private set; }
        public string JsonData { get; private set; }
    }
}