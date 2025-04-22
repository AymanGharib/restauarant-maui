using CommunityToolkit.Mvvm.ComponentModel;
using MauiMySQLDemo.Services;
using Microcharts;
using SkiaSharp;
using System.Collections.Generic;

namespace MauiMySQLDemo.ViewModels;

public partial class StatisticsViewModel : ObservableObject
{
    private readonly StatisticsService _stats = new();

    [ObservableProperty]
    private Chart? topDishesChart;

    [ObservableProperty]
    private Chart? topTablesChart;

    public StatisticsViewModel()
    {
        LoadCharts();
    }

    private void LoadCharts()
    {
        // Top Dishes
        var dishes = _stats.GetTopDishes();
        TopDishesChart = new BarChart
        {
            Entries = dishes.Select(d => new ChartEntry(d.Count)
            {
                Label = d.Name,
                ValueLabel = d.Count.ToString(),
                Color = SKColor.Parse("#FF9800")
            }).ToList()
        };

        // Most Reserved Tables
        var tables = _stats.GetTopReservedTables();
        TopTablesChart = new BarChart
        {
            Entries = tables.Select(t => new ChartEntry(t.Count)
            {
                Label = t.TableLabel,
                ValueLabel = t.Count.ToString(),
                Color = SKColor.Parse("#4CAF50")
            }).ToList()
        };
    }
}
