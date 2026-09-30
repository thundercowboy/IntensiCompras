using System.Security.Claims;
using Compras.Api.Common.Api;
using Compras.Core.Handlers;
using Compras.Core.Models;
using Compras.Core.Requests.Fornecedores;
using Compras.Core.Responses;

namespace Compras.Api.Endpoints.Fornecedores;

public class GetFornecedorByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandleAsync) 
            .WithName("Fornecedores: Recuperar")
            .WithSummary("Recupera um fornecedor")
            .WithDescription("Recupera um fornecedor pelo ID")
            .WithOrder(4)
            .Produces<Response<Fornecedor?>>();

    public static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IFornecedorHandler handler,
        int id)
    {

        var request = new GetFornecedorByIdRequest()
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