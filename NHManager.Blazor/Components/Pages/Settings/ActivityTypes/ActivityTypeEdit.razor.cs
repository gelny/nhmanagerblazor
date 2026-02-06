using Microsoft.AspNetCore.Components;
using MudBlazor;
using NHManager.Blazor.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Localization;
using NHManager.Blazor.Resources;

namespace NHManager.Blazor.Components.Pages.Settings.ActivityTypes;

public partial class ActivityTypeEdit
{

    [Parameter]
    public int Id { get; set; }

    private PhysicalActivityType? _activityType;
    private bool _loading = true;
    private bool _saving = false;

    protected override async Task OnInitializedAsync()
    {
        _activityType = await ActivityTypeService.GetByIdAsync(Id);
        _loading = false;
    }

    private async Task HandleSubmit()
    {
        if (_activityType == null) return;
        
        _saving = true;
        try
        {
            var authState = await AuthStateProvider.GetAuthenticationStateAsync();
            var userName = authState.User.Identity?.Name ?? "System";

            await ActivityTypeService.UpdateAsync(_activityType, userName);
             Snackbar.Add(string.Format(Loc["UpdatedSuccess"], Loc["ActivityType"]), Severity.Success);
            Navigation.NavigateTo("/settings/activity-types");
        }
        catch (Exception ex)
        {
             Snackbar.Add(string.Format(Loc["ErrorUpdating"], ex.Message), Severity.Error);
        }
        finally
        {
            _saving = false;
        }
    }
}
