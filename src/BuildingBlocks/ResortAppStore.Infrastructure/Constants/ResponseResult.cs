using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Constants
{
    public class ResponseResult<T> where T : class 
    {

        public string Message { get; set; }
        public bool Success { get; set; }
        public T Data { get; set; }
        public int Status { get; set; }
    }

    public class ResponseResult
    {

        public string Message { get; set; }
        public bool Success { get; set; }
        public int Status { get; set; }
        //public T Data { get; set; }
    }
    public class Response
    {
        public string? Status { get; set; }
        public string? Message { get; set; }
        public ResponseDetail ResponseDetail { get; set; }
        public bool Success { get; set; }
    }

    public class ResponseDetail
    {
        public string? Message { get; set; }
        public bool? Success { get; set; }
        public int? Status { get; set; }
    }
}
