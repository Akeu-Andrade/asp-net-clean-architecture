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
        private readonly ILogger<AnimeRepository> _logger;

        public AnimeRepository(IAppDbContext context, ILogger<AnimeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Anime> Add(Anime entity)
        {
            try
            {
                Anime anime = new Anime
                {
                    name = entity.name,
                    summary = entity.summary,
                    director = entity.director,
                    created_at = DateTime.UtcNow
                };

                _context.Animes.Add(anime);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Anime {anime.name} adicionado com sucesso");

                return anime;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao adicionar o anime {entity.name} ao banco de dados");
                throw new InvalidOperationException("Erro inesperado ao adicionar o anime ao banco de dados");
            }
        }

        public async Task<List<Anime>> GetAll(AnimeSearchCriteria criteria)
        {
            try
            {
                var query = _context.Animes.AsQueryable();

                query = query.Where(a => a.deleted_at == null);
                query = ApplyDirectorFilter(query, criteria.Director);
                query = ApplyNameFilter(query, criteria.Name);
                query = ApplySummaryFilter(query, criteria.Summary);
                query = ApplyPagination(query, criteria.PageIndex, criteria.PageSize);

                var animes = await query.ToListAsync();

                _logger.LogInformation($"Operação de busca de animes concluída. {animes.Count} animes encontrados");

                return animes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar animes");
                throw new InvalidOperationException("Erro inesperado ao buscar animes");
            }
        }

        public async Task Delete(Guid id)
        {
            try
            {
                var anime = await GetById(id);

                anime.deleted_at = DateTime.UtcNow;

                _context.Animes.Update(anime);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Operação de exclusão do anime com ID {id} concluída com sucesso");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao excluir o anime com ID {id}");
                throw new InvalidOperationException("Erro inesperado ao excluir o anime");
            }
        }

        public async Task<Anime> Update(Anime entity)
        {
            try
            {
                var anime = await GetById(entity.id);

                if (anime.deleted_at != null)
                    throw new InvalidOperationException("Não é possível editar um anime que foi deletado");

                anime.name = entity.name;
                anime.summary = entity.summary;
                anime.director = entity.director;
                anime.updated_at = DateTime.UtcNow;

                _context.Animes.Update(anime);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Operação de atualização do anime com ID {entity.id} concluída com sucesso");

                return anime;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao atualizar o anime com ID {entity.id}");
                throw new InvalidOperationException("Erro inesperado ao atualizar o anime");
            }
        }

        public async Task<Anime?> GetByName(string name)
        {
            try
            {
                var anime = await _context.Animes.FirstOrDefaultAsync(a => a.name == name);

                _logger.LogInformation(anime != null ? $"Anime {name} encontrado com sucesso" : $"Anime {name} não encontrado");

                return anime;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao buscar o anime com nome {name}");
                throw new InvalidOperationException("Erro inesperado ao buscar o anime pelo nome");
            }
        }

        public async Task<Anime> GetById(Guid id)
        {
            try
            {
                var anime = await _context.Animes.FindAsync(id);

                if (anime == null)
                {
                    throw new KeyNotFoundException("Anime não encontrado");
                }

                _logger.LogInformation($"Anime com ID {id} encontrado com sucesso");

                return anime;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao buscar o anime com ID {id}");
                throw new InvalidOperationException("Erro inesperado ao buscar o anime pelo ID");
            }
        }

        private IQueryable<Anime> ApplyDirectorFilter(IQueryable<Anime> query, string? director)
        {
            if (!string.IsNullOrEmpty(director))
            {
                query = query.Where(a => a.director == director);
            }

            return query;
        }

        private IQueryable<Anime> ApplyNameFilter(IQueryable<Anime> query, string? name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(a => a.name.Contains(name));
            }

            return query;
        }

        private IQueryable<Anime> ApplySummaryFilter(IQueryable<Anime> query, string? summary)
        {
            if (!string.IsNullOrEmpty(summary))
            {
                query = query.Where(a => a.summary!.Contains(summary));
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
