using RedisSample.DataDomain.Interfaces;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RedisSample.DataDomain.Models;

namespace RedisSample.DataDomain.Data
{
    public abstract class Repository<T> : IRepository<T> where T: Entity
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> DbSet;
        public Repository()
        {
            _context = new AppDbContext();
            DbSet = _context.Set<T>();
        }

        public async Task Add(T obj)
        {
            await DbSet.AddAsync(obj);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var entity = await this.Read(id);
            DbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<T> Read(Guid id)
        {
            return await _context.FindAsync<T>(id);
        }

        public async Task Update(T obj)
        {
            DbSet.Update(obj);
            await _context.SaveChangesAsync();
        }
    }
}
