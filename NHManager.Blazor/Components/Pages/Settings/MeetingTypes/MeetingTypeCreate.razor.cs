using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Utilities;
using NHManager.Blazor.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Localization;
using NHManager.Blazor.Resources;

namespace NHManager.Blazor.Components.Pages.Settings.MeetingTypes;

public partial class MeetingTypeCreate
{

    private MeetingType _meetingType = new() { Color = "#2196F3" }; // Default blue
    private bool _saving = false;

    // Custom palette for consistency if needed, or default
    public IEnumerable<MudColor> CustomPalette { get; set; } = new List<MudColor>()
    {
        "#F44336", "#E91E63", "#9C27B0", "#673AB7", "#3F51B5", "#2196F3", "#03A9F4", "#00BCD4", 
        "#009688", "#4CAF50", "#8BC34A", "#CDDC39", "#FFEB3B", "#FFC107", "#FF9800", "#FF5722", 
        "#795548", "#9E9E9E", "#607D8B", "#000000"
    };

    private async Task HandleSubmit()
    {
        _saving = true;
        try
        {
            var authState = await AuthStateProvider.GetAuthenticationStateAsync();
            var userName = authState.User.Identity?.Name ?? "System";

            await MeetingTypeService.CreateAsync(_meetingType, userName);
             Snackbar.Add(string.Format(Loc["CreatedSuccess"], Loc["MeetingType"]), Severity.Success);
            Navigation.NavigateTo("/settings/meeting-types");
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
