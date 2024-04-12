using Swashbuckle.AspNetCore.Annotations;

namespace AnimesProtech.Application.ConfigDoument
{
    public class AnimePutOperationAttribute : SwaggerOperationAttribute
    {
        public AnimePutOperationAttribute() : base()
        {
            this.Summary = "Atualiza um anime existente";
            this.Description = "Forneça o ID do anime e o corpo do anime atualizado. " +
                "O corpo do anime deve incluir os campos `name`, `summary` e `director`.";
            this.OperationId = "AtualizarAnime";
            this.Tags = new[] { "Animes" };
        }
    }
}