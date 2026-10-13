using Compras.Core.Handlers;
using Compras.Core.Requests.Materiais;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Compras.Web.Pages.Material;

public partial class ListMaterialPage : ComponentBase
{
    #region Properties

    public bool IsBusy { get; set; }
    
    public List<Core.Models.Material> Materiais { get; set; } = [];
    
    public string SearchTerm { get; set; } = string.Empty;

    #endregion
    
    #region Services

    public ISnackbar Snackbar { get; set; } = null!;
    
    public IDialogService DialogService { get; set; } = null!;
    
    public IMaterialHandler Handler { get; set; } = null!;
    
    #endregion
    
    #region Methods

    public async Task OnValidSubmitAsync()
    {
        IsBusy = true;

        try
        {
            var request = new GetAllMateriaisRequest();
            var result = await Handler.GetAllAsync(request);
            if (result.IsSuccess)
                Materiais = result.Data ?? [];
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }
    
    #endregion

    public async void OnDeleteButtonClickedAsync(int id, string nome)
    {
        var result = await DialogService.ShowMessageBoxAsync(
            "ATENÇÃO",
            $"Ao prosseguir o material {nome} será excluido. Esta é uma ação irreversivel, deseja continuar?",
            yesText: "EXCLUIR",
            cancelText: "CANCELAR");
        
        if (result is true)
            await OnDeleteAsync(id, nome);
        
        StateHasChanged();
    }

    public async Task OnDeleteAsync(int id, string nome)
    {
        try
        {
            var request = new DeleteMaterialRequest { Id = id };
            await Handler.DeleteAsync(request);
            Materiais.RemoveAll(x => x.Id == id);
            Snackbar.Add($"Material {nome} removido com sucesso", Severity.Success);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }

    public Func<Core.Models.Material, bool> Filter => material =>
    {
        if (string.IsNullOrWhiteSpace(SearchTerm))
            return true;

        if (material.Id.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        if (material.Nome is not null &&
            material.Nome.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    };
}