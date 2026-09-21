using Compras.Api.Common.Api;
using Compras.Core.Handlers;
using Compras.Core.Models;
using Compras.Core.Requests.Itens;
using Compras.Core.Responses;

namespace Compras.Api.Endpoints.Itens;

public class UpdateItemEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync) 
            .WithName("Itens: Atualizar")
            .WithSummary("Atualiza um item")
            .WithDescription("Atualiza um item")
            .WithOrder(2)
            .Produces<Response<Item?>>();

    public static async Task<IResult> HandleAsync(
        IItemHandler handler,
        UpdateItemRequest request,
        int id)
    {
        request.Id = id;
            
        var result = await handler.UpdateAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
