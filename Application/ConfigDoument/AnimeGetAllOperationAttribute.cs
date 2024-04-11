using Swashbuckle.AspNetCore.Annotations;

namespace AnimesProtech.Application.ConfigDoument
{
    public class AnimeGetAllOperationAttribute : SwaggerOperationAttribute
    {
        public AnimeGetAllOperationAttribute() : base()
        {
            this.Summary = "Busca todos os animes";
            this.Description = "Você pode filtrar os resultados por diretor, nome e palavras no resumo. " +
                "Para filtrar os resultados, adicione os parâmetros de consulta `Director`, `Name` e `Keywords` à URL. " +
                "Você também pode usar os parâmetros `PageIndex` e `PageSize` para paginar os resultados.";
            this.OperationId = "BuscarListaAnimes";
            this.Tags = new[] { "Animes" };
        }

    }
}