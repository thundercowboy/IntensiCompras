using Compras.Api.Common.Api;
using Compras.Core.Handlers;
using Compras.Core.Models;
using Compras.Core.Requests.Fornecedores;
using Compras.Core.Responses;

namespace Compras.Api.Endpoints.Fornecedores;

public class UpdateFornecedorEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync) 
            .WithName("Fornecedores: Atualizar")
            .WithSummary("Atualiza um fornecedor")
            .WithDescription("Atualiza um fornecedor")
            .WithOrder(2)
            .Produces<Response<Fornecedor?>>();

    public static async Task<IResult> HandleAsync(
        IFornecedorHandler handler,
        UpdateFornecedorRequest request,
        int id)
    {
        request.Id = id;
            
        var result = await handler.UpdateAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
