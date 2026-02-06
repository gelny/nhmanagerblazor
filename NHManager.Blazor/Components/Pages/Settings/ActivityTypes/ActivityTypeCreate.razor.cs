using Microsoft.AspNetCore.Components;
using MudBlazor;
using NHManager.Blazor.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Localization;
using NHManager.Blazor.Resources;

namespace NHManager.Blazor.Components.Pages.Settings.ActivityTypes;

public partial class ActivityTypeCreate
{

    private PhysicalActivityType _activityType = new();
    private bool _saving = false;

    private async Task HandleSubmit()
    {
        _saving = true;
        try
        {
            var authState = await AuthStateProvider.GetAuthenticationStateAsync();
            var userName = authState.User.Identity?.Name ?? "System";

            await ActivityTypeService.CreateAsync(_activityType, userName);
             Snackbar.Add(string.Format(Loc["CreatedSuccess"], Loc["ActivityType"]), Severity.Success);
            Navigation.NavigateTo("/settings/activity-types");
        }
        catch (Exception ex)
        {
             Snackbar.Add(string.Format(Loc["ErrorCreating"], ex.Message), Severity.Error);
        }
        finally
        {
            _saving = false;
        }
    }
}
