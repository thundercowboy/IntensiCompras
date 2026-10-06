using Compras.Api.Data;
using Compras.Api.Handlers;
using Compras.Api.Models;
using Compras.Core;
using Compras.Core.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Compras.Api.Common.Api;

public static class BuilderExtension
{
    public static void AddConfiguration(
        this WebApplicationBuilder builder)
    {
        Configuration.ConnectionString = 
            builder
                .Configuration
                .GetConnectionString("DefaultConnection") ?? String.Empty;
        Configuration.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? String.Empty;
        Configuration.FrontendUrl = builder.Configuration.GetValue<string>("FrontendUrl") ?? string.Empty;
    }

    public static void AddDocumentation(
        this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(x =>
        {
            x.CustomSchemaIds(n => n.FullName);
        });
    }

    public static void AddSecurity(
        this WebApplicationBuilder builder)
    {
        builder.Services
            .AddAuthentication(IdentityConstants.ApplicationScheme)
            .AddIdentityCookies();

        builder.Services.AddAuthorization();
    }

    public static void AddDataContexts(
        this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(x =>
        {
            x.UseSqlServer(Configuration.ConnectionString);
        });
        
        builder.Services
            .AddIdentityCore<User>()
            .AddRoles<IdentityRole<long>>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddApiEndpoints();
    }

    public static void AddServices(
        this WebApplicationBuilder builder)
    {
        builder
            .Services
            .AddTransient<IMaterialHandler, MaterialHandler>();
            
        builder
            .Services
            .AddTransient<ICategoriaItemHandler, CategoriaItemHandler>();
            
        builder
            .Services
            .AddTransient<IItemHandler, ItemHandler>();
            
        builder
            .Services
            .AddTransient<IFornecedorHandler, FornecedorHandler>();
            
        builder
            .Services
            .AddTransient<IAprovacaoHandler, AprovacaoHandler>();
            
        builder
            .Services
            .AddTransient<IUnidadeMedidaHandler, UnidadeMedidaHandler>();
            
        builder
            .Services
            .AddTransient<ISolicitacaoHandler, SolicitacaoHandler>();
    }

    public static void AddCrossOrigin(
        this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options => options.AddPolicy(
            ApiConfiguration.CorsPolicyName,
            policy => policy
                .WithOrigins([
                    Configuration.FrontendUrl,
                    Configuration.BackendUrl
                ])
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
            ));
    }
}