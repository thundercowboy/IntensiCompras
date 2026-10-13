using System.Net.Http.Json;
using Compras.Core.Handlers;
using Compras.Core.Models;
using Compras.Core.Requests.Solicitacoes;
using Compras.Core.Responses;

namespace Compras.Web.Handlers;

public class SolicitacaoHandler(IHttpClientFactory httpClientFactory) : ISolicitacaoHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
    public async Task<Response<Solicitacao?>> CreateAsync(CreateSolicitacaoRequest request)
    {
        var result = await _client.PostAsJsonAsync("v1/solicitacoes", request);
        return await result.Content.ReadFromJsonAsync<Response<Solicitacao?>>()
               ?? new Response<Solicitacao?>(null, 400, "Falha ao criar a solicitacao.");
    }

    public async Task<Response<Solicitacao?>> UpdateAsync(UpdateSolicitacaoRequest request)
    {
        var result = await _client.PutAsJsonAsync($"v1/solicitacoes/{request.Id}", request);
        return await result.Content.ReadFromJsonAsync<Response<Solicitacao?>>()
               ?? new Response<Solicitacao?>(null, 400, "Falha ao atualizar a solicitacao.");
    }

    public async Task<Response<Solicitacao?>> DeleteAsync(DeleteSolicitacaoRequest request)
    {
        var result = await _client.DeleteAsync($"v1/solicitacoes/{request.Id}");
        return await result.Content.ReadFromJsonAsync<Response<Solicitacao?>>()
               ?? new Response<Solicitacao?>(null, 400, "Falha ao excluir a solicitacao.");
    }

    public async Task<Response<Solicitacao?>> GetByIdAsync(GetSolicitacaoByIdRequest request)
        => await _client.GetFromJsonAsync<Response<Solicitacao?>>($"v1/solicitacoes/{request.Id}")
           ?? new Response<Solicitacao?>(null, 400, "Não foi possivel recuperar a solicitação.");

    public async Task<PagedResponse<List<Solicitacao>>> GetAllAsync(GetAllSolicitacoesRequest request)
        => await _client.GetFromJsonAsync<PagedResponse<List<Solicitacao>>>($"v1/solicitacoes")
           ?? new PagedResponse<List<Solicitacao>>(null, 400, "Não foi possivel recuperar as solicitacoes.");
}
