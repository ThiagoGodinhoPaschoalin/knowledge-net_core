using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poc.Encapsulation.Patterns
{
    public class Result<T>
    {
        public Exception Exception { get; set; }

        public bool Failure => Exception != null;

        public string FriendlyMessage { get; set; }

        public T Entity { get; set; }

    }

    public static class Output
    {
        public static Result<T> ByRepository<T>(Func<T> func)
        {
            try
            {
                var result = func.Invoke();
                return new Result<T>
                {
                    Entity = result
                };
            }
            catch (Exception ex)
            {
                return new Result<T>
                {
                    Exception = ex
                };
            }
        }

        public static Result<T> Error<T>(string message)
        {
            return new Result<T>
            {
                Exception = new Exception(message),
                FriendlyMessage = message
            };
        } 
    }
}
