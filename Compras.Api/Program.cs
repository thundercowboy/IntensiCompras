using Compras.Api.Data;
using Compras.Api.Endpoints;
using Compras.Api.Handlers;
using Compras.Core.Handlers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.CustomSchemaIds(n => n.FullName);
});

var cnnStr = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(cnnStr);
});

builder
    .Services
    .AddTransient<IMaterialHandler, MaterialHandler>()
    .AddTransient<ICategoriaItemHandler, CategoriaItemHandler>()
    .AddTransient<IItemHandler, ItemHandler>();

var app = builder.Build();

app.MapGet("/", () => new { message = "ok" });

app.MapEndpoints();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();
