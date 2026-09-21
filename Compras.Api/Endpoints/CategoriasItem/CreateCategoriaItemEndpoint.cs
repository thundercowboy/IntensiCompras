using Compras.Api.Common.Api;
using Compras.Core.Handlers;
using Compras.Core.Requests.CategoriasItem;

namespace Compras.Api.Endpoints.CategoriasItem;

public class CreateCategoriaItemEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync) 
            .WithName("Categorias: Criar")
            .WithSummary("Cria uma nova categoria")
            .WithDescription("Cria uma nova categoria")
            .WithOrder(1);

    public static async Task<IResult> HandleAsync(
        ICategoriaItemHandler handler,
        CreateCategoriaItemRequest request)
    {
        var result = await handler.CreateAsync(request);
        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data?.Id}", result.Data)
            : TypedResults.BadRequest(result.Data);
    }
}