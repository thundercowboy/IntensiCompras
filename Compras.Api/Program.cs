using System.Security.Claims;
using Compras.Api.Data;
using Compras.Api.Endpoints;
using Compras.Api.Handlers;
using Compras.Api.Models;
using Compras.Core.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.CustomSchemaIds(n => n.FullName);
});

builder.Services
    .AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddIdentityCookies();
builder.Services.AddAuthorization();

builder.Services
    .AddIdentityCore<User>()
    .AddRoles<IdentityRole<long>>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddApiEndpoints();

var cnnStr = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(cnnStr);
});

builder
    .Services
    .AddTransient<IMaterialHandler, MaterialHandler>()
    .AddTransient<ICategoriaItemHandler, CategoriaItemHandler>()
    .AddTransient<IItemHandler, ItemHandler>()
    .AddTransient<IFornecedorHandler, FornecedorHandler>()
    .AddTransient<IAprovacaoHandler, AprovacaoHandler>()
    .AddTransient<IUnidadeMedidaHandler, UnidadeMedidaHandler>()
    .AddTransient<ISolicitacaoHandler, SolicitacaoHandler>();

var app = builder.Build();

app.MapGet("/", () => new { message = "ok" });

app.MapEndpoints();
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapGroup("v1/identity")
    .WithTags("Identity")
    .MapIdentityApi<User>();

app.MapGroup("v1/logout")
    .WithTags("Identity")
    .MapPost("/logout", async (SignInManager<User> signInManager) =>
    {
        await signInManager.SignOutAsync();
        return Results.Ok();
    })
    .RequireAuthorization();

app.MapGroup("v1/identity")
    .WithTags("Identity")
    .MapGet("/roles", (ClaimsPrincipal user) =>
    {
        if (user.Identity is null || !user.Identity.IsAuthenticated)
            return Results.Unauthorized();
        
        var identity = (ClaimsIdentity) user.Identity;
        var roles = identity.FindAll(identity.RoleClaimType)
            .Select(c => new
            {
                c.Issuer,
                c.OriginalIssuer,
                c.Type,
                c.Value,
                c.ValueType
            });
        
        return TypedResults.Json(roles);
    })
    .RequireAuthorization();

app.Run();
