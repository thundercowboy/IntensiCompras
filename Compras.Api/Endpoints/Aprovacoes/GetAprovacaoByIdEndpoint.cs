using Compras.Api.Common.Api;
using Compras.Core.Handlers;
using Compras.Core.Models;
using Compras.Core.Requests.Aprovacoes;
using Compras.Core.Responses;

namespace Compras.Api.Endpoints.Aprovacoes;

public class GetAprovacaoByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandleAsync) 
            .WithName("Aprovacoes: Recuperar")
            .WithSummary("Recupera uma aprovacao")
            .WithDescription("Recupera uma aprovacao pelo ID")
            .WithOrder(4)
            .Produces<Response<Aprovacao?>>();

    public static async Task<IResult> HandleAsync(
        IAprovacaoHandler handler,
        int id)
    {

        var request = new GetAprovacaoByIdRequest()
        {
            Id = id,
        };
        
        var result = await handler.GetByIdAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}