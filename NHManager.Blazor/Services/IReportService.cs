namespace NHManager.Blazor.Services;

public interface IReportService
{
    Task<byte[]> GenerateClientSummaryPdfAsync(int clientId);
    Task<byte[]> ExportMeasurementsToExcelAsync(int clientId);
    Task<byte[]> ExportClientListToExcelAsync(string? search = null);
}
