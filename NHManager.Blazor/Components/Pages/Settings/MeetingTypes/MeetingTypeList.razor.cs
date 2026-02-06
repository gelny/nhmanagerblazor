using Microsoft.AspNetCore.Components;
using MudBlazor;
using NHManager.Blazor.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Localization;
using NHManager.Blazor.Resources;

namespace NHManager.Blazor.Components.Pages.Settings.MeetingTypes;

public partial class MeetingTypeList
{

    private List<MeetingType> _meetingTypes = new();
    private MudTable<MeetingType> _table = new();
    private string _searchString = "";

    protected override async Task OnInitializedAsync()
    {
        _meetingTypes = await MeetingTypeService.GetAllAsync();
    }

    private bool FilterFunc(MeetingType meetingType) => FilterFunc(meetingType, _searchString);

    private bool FilterFunc(MeetingType meetingType, string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return true;
        
        if (meetingType.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (meetingType.Abbreviation.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    }

    private async Task DeleteMeetingType(MeetingType meetingType)
    {
        bool? result = await DialogService.ShowMessageBox(
            string.Format(Loc["DeleteTitle"], Loc["MeetingType"]),
            string.Format(Loc["DeleteConfirmation"], Loc["MeetingType"], meetingType.Name), 
            yesText: Loc["Delete"], cancelText: Loc["Cancel"]);
        
        if (result == true)
        {
             try
             {
                 var authState = await AuthStateProvider.GetAuthenticationStateAsync();
                 var userName = authState.User.Identity?.Name ?? "System";
                 
                 await MeetingTypeService.DeleteAsync(meetingType.Id, userName);
                 Snackbar.Add(string.Format(Loc["DeletedSuccess"], Loc["MeetingType"]), Severity.Success);
                 _meetingTypes = await MeetingTypeService.GetAllAsync();
             }
             catch (Exception ex)
             {
                 Snackbar.Add(string.Format(Loc["ErrorDeleting"], ex.Message), Severity.Error);
             }
        }
    }
}
