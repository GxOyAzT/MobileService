using Microsoft.EntityFrameworkCore;
using MobileService.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileService.DataAccess.Repos
{
    public class BaseRepo<TEntity> : IBaseRepo<TEntity> where TEntity : BaseModel
    {
        protected readonly AppDbContext _appDbContext;

        public BaseRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public virtual async Task Delete(TEntity entity)
        {
            _appDbContext.Set<TEntity>().Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }
            

        public virtual async Task<TEntity> Get(Guid entityId) => 
            await _appDbContext.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == entityId);

        public virtual async Task<List<TEntity>> Get() => 
            await _appDbContext.Set<TEntity>().ToListAsync();

        public virtual async Task Insert(TEntity entity)
        {
            await _appDbContext.Set<TEntity>().AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public virtual async Task Update(TEntity entity)
        {
            _appDbContext.Set<TEntity>().Update(entity);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
