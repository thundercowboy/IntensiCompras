using Compras.Api.Common.Api;
using Compras.Core.Handlers;
using Compras.Core.Models;
using Compras.Core.Requests.Materiais;
using Compras.Core.Responses;

namespace Compras.Api.Endpoints.Materiais;

public class DeleteMaterialEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandleAsync)
            .WithName("Materiais: Deletar")
            .WithSummary("Deleta um material")
            .WithDescription("Deleta um material")
            .WithOrder(3)
            .Produces<Response<Material?>>();

    public static async Task<IResult> HandleAsync(
        IMaterialHandler handler,
        int id)
    {

        var request = new DeleteMaterialRequest()
        {
            Id = id
        };

        var result = await handler.DeleteAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}