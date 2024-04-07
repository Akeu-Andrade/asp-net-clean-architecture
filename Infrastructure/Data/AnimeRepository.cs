using AnimesProtech.Domain.Entities;
using AnimesProtech.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AnimesProtech.Infrastructure.Data
{
    public class AnimeRepository : IAnimeRepository
    {

        private readonly AppDbContext _context;

        public AnimeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Anime> Add(Anime entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Animes.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public Task<List<Anime>> GetAll(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Anime> GetById(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Anime?> GetByName(string name, CancellationToken cancellationToken)
        {
            return await _context.Animes.FirstOrDefaultAsync(it => 
                it.name == name, 
                cancellationToken
            );
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Anime entity)
        {
            throw new NotImplementedException();
        }
    }
}
