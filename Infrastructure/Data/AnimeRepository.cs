using AnimesProtech.Domain.Entities;
using AnimesProtech.Domain.Interfaces.DbContext;
using AnimesProtech.Domain.Interfaces.Repositorys;
using Microsoft.EntityFrameworkCore;

namespace AnimesProtech.Infrastructure.Data
{
    public class AnimeRepository : IAnimeRepository
    {

        private readonly IAppDbContext _context;

        public AnimeRepository(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Anime> Add(Anime entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            entity.created_at = DateTime.Now;

            _context.Animes.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public Task<List<Anime>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Anime> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Anime?> GetByName(string name)
        {
            return await _context.Animes.FirstOrDefaultAsync(it => 
                it.name == name
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
