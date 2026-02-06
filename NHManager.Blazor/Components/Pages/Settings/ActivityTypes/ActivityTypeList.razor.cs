using Microsoft.AspNetCore.Components;
using MudBlazor;
using NHManager.Blazor.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Localization;
using NHManager.Blazor.Resources;

namespace NHManager.Blazor.Components.Pages.Settings.ActivityTypes;

public partial class ActivityTypeList
{

    private List<PhysicalActivityType> _activityTypes = new();
    private MudTable<PhysicalActivityType> _table = new();
    private string _searchString = "";

    protected override async Task OnInitializedAsync()
    {
        _activityTypes = await ActivityTypeService.GetAllAsync();
    }

    private bool FilterFunc(PhysicalActivityType activityType) => FilterFunc(activityType, _searchString);

    private bool FilterFunc(PhysicalActivityType activityType, string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return true;
        
        if (activityType.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    }

    private async Task DeleteActivityType(PhysicalActivityType activityType)
    {
        bool? result = await DialogService.ShowMessageBox(
            string.Format(Loc["DeleteTitle"], Loc["ActivityType"]),
            string.Format(Loc["DeleteConfirmation"], Loc["ActivityType"], activityType.Name), 
            yesText: Loc["Delete"], cancelText: Loc["Cancel"]);
        
        if (result == true)
        {
             try
             {
                 var authState = await AuthStateProvider.GetAuthenticationStateAsync();
                 var userName = authState.User.Identity?.Name ?? "System";
                 
                 await ActivityTypeService.DeleteAsync(activityType.Id, userName);
                 Snackbar.Add(string.Format(Loc["DeletedSuccess"], Loc["ActivityType"]), Severity.Success);
                 _activityTypes = await ActivityTypeService.GetAllAsync();
             }
             catch (Exception ex)
             {
                 Snackbar.Add(string.Format(Loc["ErrorDeleting"], ex.Message), Severity.Error);
             }
        }
    }
}
