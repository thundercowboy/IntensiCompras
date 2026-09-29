using Compras.Api.Data;
using Compras.Core.Handlers;
using Compras.Core.Models;
using Compras.Core.Requests.Solicitacoes;
using Compras.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Compras.Api.Handlers;

public class SolicitacaoHandler(AppDbContext context) : ISolicitacaoHandler
{
    public async Task<Response<Solicitacao?>> CreateAsync(CreateSolicitacaoRequest request)
    {
        try
        {
            var solicitacao = new Solicitacao()
            {
                Nome = request.Nome,
                DataAtualizacao = request.DataAtualizacao,
                Descricao = request.Descricao,
                Setor = request.Setor,
                StatusSolicitacao = request.StatusSolicitacao,
                UserId = request.UserId
            };

            await context.AddAsync(solicitacao);
            await context.SaveChangesAsync();
            
            return new Response<Solicitacao?>(solicitacao, 201, "Solicitação criada com sucesso.");
        }
        catch
        {
            return new Response<Solicitacao?>(null, 500, "Não foi possivel criar a solicitação.");
        }
    }

    public async Task<Response<Solicitacao?>> UpdateAsync(UpdateSolicitacaoRequest request)
    {
        try
        {
            var solicitacao = await context
                .Solicitacoes.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (solicitacao == null)
                return new Response<Solicitacao?>(null, 404, "Solicitação não encontrada");
            
            solicitacao.Nome = request.Nome;
            solicitacao.DataAtualizacao = request.DataAtualizacao;
            solicitacao.Descricao = request.Descricao;
            solicitacao.Setor  = request.Setor;
            solicitacao.StatusSolicitacao = request.StatusSolicitacao;
            
            await context.SaveChangesAsync();
            
            return new Response<Solicitacao?>(solicitacao, message: "Solicitação atualizada com sucesso.");
            
        }
        catch
        {
            return new Response<Solicitacao?>(null, 500, "Não foi possivel atualizar a solicitação.");
        }
    }

    public async Task<Response<Solicitacao?>> DeleteAsync(DeleteSolicitacaoRequest request)
    {
        try
        {
            var solicitacao = await context
                .Solicitacoes
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (solicitacao == null)
                return new Response<Solicitacao?>(null, 404, "Solicitação não encontrada");

            context.Solicitacoes.Remove(solicitacao);
            await context.SaveChangesAsync();
            
            return new Response<Solicitacao?>(solicitacao, 200, "Solicitação excluida com sucesso.");
        }
        catch
        {
            return new Response<Solicitacao?>(null, 500, "Não foi possivel deletar a solicitação.");
        }
    }

    public async Task<Response<Solicitacao?>> GetByIdAsync(GetSolicitacaoByIdRequest request)
    {
        try
        {
            var solicitacao = await context
                .Solicitacoes
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            return solicitacao is null
                ? new Response<Solicitacao?>(null, 404, "Solicitação não encontrada")
                : new Response<Solicitacao?>(solicitacao);
        }
        catch 
        {
            return new Response<Solicitacao?>(null, 500, "Não foi possivel retornar a solicitação.");
        }
    }

    public async Task<PagedResponse<List<Solicitacao>>> GetAllAsync(GetAllSolicitacoesRequest request)
    {
        try
        {
            var query = context
                .Solicitacoes
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .OrderBy(x => x.Nome);

            var solicitacoes = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            
            var count = await query.CountAsync();
            
            return new PagedResponse<List<Solicitacao>>(
                solicitacoes,
                count, 
                request.PageNumber,
                request.PageSize);
        }
        catch
        {
            return new PagedResponse<List<Solicitacao>>(null, 500, "Não foi possivel consultar as solicitações.");
        }
    }
}