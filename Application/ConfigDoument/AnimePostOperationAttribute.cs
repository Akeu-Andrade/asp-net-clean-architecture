using Swashbuckle.AspNetCore.Annotations;

namespace AnimesProtech.Application.ConfigDoument
{
    public class AnimePostOperationAttribute : SwaggerOperationAttribute
    {
        public AnimePostOperationAttribute() : base()
        {
            this.Summary = "Adiciona um novo anime";
            this.Description = "Adiciona um novo anime";
            this.OperationId = "AdicionaNovoAnime";
            this.Tags = new[] { "Animes" };
        }
    }
}