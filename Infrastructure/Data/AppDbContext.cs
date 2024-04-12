using AnimesProtech.Domain.Entities;
using AnimesProtech.Domain.Interfaces.DbContext;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AnimesProtech.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Anime> Animes { get; set; }
    }
}
