using AnimesProtech.Domain.Entities;
using AnimesProtech.Domain.Interfaces;
using Microsoft.OpenApi.Models;

namespace AnimesProtech.Application.Endpoints
{
    public static class AnimesEndpoints
    {
        public static void RegisterAnimesEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/v1/animes").WithTags(nameof(Anime));

            group.MapPost("/", async (Anime anime, IAnimeRepository _animeRepository) =>
            {
                await _animeRepository.Add(anime);
                return Results.Created($"/api/v1/animes/{anime.id}", anime);
            })
                .WithName("Adiciona Novo Anime")
                .RequireAuthorization()
                .WithOpenApi(it => new OpenApiOperation(it)
                {
                    Description = "Adiciona um novo anime",
                    OperationId = "AdicionaNovoAnime",
                    Tags = new[] { new OpenApiTag { Name = "Animes" } }
                });
        }

    }
}
