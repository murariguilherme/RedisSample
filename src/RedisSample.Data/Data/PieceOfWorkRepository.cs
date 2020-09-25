using System;
using System.Collections.Generic;
using RedisSample.DataDomain.Models;
using RedisSample.DataDomain.Interfaces;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace RedisSample.DataDomain.Data
{
    public class PieceOfWorkRepository : Repository<PieceOfWork>, IPieceOfWorkRepository
    {
        public PieceOfWorkRepository(AppDbContext context, IDistributedCache distributedCache) : base(context, distributedCache) { }
        public async Task<IEnumerable<PieceOfWork>> GetListWithoutCache()
        {
            var powList = await base.DbSet.Include(p => p.Employee).ToListAsync();
            var options = new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(100) };
            _distributedCache.SetString(nameof(IEnumerable<PieceOfWork>), JsonSerializer.Serialize(powList, new JsonSerializerOptions() {  }));
            
            return powList;
        }
    }
}
