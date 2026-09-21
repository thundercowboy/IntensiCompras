using Compras.Api.Data;
using Compras.Core.Handlers;
using Compras.Core.Models;
using Compras.Core.Requests.Itens;
using Compras.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Compras.Api.Handlers;

public class ItemHandler(AppDbContext context) : IItensHandler
{
    public async Task<Response<Item?>> CreateAsync(CreateItemRequest request)
    {
        try
        {
            var item = new Item()
            {
                IdMaterial = request.IdMaterial,
                Quantidade = request.Quantidade,
                IdUnidade = request.IdUnidade,
                IdFornecedor = request.IdFornecedor,
                IdSolicitacao = request.IdSolicitacao
            };

            await context.Itens.AddAsync(item);
            await context.SaveChangesAsync();

            return new Response<Item?>(item, 201, "item criado com sucesso.");
        }
        catch 
        {
            return new Response<Item?>(null, 500, "Não foi possivel criar o item");
        }
    }

    public async Task<Response<Item?>> UpdateAsync(UpdateItemRequest request)
    {
        try
        {
            var item = await context
                .Itens
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (item is null)
                return new Response<Item?>(null, 404, "Item não encontrado");

            item.IdMaterial = request.IdMaterial;
            item.Quantidade = request.Quantidade;
            item.IdUnidade = request.IdUnidade;
            item.IdFornecedor = request.IdFornecedor;
            item.IdSolicitacao = request.IdSolicitacao;

            context.Itens.Update(item);
            await context.SaveChangesAsync();

            return new Response<Item?>(item);
        }
        catch
        {
            return new Response<Item?>(null, 500, "Não foi possivel atualizar o item");
        }
    }

    public async Task<Response<Item?>> DeleteAsync(DeleteItemRequest request)
    {
        try
        {
            var item = await context
                .Itens
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
            
            if (item is null)
                return new Response<Item?>(null, 404, "Item não encontrado");

            context.Itens.Remove(item);
            await context.SaveChangesAsync();

            return new Response<Item?>(item, 200, "Item removido com sucesso.");
        }
        catch
        {
            return new Response<Item?>(null, 500, "Não foi possivel deletar o item");
        }
    }

    public async Task<Response<Item?>> GetByIdAsync(GetItemByIdRequest request)
    {
        try
        {
            var item = await context
                .Itens
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            return item is null
                ? new Response<Item?>(null, 404, "Item não encontrado")
                : new Response<Item?>(item);
        }
        catch 
        {
            return new Response<Item?>(null, 500, "Não foi possivel recuperar o item");
        }
    }

    public async Task<PagedResponse<List<Item>>> GetAllAsync(GetAllItensRequest request)
    {
        try
        {
            

        }
        catch
        {
            return new PagedResponse<List<Item>>(null, 500, "Não foi possivel recuperar os itens");
        }
    }
}