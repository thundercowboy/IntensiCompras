using Compras.Api.Common.Api;
using Compras.Api.Endpoints.Aprovacoes;
using Compras.Api.Endpoints.CategoriasItem;
using Compras.Api.Endpoints.Fornecedores;
using Compras.Api.Endpoints.Identity;
using Compras.Api.Endpoints.Itens;
using Compras.Api.Endpoints.Materiais;
using Compras.Api.Endpoints.Solicitacoes;
using Compras.Api.Endpoints.UnidadesMedida;
using Compras.Api.Models;

namespace Compras.Api.Endpoints;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app
            .MapGroup("");
        
        endpoints.MapGroup("")
            .WithTags("health check")
            .MapGet("/", () => new { message = "ok" });
        
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

        endpoints.MapGroup("v1/fornecedores")
            .WithTags("fornecedores")
            .RequireAuthorization()
            .MapEndpoint<CreateFornecedorEndpoint>()
            .MapEndpoint<UpdateFornecedorEndpoint>()
            .MapEndpoint<DeleteFornecedorEndpoint>()
            .MapEndpoint<GetFornecedorByIdEndpoint>()
            .MapEndpoint<GetAllFornecedoresEndpoint>();
        
        endpoints.MapGroup("v1/aprovacoes")
            .WithTags("aprovacoes")
            .RequireAuthorization()
            .MapEndpoint<CreateAprovacaoEndpoint>()
            .MapEndpoint<UpdateAprovacaoEndpoint>()
            .MapEndpoint<DeleteAprovacaoEndpoint>()
            .MapEndpoint<GetAprovacaoByIdEndpoint>()
            .MapEndpoint<GetAllAprovacoesEndpoint>();

        endpoints.MapGroup("v1/unidades")
            .WithTags("unidades")
            .RequireAuthorization()
            .MapEndpoint<CreateUnidadeMedidaEndpoint>()
            .MapEndpoint<UpdateUnidadeMedidaEndpoint>()
            .MapEndpoint<DeleteUnidadeMedidaEndpoint>()
            .MapEndpoint<GetUnidadeMedidaByIdEndpoint>()
            .MapEndpoint<GetAllUnidadesMedidaEndpoint>();
        
        endpoints.MapGroup("v1/solicitacoes")
            .WithTags("solicitacoes")
            .RequireAuthorization()
            .MapEndpoint<CreateSolicitacaoEndpoint>()
            .MapEndpoint<UpdateSolicitacaoEndpoint>()
            .MapEndpoint<DeleteSolicitacaoEndpoint>()
            .MapEndpoint<GetSolicitacaoByIdEndpoint>()
            .MapEndpoint<GetAllSolicitacoesEndpoint>();

        endpoints.MapGroup("v1/identity")
            .WithTags("Identity")
            .MapIdentityApi<User>();
        
        endpoints.MapGroup("v1/identity")
            .WithTags("Identity")
            .MapEndpoint<LogoutEndpoint>()
            .MapEndpoint<GetRolesEndpoint>();
    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
        where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);
        return app;
    }
}