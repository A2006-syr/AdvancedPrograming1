using project_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace project_1.Services;

public class ReportService
{
    // أرباح الشهر المحدد
    public decimal MonthlyProfit(List<Order> orders, int month, int year)
    {
        return orders
            .Where(o => o.Date.Month == month && o.Date.Year == year)
            .Sum(o => o.Total);
    }

    // إجمالي الربح لجميع الطلبات
    public decimal TotalProfit(List<Order> orders)
    {
        return orders.Sum(o => o.Total);
    }

    // أرباح كل شهر لكل سنة موجودة في الطلبات
    public Dictionary<(int Year, int Month), decimal> ProfitPerMonth(List<Order> orders)
    {
        return orders
            .GroupBy(o => (o.Date.Year, o.Date.Month))
            .ToDictionary(g => g.Key, g => g.Sum(o => o.Total));
    }
}
