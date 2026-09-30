using System.Security.Claims;
using Compras.Api.Common.Api;
using Compras.Core.Handlers;
using Compras.Core.Requests.CategoriasItem;
using Compras.Core.Responses;

namespace Compras.Api.Endpoints.CategoriasItem;

public class DeleteCategoriaItemEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandleAsync)
            .WithName("Categorias: Deletar")
            .WithSummary("Deleta uma categoria")
            .WithDescription("Deleta uma categoria")
            .WithOrder(3)
            .Produces<Response<Core.Models.CategoriaItem?>>();

    public static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ICategoriaItemHandler handler,
        int id)
    {

        var request = new DeleteCategoriaItemRequest()
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