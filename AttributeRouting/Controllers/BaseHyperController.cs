using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AttributeRouting.Interfaces;
using AttributeRouting.Repositories;
using Microsoft.Ajax.Utilities;

namespace AttributeRouting.Controllers
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

            return result.ToHttpResponseMessage(base.Request, success, failed);
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