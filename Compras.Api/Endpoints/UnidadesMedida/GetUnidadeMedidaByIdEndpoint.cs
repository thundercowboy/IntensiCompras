using Compras.Api.Common.Api;
using Compras.Core.Handlers;
using Compras.Core.Models;
using Compras.Core.Requests.UnidadesMedida;
using Compras.Core.Responses;

namespace Compras.Api.Endpoints.UnidadesMedida;

public class GetUnidadeMedidaByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandleAsync) 
            .WithName("Unidades: Recuperar")
            .WithSummary("Recupera uma unidade de medida")
            .WithDescription("Recupera uma unidade de medida pelo ID")
            .WithOrder(4)
            .Produces<Response<UnidadeMedida?>>();

    public static async Task<IResult> HandleAsync(
        IUnidadeMedidaHandler handler,
        int id)
    {

        var request = new GetUnidadeMedidaByIdRequest()
        {
            Id = id,
        };
        
        var result = await handler.GetByIdAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}