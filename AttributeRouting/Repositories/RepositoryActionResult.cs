using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AttributeRouting.Repositories
{
    public struct RepositoryActionResult<T>
    {
        public T Result;
        public bool IsSuccessful;
        public string Message;

        public static RepositoryActionResult<T> Successful(T result)
        {
            return Successful(result, "Success");
        }

        public static RepositoryActionResult<T> Successful(T result, string message)
        {
            return new RepositoryActionResult<T>
            {
                Result = result,
                IsSuccessful = true,
                Message = message,
            };
        }

        public static RepositoryActionResult<T> Unsuccessful(T result, string message)
        {
            return new RepositoryActionResult<T>
            {
                Result = result,
                IsSuccessful = false,
                Message = message,
            };
        }

    }

    public struct RepositoryBulkActionResult<T>
    {
        public ICollection<RepositoryActionResult<T>> Result;
        public Success Success;
        public string Message;

        public static RepositoryBulkActionResult<T> FromBulkResults(ICollection<RepositoryActionResult<T>> results)
        {
            var newResult = new RepositoryBulkActionResult<T>();
            
            newResult.Success = results.All(r =>r.IsSuccessful) ? Success.Full : results.Any(r => r.IsSuccessful) ? Success.Partial : Success.NotSuccessful;

            return newResult;
        }
    }

    public enum Success
    {
        Partial,
        Full,
        NotSuccessful
    }
}