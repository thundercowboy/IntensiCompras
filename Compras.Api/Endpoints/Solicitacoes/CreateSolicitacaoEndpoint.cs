using System.Security.Claims;
using Compras.Api.Common.Api;
using Compras.Core.Handlers;
using Compras.Core.Requests.Solicitacoes;

namespace Compras.Api.Endpoints.Solicitacoes;

public class CreateSolicitacaoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync) 
            .WithName("Solicitações: Criar")
            .WithSummary("Cria uma nova solicitação")
            .WithDescription("Cria uma nova solicitação")
            .WithOrder(1);

    public static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ISolicitacaoHandler handler,
        CreateSolicitacaoRequest request)
    {
        request.UserId = user.Identity?.Name ?? string.Empty;
        var result = await handler.CreateAsync(request);
        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data?.Id}", result.Data)
            : TypedResults.BadRequest(result.Data);
    }
}