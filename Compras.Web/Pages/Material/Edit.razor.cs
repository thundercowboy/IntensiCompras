using Compras.Core.Handlers;
using Compras.Core.Requests.Materiais;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Compras.Web.Pages.Material;

public partial class EditMaterialPage : ComponentBase
{
    #region Properties
    
    public bool IsBusy { get; set; }
    public UpdateMaterialRequest InputModel { get; set; } =  new UpdateMaterialRequest();
    
    #endregion
    #region Parameters

    [Parameter]
    public string Id { get; set; } = string.Empty;

    #endregion

    #region Services

    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    public IMaterialHandler Handler { get; set; } = null!;

    #endregion
    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        GetMaterialByIdRequest? request = null;
        try
        {
            request = new GetMaterialByIdRequest
            {
                Id = long.Parse(Id)
            };
        }
        catch 
        {
            Snackbar.Add("Parametro inválido", Severity.Error);
        }

        if (request is null)
            return;
        
        IsBusy = true;
        try
        {
            var response = await Handler.GetByIdAsync(request);
            if (response is { IsSuccess: true, Data: not null })
                InputModel = new UpdateMaterialRequest()
                {
                    Id = response.Data.Id,
                    CodigoMaterial = response.Data.CodigoMaterial,
                    Nome = response.Data.Nome
                };

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
    
    #region Methods

    public async void OnValidSubmitAsync()
    {
        IsBusy = true;

        try
        {
            var result = await Handler.UpdateAsync(InputModel);
            if (result.IsSuccess)
            {
                Snackbar.Add(result.Message, Severity.Success);
                NavigationManager.NavigateTo("/materiais");
            }
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
}