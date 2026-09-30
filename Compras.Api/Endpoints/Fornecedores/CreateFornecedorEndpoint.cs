using System.Security.Claims;
using Compras.Api.Common.Api;
using Compras.Core.Handlers;
using Compras.Core.Requests.Fornecedores;

namespace Compras.Api.Endpoints.Fornecedores;

public class CreateFornecedorEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync) 
            .WithName("Fornecedor: Criar")
            .WithSummary("Cria um novo fornecedor")
            .WithDescription("Cria um novo fornecedor")
            .WithOrder(1);

    public static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IFornecedorHandler handler,
        CreateFornecedorRequest request)
    {
        request.UserId = user.Identity?.Name ?? string.Empty;
        var result = await handler.CreateAsync(request);
        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data?.Id}", result.Data)
            : TypedResults.BadRequest(result.Data);
    }
}