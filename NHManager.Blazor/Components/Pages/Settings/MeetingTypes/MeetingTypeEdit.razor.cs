using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Utilities;
using NHManager.Blazor.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Localization;
using NHManager.Blazor.Resources;

namespace NHManager.Blazor.Components.Pages.Settings.MeetingTypes;

public partial class MeetingTypeEdit
{

    [Parameter]
    public int Id { get; set; }

    private MeetingType? _meetingType;
    private bool _loading = true;
    private bool _saving = false;

    // Custom palette
    public IEnumerable<MudColor> CustomPalette { get; set; } = new List<MudColor>()
    {
        "#F44336", "#E91E63", "#9C27B0", "#673AB7", "#3F51B5", "#2196F3", "#03A9F4", "#00BCD4", 
        "#009688", "#4CAF50", "#8BC34A", "#CDDC39", "#FFEB3B", "#FFC107", "#FF9800", "#FF5722", 
        "#795548", "#9E9E9E", "#607D8B", "#000000"
    };

    protected override async Task OnInitializedAsync()
    {
        _meetingType = await MeetingTypeService.GetByIdAsync(Id);
        _loading = false;
    }

    private async Task HandleSubmit()
    {
        if (_meetingType == null) return;
        
        _saving = true;
        try
        {
            var authState = await AuthStateProvider.GetAuthenticationStateAsync();
            var userName = authState.User.Identity?.Name ?? "System";

            await MeetingTypeService.UpdateAsync(_meetingType, userName);
             Snackbar.Add(string.Format(Loc["UpdatedSuccess"], Loc["MeetingType"]), Severity.Success);
            Navigation.NavigateTo("/settings/meeting-types");
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
