using project_1.Models;
using project_1.Services;
using Spectre.Console;
using System;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using WarehouseManagementSystem.Services;

namespace WarehouseManagementSystem.UI;

public static class SalesUI
{
    public static void Show(OrderService orderService)
    {
        var warehouse = new WarehouseService();
        var order = new Order();

        while (true)
        {
            var product = AnsiConsole.Prompt(
                new SelectionPrompt<Product>()
                    .Title("Select a product")
                    .AddChoices(warehouse.Products)
                    .UseConverter(p => $"{p.Name} (Available: {p.Quantity})"));

            var qty = AnsiConsole.Ask<int>("Quantity:");

            if (qty > product.Quantity)
            {
                AnsiConsole.MarkupLine("[red]Not enough quantity available[/]");
                continue;
            }

            product.Quantity -= qty;
            warehouse.Save();

            order.Items.Add(new OrderItem
            {
                ProductName = product.Name,
                Quantity = qty,
                Price = product.Price
            });

            //  عرض السلة الحالية  
            if (order.Items.Any())
            {
                AnsiConsole.MarkupLine("\n[bold]Current Cart:[/]");
                foreach (var item in order.Items)
                    AnsiConsole.MarkupLine($"- {item.ProductName} x {item.Quantity} @ {item.Price:F2}");
            }

     

            if (!AnsiConsole.Confirm("Add another product?"))
                break;
        }
        //  حذف منتج من السلة
        if (order.Items.Any() && AnsiConsole.Confirm("Do you want to remove a product from the cart?",false))
        {
            var toRemove = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select a product to remove")
                    .AddChoices(order.Items.Select(i => i.ProductName))
            );

            var removedItem = order.Items.First(i => i.ProductName == toRemove);
            order.Items.Remove(removedItem);

            // استرجاع الكمية للمخزن
            var originalProduct = warehouse.Products.First(p => p.Name == removedItem.ProductName);
            originalProduct.Quantity += removedItem.Quantity;
            warehouse.Save();

            AnsiConsole.MarkupLine($"[yellow]{removedItem.ProductName} removed from cart[/]");
        }

        if (!order.Items.Any())
        {
            AnsiConsole.MarkupLine("[red]No products in the order. Cancelling...[/]");
            return;
        }

        var total = order.Items.Sum(i => i.Price * i.Quantity);
        var coupon = AnsiConsole.Ask<string>("Discount coupon (optional):");
        order.Total = orderService.ApplyCoupon(coupon, total);
        orderService.AddOrder(order);

        var invoiceNumber = new Random().Next(1000, 9999);
        
        //  invoice show
        var table = new Table()
            .Title($"[bold cyan]\nInvoice #{invoiceNumber}[/]")
            .Border(TableBorder.Rounded)
            .AddColumn("Product")
            .AddColumn("Quantity")
            .AddColumn("Unit Price")
            .AddColumn("Total");

        foreach (var item in order.Items)
        {
            table.AddRow(
                item.ProductName,
                item.Quantity.ToString(),
                item.Price.ToString("F2"),
                (item.Price * item.Quantity).ToString("F2"));
        }

        AnsiConsole.Write(table);

        AnsiConsole.MarkupLine($"[cyan]\nDate: {order.Date}[/]");
        AnsiConsole.MarkupLine($"[cyan]\nTotal after discount: {order.Total:F2}[/]");
        AnsiConsole.MarkupLine("[bold cyan]\nThank you for your purchase![/]");



    }
}






























































































//var sb = new StringBuilder();
//sb.AppendLine("=====================================");
//sb.AppendLine($"Invoice #: {new Random().Next(1000, 9999)}");
//sb.AppendLine($"Date: {order.Date}");
//sb.AppendLine("-------------------------------------");
//sb.AppendLine($"{"Product",-20}   {"Qty",3}   {"Unit",6}    {"Total",8}");
//sb.AppendLine("-------------------------------------");

//foreach (var item in order.Items)
//{
//    sb.AppendLine($"{item.ProductName,-20} {item.Quantity,3} {item.Price,6:F2} {item.Price * item.Quantity,8:F2}");
//}

//sb.AppendLine("-------------------------------------");
//sb.AppendLine($"Total after discount: {order.Total:F2}");
//sb.AppendLine("=====================================");
//sb.AppendLine("Thank you for your purchase!");

//// عرض الفاتورة باستخدام Spectre.Console
//AnsiConsole.Write(new Markup(sb.ToString()));