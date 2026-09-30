using System.Security.Claims;
using Compras.Api.Common.Api;
using Compras.Core.Handlers;
using Compras.Core.Models;
using Compras.Core.Requests.Itens;
using Compras.Core.Responses;

namespace Compras.Api.Endpoints.Itens;

public class GetItemByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandleAsync) 
            .WithName("Itens: Recuperar")
            .WithSummary("Recupera um item")
            .WithDescription("Recupera um item pelo ID")
            .WithOrder(4)
            .Produces<Response<Item?>>();

    public static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IItemHandler handler,
        int id)
    {

        var request = new GetItemByIdRequest()
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Id = id,
        };
        
        var result = await handler.GetByIdAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}