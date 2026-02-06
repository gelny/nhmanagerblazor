using Microsoft.AspNetCore.Components;
using MudBlazor;
using NHManager.Blazor.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Localization;
using NHManager.Blazor.Resources;

namespace NHManager.Blazor.Components.Pages.Settings.MeetingStates;

public partial class MeetingStateList
{

    private List<MeetingState> _meetingStates = new();
    private MudTable<MeetingState> _table = new();
    private string _searchString = "";

    protected override async Task OnInitializedAsync()
    {
        _meetingStates = await MeetingStateService.GetAllAsync();
    }

    private bool FilterFunc(MeetingState meetingState) => FilterFunc(meetingState, _searchString);

    private bool FilterFunc(MeetingState meetingState, string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return true;
        
        if (meetingState.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    }

    private async Task DeleteMeetingState(MeetingState meetingState)
    {
        bool? result = await DialogService.ShowMessageBox(
            string.Format(Loc["DeleteTitle"], Loc["MeetingState"]),
            string.Format(Loc["DeleteConfirmation"], Loc["MeetingState"], meetingState.Name), 
            yesText: Loc["Delete"], cancelText: Loc["Cancel"]);
        
        if (result == true)
        {
             try
             {
                 var authState = await AuthStateProvider.GetAuthenticationStateAsync();
                 var userName = authState.User.Identity?.Name ?? "System";
                 
                 await MeetingStateService.DeleteAsync(meetingState.Id, userName);
                 Snackbar.Add(string.Format(Loc["DeletedSuccess"], Loc["MeetingState"]), Severity.Success);
                 _meetingStates = await MeetingStateService.GetAllAsync();
             }
             catch (Exception ex)
             {
                 Snackbar.Add(string.Format(Loc["ErrorDeleting"], ex.Message), Severity.Error);
             }
        }
    }
}
