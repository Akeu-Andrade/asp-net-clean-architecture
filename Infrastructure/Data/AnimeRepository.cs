using AnimesProtech.Domain.Entities;
using AnimesProtech.Domain.Interfaces.DbContext;
using AnimesProtech.Domain.Interfaces.Repositorys;
using AnimesProtech.Domain.Specifications;
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

        public async Task<List<Anime>> GetAll(AnimeSearchCriteria criteria)
        {
            var query = _context.Animes.AsQueryable();

            query = ApplyDirectorFilter(query, criteria.Director);
            query = ApplyNameFilter(query, criteria.Name);
            query = ApplyKeywordsFilter(query, criteria.Keywords);
            query = ApplyPagination(query, criteria.PageIndex, criteria.PageSize);

            return await query.ToListAsync();
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
