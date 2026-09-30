using System.Security.Claims;
using Compras.Api.Common.Api;
using Compras.Core.Handlers;
using Compras.Core.Requests.Itens;

namespace Compras.Api.Endpoints.Itens;

public class CreateItemEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync) 
            .WithName("Itens: Criar")
            .WithSummary("Cria um novo item")
            .WithDescription("Cria um novo item")
            .WithOrder(1);

    public static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IItemHandler handler,
        CreateItemRequest request)
    {
        request.UserId = user.Identity?.Name ?? string.Empty;
        var result = await handler.CreateAsync(request);
        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data?.Id}", result.Data)
            : TypedResults.BadRequest(result.Data);
    }
}