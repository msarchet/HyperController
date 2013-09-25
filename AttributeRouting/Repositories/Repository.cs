using System;
using System.Collections.Generic;
using System.Linq;
using AttributeRouting.Interfaces;

namespace AttributeRouting.Repositories
{
    public class Repository<T, TEntityId> : IRepository<T, TEntityId> where T: class, IEntity<TEntityId>
    {
        public virtual ICollection<T> Entities { get; set; }

        public virtual RepositoryActionResult<ICollection<T>> List()
        {
            return RepositoryActionResult<ICollection<T>>.Successful(Entities);
        }

        public virtual RepositoryActionResult<T> GetById(TEntityId id)
        {
            var entity = Entities.FirstOrDefault(e => e.Id.Equals(id));

            if (entity != null)
            {
                return RepositoryActionResult<T>.Successful(entity);
            }

            return RepositoryActionResult<T>.Unsuccessful(null, String.Format("Failed to add new {0}", typeof (T)));
        }

        public RepositoryActionResult<T> Add(T entity)
        {
            Entities.Add(entity);
            return RepositoryActionResult<T>.Successful(entity);
        }

        public RepositoryActionResult<bool> Remove(TEntityId id)
        {
            var existing = GetById(id);
            
            if (existing.IsSuccessful && Entities.Remove(existing.Result))
            {
                return RepositoryActionResult<bool>.Successful(true);
            }

            return RepositoryActionResult<bool>.Unsuccessful(false, String.Format("Unable to remove {0}", typeof(T)));
        }

        public RepositoryActionResult<T> Update(T entity)
        {
            var existing = GetById(entity.Id);
            if (existing.IsSuccessful)
            {
                existing.Result.Update(entity);
                return RepositoryActionResult<T>.Successful(entity);
            }

            return RepositoryActionResult<T>.Unsuccessful(entity, String.Format("Unable to update {0}", typeof(T)));
        }
    }
}