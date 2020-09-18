using RedisSample.DataDomain.Interfaces;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RedisSample.DataDomain.Models;
using EasyCaching.Core;

namespace RedisSample.DataDomain.Data
{
    public abstract class Repository<T> : IRepository<T> where T: Entity
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> DbSet;
        protected readonly IEasyCachingProvider _provider;
        protected readonly IEasyCachingProviderFactory _factory;
        public Repository(AppDbContext context, IEasyCachingProviderFactory factory)
        {
            _context = context;
            DbSet = _context.Set<T>();
            _factory = factory;
            _provider = _factory.GetCachingProvider("redis1");
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task Add(T obj)
        {
            await DbSet.AddAsync(obj);
            _provider.Set<T>(obj.Id.ToString(), obj, TimeSpan.FromDays(100));
        }

        public async Task Delete(Guid id)
        {
            var entity = await this.Read(id);
            DbSet.Remove(entity);
            _provider.Remove(id.ToString());
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<T> Read(Guid id)
        {
            var objectCache = _provider.Get<T>(id.ToString());

            if (objectCache.HasValue) return objectCache.Value;
            
            var objectDatabase = await _context.FindAsync<T>(id);
            
            _provider.Set<T>(id.ToString(), objectDatabase, TimeSpan.FromDays(100));

            return objectDatabase;
        }

        public async Task Update(T obj)
        {
            DbSet.Update(obj);
            _provider.Set<T>(obj.Id.ToString(), obj, TimeSpan.FromDays(100));
        }
    }
}
