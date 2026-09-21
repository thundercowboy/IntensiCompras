using Compras.Api.Common.Api;
using Compras.Core.Handlers;
using Compras.Core.Requests.CategoriasItem;
using Compras.Core.Responses;

namespace Compras.Api.Endpoints.CategoriasItem;

public class GetCategoriaItemByIdEndpoint: IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandleAsync) 
            .WithName("Categorias: Recuperar")
            .WithSummary("Recupera uma categoria")
            .WithDescription("Recupera uma categoria pelo ID")
            .WithOrder(4)
            .Produces<Response<Core.Models.CategoriaItem?>>();

    public static async Task<IResult> HandleAsync(
        ICategoriaItemHandler handler,
        int id)
    {

        var request = new GetCategoriaItemByIdRequest()
        {
            Id = id,
        };
        
        var result = await handler.GetByIdAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}