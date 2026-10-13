using Compras.Core.Handlers;
using Compras.Core.Requests.Materiais;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Compras.Web.Pages.Material;

public partial class CreateMaterialPage : ComponentBase
{
    #region Properties

    public bool IsBusy { get; set; } = false;
    public CreateMaterialRequest InputModel { get; set; } = new();

    #endregion

    #region Services

    [Inject] public IMaterialHandler Handler { get; set; } = null!;

    [Inject] public NavigationManager NavigationManager { get; set; } = null!;

    [Inject] public ISnackbar Snackbar { get; set; } = null!;

    #endregion

    #region Methods

    public async Task OnValidSubmitAsync()
    {
        IsBusy = true;

        try
        {
            var result = await Handler.CreateAsync(InputModel);
            if (result.IsSuccess)
            {
                Snackbar.Add(result.Message, Severity.Success);
                NavigationManager.NavigateTo("/materiais");
            }
            else
            {
                Snackbar.Add(result.Message, Severity.Error);
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