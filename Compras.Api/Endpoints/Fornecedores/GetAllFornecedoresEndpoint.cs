using System.Security.Claims;
using Compras.Api.Common.Api;
using Compras.Core;
using Compras.Core.Handlers;
using Compras.Core.Models;
using Compras.Core.Requests.Fornecedores;
using Compras.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Compras.Api.Endpoints.Fornecedores;

public class GetAllFornecedoresEndpoint  : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync) 
            .WithName("Fornecedor: Recuperar por usuario")
            .WithSummary("Recupera todos os fornecedores do usuário")
            .WithDescription("Recupera todos os fornecedores do usuário")
            .WithOrder(5)
            .Produces<PagedResponse<List<Fornecedor>?>>();

    public static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IFornecedorHandler handler,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize
    )
    {
        var request = new GetAllFornecedoresRequest()
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