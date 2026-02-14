namespace NHManager.Blazor.Components.Pages.ClientGraphs;

public record ChartDataPoint(DateTime Date, decimal Value)
{
    public static List<ChartDataPoint> DeduplicateByDate(List<ChartDataPoint> points)
        => points.GroupBy(p => p.Date.Date)
                 .Select(g => new ChartDataPoint(g.Key, g.Max(p => p.Value)))
                 .OrderBy(p => p.Date)
                 .ToList();
}

public record MultiSeriesDataPoint(DateTime Date, decimal Value, string Series);
