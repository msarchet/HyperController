using System.Net;
using System.Net.Http;
using System.Web.Http;
using HyperApi.Interfaces;

namespace HyperApi.Controllers
{
    public class HyperController<TEntity, TRepo, TEntityId> : BaseHyperController<TEntity, TRepo, TEntityId>
        where TRepo : IRepository<TEntity, TEntityId>
        where TEntity : class, IEntity<TEntityId>
    {

        public HyperController(TRepo repo) : base(repo)
        {
        }

        [HttpGet("")]
        public HttpResponseMessage Get()
        {
            return BaseRequestMethod(() => repo.List());
        }

        [HttpGet("{id}")]
        public HttpResponseMessage Get(TEntityId id)
        {
            return BaseRequestMethod(() => repo.GetById(id));
        }

        [HttpPut("{id}")]
        public HttpResponseMessage Put(TEntity entity)
        {
            return BaseRequestMethod(() => repo.Update(entity), HttpStatusCode.OK, HttpStatusCode.InternalServerError);
        }

        [HttpPost("")]
        public HttpResponseMessage Post(TEntity entity)
        {
            return BaseRequestMethod(() => repo.Add(entity), HttpStatusCode.Created, HttpStatusCode.InternalServerError);
        }

        [HttpDelete("{id}")]
        public HttpResponseMessage Delete(TEntityId id)
        {
            return BaseRequestMethod(() => repo.Remove(id), HttpStatusCode.NoContent, HttpStatusCode.InternalServerError);
        }
    }
    
}