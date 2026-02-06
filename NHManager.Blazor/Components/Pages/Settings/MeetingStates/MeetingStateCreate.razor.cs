using Microsoft.AspNetCore.Components;
using MudBlazor;
using NHManager.Blazor.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Localization;
using NHManager.Blazor.Resources;

namespace NHManager.Blazor.Components.Pages.Settings.MeetingStates;

public partial class MeetingStateCreate
{

    private MeetingState _meetingState = new();
    private bool _saving = false;

    private async Task HandleSubmit()
    {
        _saving = true;
        try
        {
            var authState = await AuthStateProvider.GetAuthenticationStateAsync();
            var userName = authState.User.Identity?.Name ?? "System";

            await MeetingStateService.CreateAsync(_meetingState, userName);
             Snackbar.Add(string.Format(Loc["CreatedSuccess"], Loc["MeetingState"]), Severity.Success);
            Navigation.NavigateTo("/settings/meeting-states");
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
