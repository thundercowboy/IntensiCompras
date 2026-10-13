using Compras.Core.Handlers;
using Compras.Core.Requests.UnidadesMedida;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Compras.Web.Pages.UnidadesMedida;

public partial class EditUnidadePage : ComponentBase
{
    #region Properties
    
    public bool IsBusy { get; set; }
    public UpdateUnidadeMedidaRequest InputModel { get; set; } =  new UpdateUnidadeMedidaRequest();
    
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
    public IUnidadeMedidaHandler Handler { get; set; } = null!;

    #endregion
    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        GetUnidadeMedidaByIdRequest? request = null;
        try
        {
            request = new GetUnidadeMedidaByIdRequest()
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
                InputModel = new UpdateUnidadeMedidaRequest()
                {
                    Id = response.Data.Id,
                    Sigla = response.Data.Sigla,
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
                NavigationManager.NavigateTo("/unidades");
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