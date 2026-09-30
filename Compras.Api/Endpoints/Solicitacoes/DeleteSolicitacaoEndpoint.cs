using System.Security.Claims;
using Compras.Api.Common.Api;
using Compras.Core.Handlers;
using Compras.Core.Models;
using Compras.Core.Requests.Solicitacoes;
using Compras.Core.Responses;

namespace Compras.Api.Endpoints.Solicitacoes;

public class DeleteSolicitacaoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandleAsync)
            .WithName("Solicitações: Deletar")
            .WithSummary("Deleta uma solicitação")
            .WithDescription("Deleta uma solicitação")
            .WithOrder(3)
            .Produces<Response<Solicitacao?>>();

    public static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ISolicitacaoHandler handler,
        int id)
    {
        
        var request = new DeleteSolicitacaoRequest()
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