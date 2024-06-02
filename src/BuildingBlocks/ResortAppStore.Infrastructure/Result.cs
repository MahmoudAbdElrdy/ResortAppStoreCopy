using Common.Infrastructures;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace Common {
    public class Result<T> where T : class
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public T Response { get; set; }
        public bool Success { get; set; }
        public Result()
        {

        }

        public static Result<T> CreateSuccess( string message, T response)
        {
            return new Result<T>
            {
                Status = "Success",
                Message = message,
                Response = response,
                Success = true
        };
        }

        public static Result<T> CreateFailure(string message, T response = null)
        {
            return new Result<T>
            {
                Status = "Failure",
                Message = message,
                Response = response,
                Success = false
            };
        }
    }

    public class Result : Result<object>
    {

    }

    public class ExceptionResult : Result
    {
        public string ExceptionMessage { get; set; }
    }

    public static class ExceptionResultHelper
    {
        public static ExceptionResult CreateFailure(string message, object response = null, string exceptionMessage = "")
        {
            return new ExceptionResult
            {
                Status = "Failure",
                Message = message,
                Response = response,
                Success = false,
                ExceptionMessage = exceptionMessage
            };
        }
    }

    public static class ResultHelper
    {
        public static Result<T> CreateSuccess<T>(string message, T response)
            where T : class
        {
            return new Result<T>
            {
                Status = "Success",
                Message = message,
                Response = response,
                Success = true
            };
        }

        public static Result<T> CreateFailure<T>(string message, T response = null)
            where T : class
        {
            return new Result<T>
            {
                Status = "Failure",
                Message = message,
                Response = response,
                Success = false
            };
        }


        public static Result CreateSuccess(string message, object response)
        {
            return new Result
            {
                Status = "Success",
                Message = message,
                Response = response,
                Success = true
            };
        }

        public static Result CreateFailure(string message, object response = null)
        {
            return new Result
            {
                Status = "Failure",
                Message = message,
                Response = response,
                Success = false
            };
        }
    }
}