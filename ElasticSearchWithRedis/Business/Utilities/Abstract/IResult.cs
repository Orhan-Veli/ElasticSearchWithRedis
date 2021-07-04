using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSearchWithRedis.Business.Utilities.Abstract
{
    public interface IResult<T> where T:class
    {
        public T Data { get; set; }

        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
