using Microsoft.OpenApi.Models;

namespace AnimesProtech.Application.ConfigDoument
{
    public static class OpenApiConfigurations
    {
        public static OpenApiOperation AnimePostOperation(OpenApiOperation operation)
        {
            operation.Description = "Adiciona um novo anime";
            operation.OperationId = "AdicionaNovoAnime";
            operation.Tags = new[] { new OpenApiTag { Name = "Animes" } };
            return operation;
        }
    }
}