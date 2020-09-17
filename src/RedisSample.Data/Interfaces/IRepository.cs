using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RedisSample.DataDomain.Models;

namespace RedisSample.DataDomain.Interfaces
{
    public interface IRepository<T> where T: Entity
    {
        IUnitOfWork UnitOfWork { get; }
        Task Add(T obj);
        Task<T> Read(Guid id);
        Task Update(T obj);
        Task Delete(Guid id);
        Task<IEnumerable<T>> GetAll();
    }
}
