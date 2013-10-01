using System.Collections.Generic;
using HyperApi.Helpers;

namespace HyperApi.Interfaces
{
    public interface IRepository<T, in TEntityId> where T : IEntity<TEntityId>
    {
        ICollection<T> Entities { get; set; }
        RepositoryActionResult<ICollection<T>> List();
        RepositoryActionResult<T> GetById(TEntityId id);
        RepositoryActionResult<T> Add(T entity);
        RepositoryActionResult<bool> Remove(TEntityId id);
        RepositoryActionResult<T> Update(T entity);
    }
}
