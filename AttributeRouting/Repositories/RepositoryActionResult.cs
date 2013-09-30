using System.Collections.Generic;
using System.Linq;

namespace AttributeRouting.Repositories
{
    /// <summary>
    /// A container from handling Repository Actions
    /// </summary>
    /// <typeparam name="T">The type of the actions result</typeparam>
    public struct RepositoryActionResult<T>
    {
        public T Result;
        public bool IsSuccessful;
        public string Message;

        /// <summary>
        /// A generic successful result
        /// </summary>
        /// <param name="result">Result of abitrary type <typeparam name="T"></typeparam></param>
        /// <returns>A <see cref="RepositoryActionResult{T}"/></returns>
        public static RepositoryActionResult<T> Successful(T result)
        {
            return Successful(result, "BulkActionSuccessLevel");
        }

        /// <summary>
        /// A successful result with custom message
        /// </summary>
        /// <param name="result">Result of abitrary type <typeparam name="T"></typeparam></param>
        /// <param name="message">Message indicating reason for success</param>
        /// <returns>A <see cref="RepositoryActionResult{T}"/></returns>
        public static RepositoryActionResult<T> Successful(T result, string message)
        {
            return new RepositoryActionResult<T>
            {
                Result = result,
                IsSuccessful = true,
                Message = message,
            };
        }

        /// <summary>
        /// A unsuccessful result with a custom message
        /// </summary>
        /// <param name="result">Result of abitrary type <typeparam name="T"></typeparam></param>
        /// <param name="message">Message indicating reason for failure</param>
        /// <returns>A <see cref="RepositoryActionResult{T}"/></returns>
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

    /// <summary>
    /// A container form handling Bulk Repository Actions
    /// </summary>
    /// <typeparam name="T">The type of the actions result</typeparam>
    public struct RepositoryBulkActionResult<T>
    {
        public ICollection<RepositoryActionResult<T>> Result;
        public BulkActionSuccessLevel BulkActionSuccessLevel;
        public string Message;

        /// <summary>
        /// A Method for indicating success level from a batch operation
        /// </summary>
        /// <param name="results">A <see cref="ICollection{T}"/> of <see cref="RepositoryActionResult{T}"/></param>
        /// <returns>A <see cref="RepositoryBulkActionResult{T}"/> that contains information about a bulk action</returns>
        public static RepositoryBulkActionResult<T> FromBulkResults(ICollection<RepositoryActionResult<T>> results)
        {
            var newResult = new RepositoryBulkActionResult<T>();
            var successfullResults = results.Count(r => r.IsSuccessful);

            newResult.BulkActionSuccessLevel = successfullResults == results.Count ? BulkActionSuccessLevel.Full : successfullResults > 0 ? BulkActionSuccessLevel.Partial : BulkActionSuccessLevel.NotSuccessful;

            return newResult;
        }
    }

    /// <summary>
    /// Used for indicating Success of a <see cref="RepositoryBulkActionResult{T}"/>
    /// </summary>
    public enum BulkActionSuccessLevel
    {
        Partial,
        Full,
        NotSuccessful
    }
}