using System.Security.Claims;
using Compras.Api.Common.Api;
using Compras.Core.Handlers;
using Compras.Core.Requests.UnidadesMedida;

namespace Compras.Api.Endpoints.UnidadesMedida;

public class CreateUnidadeMedidaEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync) 
            .WithName("Unidades: Criar")
            .WithSummary("Cria uma nova unidade de  medida")
            .WithDescription("Cria um nova unidade de medida")
            .WithOrder(1);

    public static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IUnidadeMedidaHandler handler,
        CreateUnidadeMedidaRequest request)
    {
        request.UserId = user.Identity?.Name ?? string.Empty;
        var result = await handler.CreateAsync(request);
        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data?.Id}", result.Data)
            : TypedResults.BadRequest(result.Data);
    }
}