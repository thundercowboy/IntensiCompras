using Compras.Api.Common.Api;
using Compras.Api.Endpoints.CategoriasItem;
using Compras.Api.Endpoints.Itens;
using Compras.Api.Endpoints.Materiais;

namespace Compras.Api.Endpoints;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app
            .MapGroup("");
        
        endpoints.MapGroup("v1/materiais")
            .WithTags("materiais")
            .RequireAuthorization()
            .MapEndpoint<CreateMaterialEndpoint>()
            .MapEndpoint<UpdateMaterialEndpoint>()
            .MapEndpoint<DeleteMaterialEndpoint>()
            .MapEndpoint<GetMaterialByIdEndpoint>()
            .MapEndpoint<GetAllMateriaisEndpoint>();
        
        endpoints.MapGroup("v1/categorias")
            .WithTags("categorias")
            .RequireAuthorization()
            .MapEndpoint<CreateCategoriaItemEndpoint>()
            .MapEndpoint<UpdateCategoriaItemEndpoint>()
            .MapEndpoint<DeleteCategoriaItemEndpoint>()
            .MapEndpoint<GetCategoriaItemByIdEndpoint>()
            .MapEndpoint<GetAllCategoriasItemEndpoint>();
        
        endpoints.MapGroup("v1/itens")
            .WithTags("itens")
            .RequireAuthorization()
            .MapEndpoint<CreateItemEndpoint>()
            .MapEndpoint<UpdateItemEndpoint>()
            .MapEndpoint<DeleteItemEndpoint>()
            .MapEndpoint<GetItemByIdEndpoint>()
            .MapEndpoint<GetAllItensEndpoint>();
    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
        where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);
        return app;
    }
}