using Compras.Api.Common.Api;
using Compras.Core;
using Compras.Core.Handlers;
using Compras.Core.Requests.CategoriasItem;
using Compras.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Compras.Api.Endpoints.CategoriasItem;

public class GetAllCategoriasItemEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync) 
            .WithName("Categoria: Recuperar por usuario")
            .WithSummary("Recupera todas as categorias do usuário")
            .WithDescription("Recupera todos as categorias do usuário")
            .WithOrder(5)
            .Produces<PagedResponse<List<Core.Models.CategoriaItem>?>>();

    public static async Task<IResult> HandleAsync(
        ICategoriaItemHandler handler,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize
    )
    {
        var request = new GetAllCategoriasItemRequest()
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