using Compras.Api.Common.Api;
using Compras.Core.Handlers;
using Compras.Core.Models;
using Compras.Core.Requests.CategoriasItem;
using Compras.Core.Responses;

namespace Compras.Api.Endpoints.CategoriasItem;

public class UpdateCategoriaItemEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync) 
            .WithName("Categorias: Atualizar")
            .WithSummary("Atualiza uma categoria")
            .WithDescription("Atualiza uma categoria")
            .WithOrder(2)
            .Produces<Response<CategoriaItem?>>();

    public static async Task<IResult> HandleAsync(
        ICategoriaItemHandler handler,
        UpdateCategoriaItemRequest request,
        int id)
    {
        request.Id = id;
            
        var result = await handler.UpdateAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
