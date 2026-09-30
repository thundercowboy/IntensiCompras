using System.Security.Claims;
using Compras.Api.Common.Api;
using Compras.Core.Handlers;
using Compras.Core.Requests.Aprovacoes;

namespace Compras.Api.Endpoints.Aprovacoes;

public class CreateAprovacaoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync) 
            .WithName("Aprovacoes: Criar")
            .WithSummary("Cria uma nova aprovacao")
            .WithDescription("Cria uma nova aprovacao")
            .WithOrder(1);

    public static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IAprovacaoHandler handler,
        CreateAprovacaoRequest request)
    {
        request.UserId = user.Identity?.Name ?? string.Empty;
        var result = await handler.CreateAsync(request);
        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data?.Id}", result.Data)
            : TypedResults.BadRequest(result.Data);
    }
}