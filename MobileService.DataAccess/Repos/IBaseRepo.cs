using MobileService.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileService.DataAccess.Repos
{
    public interface IBaseRepo<TEntity> where TEntity : BaseModel
    {
        public Task Insert(TEntity entity);
        public Task<TEntity> Get(Guid entityId);
        public Task<List<TEntity>> Get();
        public Task Delete(TEntity entity);
        public Task Update(TEntity entity);
    }
}
