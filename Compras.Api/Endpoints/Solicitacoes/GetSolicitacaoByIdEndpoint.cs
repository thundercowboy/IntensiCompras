using System.Security.Claims;
using Compras.Api.Common.Api;
using Compras.Core.Handlers;
using Compras.Core.Models;
using Compras.Core.Requests.Solicitacoes;
using Compras.Core.Responses;

namespace Compras.Api.Endpoints.Solicitacoes;

public class GetSolicitacaoByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandleAsync) 
            .WithName("Solicitações: Recuperar")
            .WithSummary("Recupera uma solicitação")
            .WithDescription("Recupera uma solicitação pelo ID")
            .WithOrder(4)
            .Produces<Response<Solicitacao?>>();

    public static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ISolicitacaoHandler handler,
        int id)
    {

        var request = new GetSolicitacaoByIdRequest()
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