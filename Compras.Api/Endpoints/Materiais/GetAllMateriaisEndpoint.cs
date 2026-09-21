using Compras.Api.Common.Api;
using Compras.Core;
using Compras.Core.Handlers;
using Compras.Core.Models;
using Compras.Core.Requests.Materiais;
using Compras.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Compras.Api.Endpoints.Materiais;

public class GetAllMateriaisEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync) 
            .WithName("Materiais: Recuperar por usuario")
            .WithSummary("Recupera todos os materiais do usuário")
            .WithDescription("Recupera todos os materiais do usuário")
            .WithOrder(5)
            .Produces<PagedResponse<List<Material>?>>();

    public static async Task<IResult> HandleAsync(
        IMaterialHandler handler,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize
        )
    {
        var request = new GetAllMateriaisRequest()
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };
            
        var result = await handler.GetAllAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}