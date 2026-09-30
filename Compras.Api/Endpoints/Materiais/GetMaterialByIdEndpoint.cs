using System.Security.Claims;
using Compras.Api.Common.Api;
using Compras.Core.Handlers;
using Compras.Core.Models;
using Compras.Core.Requests.Materiais;
using Compras.Core.Responses;

namespace Compras.Api.Endpoints.Materiais;

public class GetMaterialByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandleAsync) 
            .WithName("Materiais: Recuperar")
            .WithSummary("Recupera um material")
            .WithDescription("Recupera um material pelo ID")
            .WithOrder(4)
            .Produces<Response<Material?>>();

    public static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IMaterialHandler handler,
        int id)
    {

        var request = new GetMaterialByIdRequest()
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