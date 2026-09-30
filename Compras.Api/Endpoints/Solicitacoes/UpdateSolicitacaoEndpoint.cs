using System.Security.Claims;
using Compras.Api.Common.Api;
using Compras.Core.Handlers;
using Compras.Core.Models;
using Compras.Core.Requests.Solicitacoes;
using Compras.Core.Responses;

namespace Compras.Api.Endpoints.Solicitacoes;

public class UpdateSolicitacaoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync) 
            .WithName("Solicitações: Atualizar")
            .WithSummary("Atualiza uma solicitação")
            .WithDescription("Atualiza uma solicitação")
            .WithOrder(2)
            .Produces<Response<Solicitacao?>>();

    public static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ISolicitacaoHandler handler,
        UpdateSolicitacaoRequest request,
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