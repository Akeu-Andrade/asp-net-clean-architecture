using AnimesProtech.Application.ConfigDoument;
using AnimesProtech.Domain.Entities;
using AnimesProtech.Domain.Interfaces.UseCases;
using AnimesProtech.Domain.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnimesProtech.Application.Controllers
{
    [ApiController]
    [Route("/api/v1/animes")]
    [Authorize]
    public class AnimesController : ControllerBase
    {
        private readonly IAddAnimeUseCase _addAnimeUseCase;
        private readonly IGetAnimesUseCase _getAnimesUseCase;
        private readonly IUpdateAnimeUseCase _updateAnimeUseCase;
        private readonly IDeleteAnimeUseCase _deleteAnimeUseCase;

        public AnimesController(
            IAddAnimeUseCase addAnimeUseCase,
            IGetAnimesUseCase getAnimesUseCase,            
            IUpdateAnimeUseCase updateAnimeUseCase,
            IDeleteAnimeUseCase deleteAnimeUseCase
        ) {
            _addAnimeUseCase = addAnimeUseCase;
            _getAnimesUseCase = getAnimesUseCase;
            _updateAnimeUseCase = updateAnimeUseCase;
            _deleteAnimeUseCase = deleteAnimeUseCase;
        }

        [HttpPost]
        [AnimePostOperation]
        public async Task<IActionResult> Post(Anime anime)
        {
            var addedAnime = await _addAnimeUseCase.Execute(anime);
            return Created($"/api/v1/animes/{addedAnime.id}", addedAnime);
        }

        [HttpGet]
        [AnimeGetAllOperation]
        public async Task<ActionResult<List<Anime>>> Get([FromQuery] AnimeSearchCriteria criteria)
        {
            var animes = await _getAnimesUseCase.Execute(criteria);
            return Ok(animes);
        }

        [HttpPut("{id}")]
        [AnimePutOperation]
        public async Task<IActionResult> Put(Guid id, Anime anime)
        {
            if (id != anime.id)
            {
                return BadRequest();
            }

            var updatedAnime = await _updateAnimeUseCase.Execute(anime);

            return Ok(updatedAnime);
        }

    }
}