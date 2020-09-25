using RedisSample.DataDomain.Models;
using RedisSample.DataDomain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace RedisSample.DataDomain.Data
{
    public class EmployeeRepository: Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context, IDistributedCache distributedCache) : base(context, distributedCache) { }
    }
}
