using Compras.Api.Common.Api;
using Compras.Core.Handlers;
using Compras.Core.Models;
using Compras.Core.Requests.Aprovacoes;
using Compras.Core.Responses;

namespace Compras.Api.Endpoints.Aprovacoes;

public class UpdateAprovacaoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync) 
            .WithName("Aprovacoes: Atualizar")
            .WithSummary("Atualiza uma aprovacao")
            .WithDescription("Atualiza uma aprovacao")
            .WithOrder(2)
            .Produces<Response<Aprovacao?>>();

    public static async Task<IResult> HandleAsync(
        IAprovacaoHandler handler,
        UpdateAprovacaoRequest request,
        int id)
    {
        request.Id = id;
            
        var result = await handler.UpdateAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}