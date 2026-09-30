using System.Security.Claims;
using Compras.Api.Common.Api;
using Compras.Core.Handlers;
using Compras.Core.Models;
using Compras.Core.Requests.Aprovacoes;
using Compras.Core.Responses;

namespace Compras.Api.Endpoints.Aprovacoes;

public class DeleteAprovacaoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandleAsync)
            .WithName("Aprovacoes: Deletar")
            .WithSummary("Deleta uma aprovacao")
            .WithDescription("Deleta uma aprovacao")
            .WithOrder(3)
            .Produces<Response<Aprovacao?>>();

    public static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IAprovacaoHandler handler,
        int id)
    {

        var request = new DeleteAprovacaoRequest()
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