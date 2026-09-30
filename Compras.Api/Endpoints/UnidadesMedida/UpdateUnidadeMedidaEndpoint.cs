using System.Security.Claims;
using Compras.Api.Common.Api;
using Compras.Core.Handlers;
using Compras.Core.Models;
using Compras.Core.Requests.UnidadesMedida;
using Compras.Core.Responses;

namespace Compras.Api.Endpoints.UnidadesMedida;

public class UpdateUnidadeMedidaEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync)
            .WithName("Unidades: Atualizar")
            .WithSummary("Atualiza uma unidade de medida")
            .WithDescription("Atualiza uma unidade de medida")
            .WithOrder(2)
            .Produces<Response<UnidadeMedida?>>();

    public static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IUnidadeMedidaHandler handler,
        UpdateUnidadeMedidaRequest request,
        int id)
    {
        request.UserId = user.Identity?.Name ?? string.Empty;
        request.Id = id;

        var result = await handler.UpdateAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}