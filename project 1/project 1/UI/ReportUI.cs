using project_1.Models;
using project_1.Services;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace project_1.UI;

public static class ReportUI
{
    public static void Show(OrderService orderService)
    {
        var orders = orderService.GetOrders();
        var report = new ReportService();

        // إجمالي الربح
        var overallTotal = report.TotalProfit(orders);
        AnsiConsole.MarkupLine($"[green]\nOverall Profit: {overallTotal}[/]");

        // أرباح كل شهر
        var monthlyProfits = report.ProfitPerMonth(orders);

        if (monthlyProfits.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]\nNo orders found[/]");
            return;
        }

        var table = new Table()
            .Border(TableBorder.Rounded)
            .AddColumn("Year")
            .AddColumn("Month")
            .AddColumn("Profit");

        foreach (var kvp in monthlyProfits.OrderBy(k => k.Key.Year).ThenBy(k => k.Key.Month))
        {
            table.AddRow(
                kvp.Key.Year.ToString(),
                kvp.Key.Month.ToString(),
                kvp.Value.ToString("F2"));
        }

        AnsiConsole.Write(table);
    }
}





























































