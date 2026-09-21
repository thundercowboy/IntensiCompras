using Compras.Api.Data;
using Compras.Core.Handlers;
using Compras.Core.Models;
using Compras.Core.Requests.Materiais;
using Compras.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Compras.Api.Handlers;

public class MaterialHandler(AppDbContext context) : IMaterialHandler
{
    public async Task<Response<Material?>> CreateAsync(CreateMaterialRequest request)
    {
        try
        {
            var material = new Material()
            {
                UserId = request.UserId,
                CodigoMaterial = request.CodigoMaterial,
                Nome = request.Nome
            };

            await context.AddAsync(material);
            await context.SaveChangesAsync();
            
            return new Response<Material?>(material, 201, "Material criado com sucesso.");
        }
        catch
        {
            return new Response<Material?>(null, 500, "Não foi possivel criar o material.");
        }
    }

    public async Task<Response<Material?>> UpdateAsync(UpdateMaterialRequest request)
    {
        try
        {
            var material = await context
                .Materiais.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (material == null)
                return new Response<Material?>(null, 404, "Material não encontrado");
            
            material.CodigoMaterial = request.CodigoMaterial;
            material.Nome = request.Nome;

            await context.SaveChangesAsync();
            
            return new Response<Material?>(material, message: "Material atualizado com sucesso.");
            
        }
        catch
        {
            return new Response<Material?>(null, 500, "Não foi possivel  atualizar o material.");
        }
    }

    public async Task<Response<Material?>> DeleteAsync(DeleteMaterialRequest request)
    {
        try
        {
            var material = await context
                .Materiais
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (material == null)
                return new Response<Material?>(null, 404, "Material não encontrado");

            context.Materiais.Remove(material);
            await context.SaveChangesAsync();
            
            return new Response<Material?>(material, 200, "Material excluido com sucesso.");
        }
        catch
        {
            return new Response<Material?>(null, 500, "Não foi possivel deletar o Material.");
        }
    }

    public async Task<Response<Material?>> GetByIdAsync(GetMaterialByIdRequest request)
    {
        try
        {
            var material = await context
                .Materiais
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            return material is null
                ? new Response<Material?>(null, 404, "Material não encontrado")
                : new Response<Material?>(material);
        }
        catch 
        {
            return new Response<Material?>(null, 500, "Não foi possivel retornar o material.");
        }
    }

    public async Task<PagedResponse<List<Material>>> GetAllAsync(GetAllMateriaisRequest request)
    {
        try
        {
            var query = context
                .Materiais
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .OrderBy(x => x.Nome);

            var materiais = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            
            var count = await query.CountAsync();
            
            return new PagedResponse<List<Material>>(
                materiais,
                count, 
                request.PageNumber,
                request.PageSize);
        }
        catch
        {
            return new PagedResponse<List<Material>>(null, 500, "Não foi possivel consultar os materiais");
        }
    }
}