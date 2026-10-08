using System.Net.Http.Json;
using System.Text;
using Compras.Core.Handlers;
using Compras.Core.Requests.Account;
using Compras.Core.Responses;

namespace Compras.Web.Handlers;

public class AccountHandler(IHttpClientFactory httpClientFactory) : IAccountHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
    
    public async Task<Response<string>> LoginAsync(LoginRequest request)
    {
        var result = await _client.PostAsJsonAsync("v1/identity/login?useCookies=true", request);
        return result.IsSuccessStatusCode
            ? new Response<string>("Login realizado com sucesso!", 200, "Login realizado com sucesso!")
            : new Response<string>(null, 500, "Não foi possivel realizar o login");
    }

    public async Task<Response<string>> RegisterAsync(RegisterRequest request)
    {
        var result = await _client.PostAsJsonAsync("v1/identity/register", request);
        return result.IsSuccessStatusCode
            ? new Response<string>("Cadastro realizado com sucesso!", 200, "Cadastro realizado com sucesso!")
            : new Response<string>(null, 500, "Não foi possivel realizar o cadastro");
    }

    public Task LogoutAsync()
    {
        var emptyContent = new StringContent("{}", Encoding.UTF8, "/application/json");
        return _client.PostAsync("v1/identity/logout", emptyContent);
    }
}