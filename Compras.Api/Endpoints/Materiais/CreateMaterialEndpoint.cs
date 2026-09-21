using Compras.Api.Common.Api;
using Compras.Core.Handlers;
using Compras.Core.Requests.Materiais;

namespace Compras.Api.Endpoints.Materiais;

public class CreateMaterialEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync) 
            .WithName("Materiais: Criar")
            .WithSummary("Cria um novo material")
            .WithDescription("Cria um novo material")
            .WithOrder(1);

    public static async Task<IResult> HandleAsync(
        IMaterialHandler handler,
        CreateMaterialRequest request)
    {
        var result = await handler.CreateAsync(request);
        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data?.Id}", result.Data)
            : TypedResults.BadRequest(result.Data);
    }
}