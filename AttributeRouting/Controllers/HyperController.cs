using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AttributeRouting.Interfaces;
using AttributeRouting.Repositories;

namespace AttributeRouting.Controllers
{
    public class HyperController<TEntity, TRepo, TEntityId> : ApiController
        where TRepo : IRepository<TEntity, TEntityId>
        where TEntity : class, IEntity<TEntityId>
    {
        private readonly TRepo repo;

        public HyperController(TRepo repo)
        {
            this.repo = repo;
        }

        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return Get(request, () => repo.List());
        }

        public HttpResponseMessage Get(HttpRequestMessage request, TEntityId id)
        {
            return Get(request, () => repo.GetById(id));
        }

        private static HttpResponseMessage Get<T>(HttpRequestMessage request, Func<RepositoryActionResult<T>> func)
        {
            var result = func.Invoke();

            return result.ToHttpResponseMessage(request, HttpStatusCode.OK, HttpStatusCode.NotFound);
        }

        public HttpResponseMessage Put(HttpRequestMessage request, TEntity entity)
        {
            var result = repo.Update(entity);

            return result.ToGenericHttpResponseMessage(request);
        }

        public HttpResponseMessage Post(HttpRequestMessage request, TEntity entity)
        {
            var result = repo.Add(entity);

            return result.ToGenericHttpResponseMessage(request);
        }

        public HttpResponseMessage Delete(HttpRequestMessage request, TEntityId id)
        {
            var result = repo.Remove(id);

            return result.ToGenericHttpResponseMessage(request);
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
            if (result.IsSuccessful)
            {
                return request.CreateResponse(successCode, result.Result);
            }

            return request.CreateErrorResponse(failureCode, result.Message);
        }
    }
}