using Compras.Api.Common.Api;
using Compras.Core;
using Compras.Core.Handlers;
using Compras.Core.Models;
using Compras.Core.Requests.Itens;
using Compras.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Compras.Api.Endpoints.Itens;

public class GetAllItensEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync) 
            .WithName("Item: Recuperar por usuario")
            .WithSummary("Recupera todas os itens de um usuario")
            .WithDescription("Recupera todos os itens do usuário")
            .WithOrder(5)
            .Produces<PagedResponse<List<Item>?>>();

    public static async Task<IResult> HandleAsync(
        IItemHandler handler,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize
    )
    {
        var request = new GetAllItensRequest()
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