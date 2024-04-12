using Swashbuckle.AspNetCore.Annotations;

namespace AnimesProtech.Application.ConfigDoument
{
    public class AnimeDeleteOperationAttribute : SwaggerOperationAttribute
    {
        public AnimeDeleteOperationAttribute() : base()
        {
            this.Summary = "Deleta um anime existente";
            this.Description = "Forneça o ID do anime que deseja deletar.";
            this.OperationId = "DeletarAnime";
            this.Tags = new[] { "Animes" };
        }
    }
}