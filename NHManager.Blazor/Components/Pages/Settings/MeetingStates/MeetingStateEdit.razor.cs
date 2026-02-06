using Microsoft.AspNetCore.Components;
using MudBlazor;
using NHManager.Blazor.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Localization;
using NHManager.Blazor.Resources;

namespace NHManager.Blazor.Components.Pages.Settings.MeetingStates;

public partial class MeetingStateEdit
{

    [Parameter]
    public int Id { get; set; }

    private MeetingState? _meetingState;
    private bool _loading = true;
    private bool _saving = false;

    protected override async Task OnInitializedAsync()
    {
        _meetingState = await MeetingStateService.GetByIdAsync(Id);
        _loading = false;
    }

    private async Task HandleSubmit()
    {
        if (_meetingState == null) return;
        
        _saving = true;
        try
        {
            var authState = await AuthStateProvider.GetAuthenticationStateAsync();
            var userName = authState.User.Identity?.Name ?? "System";

            await MeetingStateService.UpdateAsync(_meetingState, userName);
             Snackbar.Add(string.Format(Loc["UpdatedSuccess"], Loc["MeetingState"]), Severity.Success);
            Navigation.NavigateTo("/settings/meeting-states");
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
