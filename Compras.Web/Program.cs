using Compras.Core.Handlers;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Compras.Web;
using Compras.Web.Handlers;
using Compras.Web.Security;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

Configuration.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<CookieHandler>();

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddScoped<AuthenticationStateProvider, CookieAuthenticationStateProvider>();
builder.Services.AddTransient<IAccountHandler, AccountHandler>();
builder.Services.AddTransient<IAprovacaoHandler, AprovacaoHandler>();
builder.Services.AddTransient<ICategoriaItemHandler, CategoriaItemHandler>();
builder.Services.AddTransient<IFornecedorHandler, FornecedorHandler>();
builder.Services.AddTransient<IItemHandler, ItemHandler>();
builder.Services.AddTransient<IMaterialHandler, MaterialHandler>();
builder.Services.AddTransient<ISolicitacaoHandler, SolicitacaoHandler>();
builder.Services.AddTransient<IUnidadeMedidaHandler, UnidadeMedidaHandler>();

builder.Services.AddScoped(x =>
    (ICookieAuthenticationStateProvider)x.GetRequiredService<AuthenticationStateProvider>());

builder.Services.AddHttpClient(Configuration.HttpClientName,
    opt =>
    {
        opt.BaseAddress = new Uri(Configuration.BackendUrl);
    }).AddHttpMessageHandler<CookieHandler>();
    
builder.Services.AddMudServices();

await builder.Build().RunAsync();
