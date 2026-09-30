using System.Security.Claims;
using Compras.Api.Common.Api;
using Compras.Core.Handlers;
using Compras.Core.Models;
using Compras.Core.Requests.UnidadesMedida;
using Compras.Core.Responses;

namespace Compras.Api.Endpoints.UnidadesMedida;

public class DeleteUnidadeMedidaEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandleAsync)
            .WithName("Unidades: Deletar")
            .WithSummary("Deleta uma unidade de medida")
            .WithDescription("Deleta uma unidade de medida")
            .WithOrder(3)
            .Produces<Response<UnidadeMedida?>>();

    public static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IUnidadeMedidaHandler handler,
        int id)
    {

        var request = new DeleteUnidadeMedidaRequest()
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