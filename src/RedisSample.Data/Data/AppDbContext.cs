using Microsoft.EntityFrameworkCore;
using RedisSample.Data.Models;

namespace RedisSample.Data.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(): base() { }
        public DbSet<Employeer> Employeers { get; set; }
        public DbSet<Task> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseInMemoryDatabase("RedisSample");
        }
    }
}
