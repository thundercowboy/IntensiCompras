using System.Security.Claims;
using Compras.Api.Common.Api;
using Compras.Core;
using Compras.Core.Handlers;
using Compras.Core.Models;
using Compras.Core.Requests.Solicitacoes;
using Compras.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Compras.Api.Endpoints.Solicitacoes;

public class GetAllSolicitacoesEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync) 
            .WithName("Solicitações: Recuperar por usuario")
            .WithSummary("Recupera todas as solicitações do usuário")
            .WithDescription("Recupera todos as solicitações do usuário")
            .WithOrder(5)
            .Produces<PagedResponse<List<Solicitacao>?>>();

    public static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ISolicitacaoHandler handler,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize
    )
    {
        var request = new GetAllSolicitacoesRequest()
        {
            UserId = user.Identity?.Name ?? string.Empty,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
            
        var result = await handler.GetAllAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}