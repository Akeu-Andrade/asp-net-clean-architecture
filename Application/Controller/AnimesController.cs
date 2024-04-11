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

        public AnimesController(
            IAddAnimeUseCase addAnimeUseCase,
            IGetAnimesUseCase getAnimesUseCase
        ) {
            _addAnimeUseCase = addAnimeUseCase;
            _getAnimesUseCase = getAnimesUseCase;
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

    }
}