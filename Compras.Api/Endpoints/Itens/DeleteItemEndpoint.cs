using System.Security.Claims;
using Compras.Api.Common.Api;
using Compras.Core.Handlers;
using Compras.Core.Models;
using Compras.Core.Requests.Itens;
using Compras.Core.Responses;

namespace Compras.Api.Endpoints.Itens;

public class DeleteItemEndpoint : IEndpoint
{
public static void Map(IEndpointRouteBuilder app)
    => app.MapDelete("/{id}", HandleAsync)
        .WithName("Itens: Deletar")
        .WithSummary("Deleta um item")
        .WithDescription("Deleta um item")
        .WithOrder(3)
        .Produces<Response<Item?>>();

public static async Task<IResult> HandleAsync(
    ClaimsPrincipal user,
    IItemHandler handler,
    int id)
{

    var request = new DeleteItemRequest()
    {
        UserId = user.Identity?.Name ?? string.Empty,
        Id = id
    };

    var result = await handler.DeleteAsync(request);
    return result.IsSuccess
        ? TypedResults.Ok(result)
        : TypedResults.BadRequest(result);
}
}