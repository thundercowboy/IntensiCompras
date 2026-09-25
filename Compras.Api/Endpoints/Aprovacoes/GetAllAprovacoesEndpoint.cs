using Compras.Api.Common.Api;
using Compras.Core;
using Compras.Core.Handlers;
using Compras.Core.Models;
using Compras.Core.Requests.Aprovacoes;
using Compras.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Compras.Api.Endpoints.Aprovacoes;

public class GetAllAprovacoesEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync) 
            .WithName("Aprovacoes: Recuperar por usuario")
            .WithSummary("Recupera todas as aprovacoes do usuário")
            .WithDescription("Recupera todos as aprovacoes do usuário")
            .WithOrder(5)
            .Produces<PagedResponse<List<Aprovacao>?>>();

    public static async Task<IResult> HandleAsync(
        IAprovacaoHandler handler,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize
    )
    {
        var request = new GetAllAprovacoesRequest()
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