using Compras.Api.Common.Api;
using Compras.Core;
using Compras.Core.Handlers;
using Compras.Core.Models;
using Compras.Core.Requests.UnidadesMedida;
using Compras.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Compras.Api.Endpoints.UnidadesMedida;

public class GetAllUnidadesMedidaEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync) 
            .WithName("Unidades: Recuperar por usuario")
            .WithSummary("Recupera todas as unidades de medida do usuário")
            .WithDescription("Recupera todas as unidades de medida do usuário")
            .WithOrder(5)
            .Produces<PagedResponse<List<UnidadeMedida>?>>();

    public static async Task<IResult> HandleAsync(
        IUnidadeMedidaHandler handler,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize
    )
    {
        var request = new GetAllUnidadesMedidaRequest()
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };
            
        var result = await handler.GetAllAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}