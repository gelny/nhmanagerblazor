using ClosedXML.Excel;
using NHManager.Blazor.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace NHManager.Blazor.Services;

public class ReportService : IReportService
{
    private readonly IClientService _clientService;
    private readonly IClientMeasurementService _measurementService;

    public ReportService(IClientService clientService, IClientMeasurementService measurementService)
    {
        _clientService = clientService;
        _measurementService = measurementService;
    }

    public async Task<byte[]> GenerateClientSummaryPdfAsync(int clientId)
    {
        var client = await _clientService.GetByIdWithDetailsAsync(clientId);
        if (client == null)
            throw new KeyNotFoundException($"Client {clientId} not found");

        var latestResult = await _measurementService.GetLatestResultAsync(clientId);

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(30);
                page.DefaultTextStyle(x => x.FontSize(10));

                page.Header().Column(col =>
                {
                    col.Item().Text("NHManager").Bold().FontSize(16).FontColor(Colors.Teal.Darken2);
                    col.Item().Text($"Karta klienta - {client.FullName}").FontSize(14).Bold();
                    col.Item().Text($"Vygenerováno: {DateTime.Now:dd.MM.yyyy HH:mm}").FontSize(8).FontColor(Colors.Grey.Medium);
                    col.Item().PaddingBottom(10).LineHorizontal(1).LineColor(Colors.Grey.Lighten2);
                });

                page.Content().Column(col =>
                {
                    // Personal data
                    col.Item().Text("Osobní údaje").Bold().FontSize(12);
                    col.Item().PaddingTop(5).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(1);
                            columns.RelativeColumn(2);
                        });

                        AddTableRow(table, "Jméno", client.FullName);
                        AddTableRow(table, "Datum narození", client.DateOfBirth.ToString("dd.MM.yyyy"));
                        AddTableRow(table, "Pohlaví", client.Sex == 1 ? "Muž" : "Žena");
                        AddTableRow(table, "Telefon", client.Phone ?? "-");
                        AddTableRow(table, "Email", client.Email ?? "-");
                        AddTableRow(table, "Adresa", FormatAddress(client));
                        AddTableRow(table, "Konzultant", client.Consultant?.FullName ?? "-");
                    });

                    col.Item().PaddingVertical(10).LineHorizontal(0.5f).LineColor(Colors.Grey.Lighten2);

                    // Latest measurement result
                    if (latestResult != null)
                    {
                        col.Item().Text("Poslední výsledky měření").Bold().FontSize(12);
                        col.Item().Text($"Datum: {latestResult.Date:dd.MM.yyyy}").FontSize(9).FontColor(Colors.Grey.Medium);
                        col.Item().PaddingTop(5).Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(1);
                                columns.RelativeColumn(1);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("Ukazatel").Bold();
                                header.Cell().Text("Hodnota").Bold();
                                header.Cell().Text("Doporučeno").Bold();
                            });

                            AddResultRow(table, "BMI", latestResult.BMI.ToString("F1"), latestResult.BMI_Recommended.ToString("F1"));
                            AddResultRow(table, "Metabolický věk", latestResult.MetabolicAge.ToString("F0"), latestResult.MetabolicAge_Recommended.ToString("F0"));
                            AddResultRow(table, "Viscerální tuk", latestResult.VisceralFat.ToString("F1"), latestResult.VisceralFat_Recommended.ToString("F1"));
                            AddResultRow(table, "Hmotnost (min-max)", "", $"{latestResult.Weight_RecommendedMin:F1} - {latestResult.Weight_RecommendedMax:F1}");
                            AddResultRow(table, "BRM (kcal)", latestResult.BRM_KCAL.ToString(), "-");
                            AddResultRow(table, "BRM (kJ)", latestResult.BRM_KJ.ToString(), "-");
                        });
                    }

                    // Measurements summary
                    if (client.Measurements.Any())
                    {
                        col.Item().PaddingVertical(10).LineHorizontal(0.5f).LineColor(Colors.Grey.Lighten2);
                        col.Item().Text("Historie měření").Bold().FontSize(12);
                        col.Item().PaddingTop(5).Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(1);
                                columns.RelativeColumn(1);
                                columns.RelativeColumn(1);
                                columns.RelativeColumn(1);
                                columns.RelativeColumn(1);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("Datum").Bold().FontSize(9);
                                header.Cell().Text("Hmotnost").Bold().FontSize(9);
                                header.Cell().Text("Tuk (%)").Bold().FontSize(9);
                                header.Cell().Text("Voda (%)").Bold().FontSize(9);
                                header.Cell().Text("Výška").Bold().FontSize(9);
                            });

                            foreach (var m in client.Measurements.OrderByDescending(m => m.Date).Take(10))
                            {
                                table.Cell().Text(m.Date.ToString("dd.MM.yyyy")).FontSize(9);
                                table.Cell().Text(m.Weight.ToString("F1")).FontSize(9);
                                table.Cell().Text(m.FatPercentage.ToString("F1")).FontSize(9);
                                table.Cell().Text(m.WaterPercentage.ToString("F1")).FontSize(9);
                                table.Cell().Text(m.Height.ToString()).FontSize(9);
                            }
                        });
                    }
                });

                page.Footer().AlignCenter().Text(text =>
                {
                    text.Span("NHManager - ").FontSize(8).FontColor(Colors.Grey.Medium);
                    text.CurrentPageNumber().FontSize(8);
                    text.Span(" / ").FontSize(8);
                    text.TotalPages().FontSize(8);
                });
            });
        });

        return document.GeneratePdf();
    }

    public async Task<byte[]> ExportMeasurementsToExcelAsync(int clientId)
    {
        var client = await _clientService.GetByIdWithDetailsAsync(clientId);
        if (client == null)
            throw new KeyNotFoundException($"Client {clientId} not found");

        using var workbook = new XLWorkbook();
        var ws = workbook.Worksheets.Add("Měření");

        // Header
        ws.Cell(1, 1).Value = "Datum";
        ws.Cell(1, 2).Value = "Hmotnost (kg)";
        ws.Cell(1, 3).Value = "Tuk (%)";
        ws.Cell(1, 4).Value = "Voda (%)";
        ws.Cell(1, 5).Value = "Kostní hmota (kg)";
        ws.Cell(1, 6).Value = "Viscerální tuk";
        ws.Cell(1, 7).Value = "Beztuková hmota (kg)";
        ws.Cell(1, 8).Value = "Obvod pasu (cm)";
        ws.Cell(1, 9).Value = "Obvod boků (cm)";
        ws.Cell(1, 10).Value = "Výška (cm)";
        ws.Cell(1, 11).Value = "Systolický TK";
        ws.Cell(1, 12).Value = "Diastolický TK";

        var headerRange = ws.Range(1, 1, 1, 12);
        headerRange.Style.Font.Bold = true;
        headerRange.Style.Fill.BackgroundColor = XLColor.LightGreen;

        int row = 2;
        foreach (var m in client.Measurements.OrderByDescending(m => m.Date))
        {
            ws.Cell(row, 1).Value = m.Date.ToString("dd.MM.yyyy");
            ws.Cell(row, 2).Value = (double)m.Weight;
            ws.Cell(row, 3).Value = (double)m.FatPercentage;
            ws.Cell(row, 4).Value = (double)m.WaterPercentage;
            ws.Cell(row, 5).Value = (double)m.BoneMass;
            ws.Cell(row, 6).Value = (double)m.VisceralFat;
            ws.Cell(row, 7).Value = (double)m.LeanBodyMass;
            ws.Cell(row, 8).Value = m.WaistCircumference;
            ws.Cell(row, 9).Value = m.HipCircumference;
            ws.Cell(row, 10).Value = m.Height;
            ws.Cell(row, 11).Value = m.SystolicBloodPressure;
            ws.Cell(row, 12).Value = m.DiastolicBloodPressure;
            row++;
        }

        ws.Columns().AdjustToContents();

        using var ms = new MemoryStream();
        workbook.SaveAs(ms);
        return ms.ToArray();
    }

    public async Task<byte[]> ExportClientListToExcelAsync(string? search = null)
    {
        var clients = await _clientService.GetAllAsync(search);

        using var workbook = new XLWorkbook();
        var ws = workbook.Worksheets.Add("Klienti");

        ws.Cell(1, 1).Value = "Jméno";
        ws.Cell(1, 2).Value = "Příjmení";
        ws.Cell(1, 3).Value = "Telefon";
        ws.Cell(1, 4).Value = "Email";
        ws.Cell(1, 5).Value = "Datum narození";
        ws.Cell(1, 6).Value = "Město";
        ws.Cell(1, 7).Value = "Konzultant";

        var headerRange = ws.Range(1, 1, 1, 7);
        headerRange.Style.Font.Bold = true;
        headerRange.Style.Fill.BackgroundColor = XLColor.LightGreen;

        int row = 2;
        foreach (var c in clients)
        {
            ws.Cell(row, 1).Value = c.FirstName;
            ws.Cell(row, 2).Value = c.SurName;
            ws.Cell(row, 3).Value = c.Phone ?? "";
            ws.Cell(row, 4).Value = c.Email ?? "";
            ws.Cell(row, 5).Value = c.DateOfBirth.ToString("dd.MM.yyyy");
            ws.Cell(row, 6).Value = c.City ?? "";
            ws.Cell(row, 7).Value = c.Consultant?.FullName ?? "";
            row++;
        }

        ws.Columns().AdjustToContents();

        using var ms = new MemoryStream();
        workbook.SaveAs(ms);
        return ms.ToArray();
    }

    private static string FormatAddress(Client client)
    {
        var parts = new[] { client.Street, client.City, client.PostalCode, client.Country }
            .Where(p => !string.IsNullOrEmpty(p));
        return parts.Any() ? string.Join(", ", parts) : "-";
    }

    private static void AddTableRow(TableDescriptor table, string label, string value)
    {
        table.Cell().PaddingVertical(2).Text(label).FontSize(9).FontColor(Colors.Grey.Darken1);
        table.Cell().PaddingVertical(2).Text(value).FontSize(9);
    }

    private static void AddResultRow(TableDescriptor table, string label, string value, string recommended)
    {
        table.Cell().PaddingVertical(2).Text(label).FontSize(9);
        table.Cell().PaddingVertical(2).Text(value).FontSize(9);
        table.Cell().PaddingVertical(2).Text(recommended).FontSize(9).FontColor(Colors.Grey.Darken1);
    }
}
