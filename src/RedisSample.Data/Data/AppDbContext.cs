using Microsoft.EntityFrameworkCore;
using RedisSample.DataDomain.Interfaces;
using RedisSample.DataDomain.Models;
using System.Threading.Tasks;

namespace RedisSample.DataDomain.Data
{
    public class AppDbContext: DbContext, IUnitOfWork
    {
        public AppDbContext(): base() { }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<PieceOfWork> PiecesOfWork { get; set; }

        public async Task<bool> Commit()
        {
            return await SaveChangesAsync() > 1;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseInMemoryDatabase("RedisSample");
        }
    }
}
