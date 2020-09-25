using RedisSample.DataDomain.Interfaces;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RedisSample.DataDomain.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace RedisSample.DataDomain.Data
{
    public abstract class Repository<T> : IRepository<T> where T: Entity
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> DbSet;
        protected readonly IDistributedCache _distributedCache;        
        public Repository(AppDbContext context, IDistributedCache distributedCache)
        {
            _context = context;
            DbSet = _context.Set<T>();
            _distributedCache = distributedCache;            
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task Add(T obj)
        {
            var json = JsonSerializer.Serialize<T>(obj);
            await DbSet.AddAsync(obj);
            
            var options = new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(100) };
            _distributedCache.SetString(obj.Id.ToString(), json);
        }

        public async Task Delete(Guid id)
        {
            var entity = await this.Read(id);
            DbSet.Remove(entity);
            _distributedCache.Remove(id.ToString());
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var listCache = await _distributedCache.GetStringAsync(nameof(IEnumerable<T>));
            if (listCache != null) return JsonSerializer.Deserialize<IEnumerable<T>>(listCache);
            return await DbSet.AsNoTracking().ToListAsync();
        }

        public async Task<T> Read(Guid id)
        {
            var objectCache = await _distributedCache.GetStringAsync(id.ToString());

            if (objectCache != null) return JsonSerializer.Deserialize<T>(objectCache);
            
            var objectDatabase = await _context.FindAsync<T>(id);
            
            var options = new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(100) };
            _distributedCache.SetString(objectDatabase.Id.ToString(), JsonSerializer.Serialize(objectDatabase), options);

            return objectDatabase;
        }

        public async Task Update(T obj)
        {
            DbSet.Update(obj);            
            var options = new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(100) };
            _distributedCache.SetString(obj.Id.ToString(), JsonSerializer.Serialize(obj), options);

        }
    }
}
