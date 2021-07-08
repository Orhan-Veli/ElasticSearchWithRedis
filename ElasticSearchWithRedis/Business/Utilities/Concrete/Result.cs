using ElasticSearchWithRedis.Business.Utilities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSearchWithRedis.Business.Utilities.Concrete
{
    public class Result<T> : IResult<T> 
    {

        public Result(string message, bool success, T data)
        {
            Data = data;
            Message = message;
            Success = success;
        }

        public Result(string message, bool success)
        {
            Message = message;
            Success = success;
        }

        public Result(bool success, T data)
        {
            Data = data;
            Success = success;
        }
        public Result(bool success)
        {
            Success = success;
        }
        public T Data { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
