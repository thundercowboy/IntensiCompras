using Compras.Api.Common.Api;
using Compras.Core.Handlers;
using Compras.Core.Models;
using Compras.Core.Requests.Fornecedores;
using Compras.Core.Responses;

namespace Compras.Api.Endpoints.Fornecedores;

public class DeleteFornecedorEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandleAsync)
            .WithName("Fornecedor: Deletar")
            .WithSummary("Deleta um fornecedor")
            .WithDescription("Deleta um fornecedor")
            .WithOrder(3)
            .Produces<Response<Fornecedor?>>();

    public static async Task<IResult> HandleAsync(
        IFornecedorHandler handler,
        int id)
    {

        var request = new DeleteFornecedorRequest()
        {
            Id = id
        };

        var result = await handler.DeleteAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}