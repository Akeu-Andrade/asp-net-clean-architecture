using AnimesProtech.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AnimesProtech.Domain.Interfaces.DbContext
{
    public interface IAppDbContext
    {
        DbSet<Anime> Animes { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
