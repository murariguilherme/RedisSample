using Microsoft.EntityFrameworkCore;
using RedisSample.DataDomain.Models;

namespace RedisSample.DataDomain.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(): base() { }
        public DbSet<Employeer> Employeers { get; set; }
        public DbSet<PieceOfWork> PiecesOfWork { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseInMemoryDatabase("RedisSample");
        }
    }
}
