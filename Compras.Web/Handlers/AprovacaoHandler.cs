using System.Net.Http.Json;
using Compras.Core.Handlers;
using Compras.Core.Models;
using Compras.Core.Requests.Aprovacoes;
using Compras.Core.Responses;

namespace Compras.Web.Handlers;

public class AprovacaoHandler(IHttpClientFactory httpClientFactory) : IAprovacaoHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
    
    public async Task<Response<Aprovacao?>> CreateAsync(CreateAprovacaoRequest request)
    {
        var result = await _client.PostAsJsonAsync("v1/aprovacoes", request);
        return await result.Content.ReadFromJsonAsync<Response<Aprovacao?>>()
            ?? new Response<Aprovacao?>(null, 400, "Falha ao criar a aprovação.");
    }

    public async Task<Response<Aprovacao?>> UpdateAsync(UpdateAprovacaoRequest request)
    {
        var result = await _client.PutAsJsonAsync($"v1/aprovacoes/{request.Id}", request);
        return await result.Content.ReadFromJsonAsync<Response<Aprovacao?>>()
            ?? new Response<Aprovacao?>(null, 400, "Falha ao atualizar a aprovação.");
    }

    public async Task<Response<Aprovacao?>> DeleteAsync(DeleteAprovacaoRequest request)
    {
        var result = await _client.DeleteAsync($"v1/aprovacoes/{request.Id}");
        return await result.Content.ReadFromJsonAsync<Response<Aprovacao?>>()
            ?? new Response<Aprovacao?>(null, 400, "Falha ao excluir a aprovação.");
    }

    public async Task<Response<Aprovacao?>> GetByIdAsync(GetAprovacaoByIdRequest request)
        => await _client.GetFromJsonAsync<Response<Aprovacao?>>($"v1/aprovacoes/{request.Id}")
        ?? new Response<Aprovacao?>(null, 400, "Não foi possivel recuperar a aprovação.");

    public async Task<PagedResponse<List<Aprovacao>>> GetAllAsync(GetAllAprovacoesRequest request)
        => await _client.GetFromJsonAsync<PagedResponse<List<Aprovacao>>>($"v1/aprovacoes")
           ?? new PagedResponse<List<Aprovacao>>(null, 400, "Não foi possivel recuperar as aprovações.");
}