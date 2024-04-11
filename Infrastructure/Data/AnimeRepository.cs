using AnimesProtech.Domain.Entities;
using AnimesProtech.Domain.Interfaces.DbContext;
using AnimesProtech.Domain.Interfaces.Repositorys;
using AnimesProtech.Domain.Specifications;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

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

            entity.updated_at = DateTime.UtcNow;
            _context.Animes.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<List<Anime>> GetAll(AnimeSearchCriteria criteria)
        {
            var query = _context.Animes.AsQueryable();

            query = ApplyDirectorFilter(query, criteria.Director);
            query = ApplyNameFilter(query, criteria.Name);
            query = ApplyKeywordsFilter(query, criteria.Keywords);
            query = ApplyPagination(query, criteria.PageIndex, criteria.PageSize);

            return await query.ToListAsync();
        }

        public async Task Delete(Guid id)
        {
            var anime = await _context.Animes.FindAsync(id);

            if (anime == null)
                throw new KeyNotFoundException("Anime não encontrado");

            anime.deleted_at = DateTime.UtcNow;

            _context.Animes.Update(anime);
            await _context.SaveChangesAsync();
        }

        public async Task<Anime> Update(Anime entity)
        {
            var anime = await _context.Animes.FindAsync(entity.id);
            if (anime == null)
                throw new KeyNotFoundException("Anime não encontrado");

            anime.name = entity.name;
            anime.summary = entity.summary;
            anime.director = entity.director;
            anime.updated_at = DateTime.UtcNow;

            _context.Animes.Update(anime);
            await _context.SaveChangesAsync();

            return anime;
        }

        public async Task<Anime> GetById(Guid id)
        {
            return await _context.Animes.FindAsync(id);
        }

        private IQueryable<Anime> ApplyDirectorFilter(IQueryable<Anime> query, string director)
        {
            if (!string.IsNullOrEmpty(director))
            {
                query = query.Where(a => a.director == director);
            }

            return query;
        }

        private IQueryable<Anime> ApplyNameFilter(IQueryable<Anime> query, string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(a => a.name.Contains(name));
            }

            return query;
        }

        private IQueryable<Anime> ApplyKeywordsFilter(IQueryable<Anime> query, string keywords)
        {
            if (!string.IsNullOrEmpty(keywords))
            {
                query = query.Where(a => a.summary.Contains(keywords));
            }

            return query;
        }

        private IQueryable<Anime> ApplyPagination(IQueryable<Anime> query, int? pageIndex, int? pageSize)
        {
            if (pageIndex.HasValue && pageSize.HasValue)
            {
                query = query.Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            return query;
        }
    }
}
