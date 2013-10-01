using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HyperApi.Helpers;
using HyperApi.Interfaces;

namespace HyperApi.Controllers
{
    public class BaseHyperController<TEntity, TRepo, TEntityId> : ApiController
        where TRepo : IRepository<TEntity, TEntityId>
        where TEntity : class, IEntity<TEntityId>
    {
        public TRepo repo { get; private set; }

        public BaseHyperController(TRepo repo)
        {
            this.repo = repo;
        }
        
        protected HttpResponseMessage BaseRequestMethod<T>(Func<RepositoryActionResult<T>> action)
        {
            return BaseRequestMethod(action, HttpStatusCode.OK, HttpStatusCode.InternalServerError);
        }

        protected HttpResponseMessage BaseRequestMethod<T>(Func<RepositoryActionResult<T>> action, HttpStatusCode success,
            HttpStatusCode failed)
        {
            var result = action.Invoke();

            return result.ToHttpResponseMessage(Request, success, failed);
        }
    }


    public static class RepositoryActionResultExteionsion
    {
        public static HttpResponseMessage ToGenericHttpResponseMessage<T>(this RepositoryActionResult<T> result, HttpRequestMessage request)
        {
            return result.ToHttpResponseMessage(request, HttpStatusCode.OK, HttpStatusCode.InternalServerError);
        }

        public static HttpResponseMessage ToHttpResponseMessage<T>(this RepositoryActionResult<T> result, HttpRequestMessage request, HttpStatusCode successCode, HttpStatusCode failureCode)
        {
            return result.IsSuccessful ? request.CreateResponse(successCode, result.Result) : request.CreateErrorResponse(failureCode, result.Message);
        }
    }
}