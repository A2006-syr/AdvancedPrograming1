using System;
using System.Collections.Generic;
using System.Text;
using project_1.Models;
using project_1.Services;
using Spectre.Console;
using WarehouseManagementSystem.Services;

namespace WarehouseManagementSystem.UI
{
    public static class ReInvoiceUI
    {
        public static void Show(OrderService orderService)
        {
            var warehouse = new WarehouseService();
            var orders = orderService.GetOrders();

            if (!orders.Any())
            {
                AnsiConsole.MarkupLine("[yellow]No previous orders found[/]");
                return;
            }

            // اختيار الفاتورة لاسترجاع منتجاتها
            var selectedOrder = AnsiConsole.Prompt(
                new SelectionPrompt<Order>()
                    .Title("Select an invoice to restore its products to the warehouse")
                    .AddChoices(orders)
                    .UseConverter(o => $"Invoice on {o.Date} - Total: {o.Total:F2}"));

            // استرجاع المنتجات للمخزون
            foreach (var item in selectedOrder.Items)
            {
                var product = warehouse.Products.FirstOrDefault(p => p.Name == item.ProductName);
                if (product != null)
                {
                    product.Quantity += item.Quantity;
                }
                else
                {
                    // إذا المنتج لم يعد موجود في المخزن نضيفه 
                    warehouse.Products.Add(new Product
                    {
                        Name = item.ProductName,
                        Category = "Unknown",
                        Price = item.Price,
                        Quantity = item.Quantity
                    });
                }
            }

            warehouse.Save();
            AnsiConsole.MarkupLine("[green]\nProducts from the selected invoice have been restored to the warehouse.[/]");
        }
    }
}

