using Compras.Api.Common.Api;
using Compras.Core.Handlers;
using Compras.Core.Models;
using Compras.Core.Requests.Materiais;
using Compras.Core.Responses;

namespace Compras.Api.Endpoints.Materiais;

public class UpdateMaterialEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
            => app.MapPut("/{id}", HandleAsync) 
                .WithName("Materiais: Atualizar")
                .WithSummary("Atualiza um material")
                .WithDescription("Atualiza um material")
                .WithOrder(2)
                .Produces<Response<Material?>>();

        public static async Task<IResult> HandleAsync(
            IMaterialHandler handler,
            UpdateMaterialRequest request,
            int id)
        {
            request.Id = id;
            
            var result = await handler.UpdateAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
}
