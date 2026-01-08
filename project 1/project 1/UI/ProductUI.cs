using project_1.Services;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;
using WarehouseManagementSystem.Services;

namespace WarehouseManagementSystem.UI;

public static class ProductUI
{
    public static void Show()
    {
        var warehouse = new WarehouseService();

        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Product Management")
                .AddChoices(
                    "View Products",
                    "Add Product",
                    "Delete Product",
                    "Back"));

        switch (choice)
        {
            case "View Products":
                ViewProducts(warehouse);
                break;

            case "Add Product":
                AddProduct(warehouse);
                break;

            case "Delete Product":
                DeleteProduct(warehouse);
                break;
        }
    }

    private static void ViewProducts(WarehouseService warehouse)
    {
        if (!warehouse.Products.Any())
        {
            AnsiConsole.MarkupLine("[yellow]No products available[/]");
            return;
        }

        var table = new Table()
            .AddColumn("Name")
            .AddColumn("Category")
            .AddColumn("Price")
            .AddColumn("Quantity");

        foreach (var p in warehouse.Products)
        {
            table.AddRow(
                p.Name,
                p.Category,
                p.Price.ToString(),
                p.Quantity.ToString());
        }

        AnsiConsole.Write(table);
    }

    private static void AddProduct(WarehouseService warehouse)
    {
        var name = AnsiConsole.Ask<string>("Product name:");
        var category = AnsiConsole.Ask<string>("Category:");
        var price = AnsiConsole.Ask<decimal>("Price:");
        var quantity = AnsiConsole.Ask<int>("Quantity:");

        warehouse.AddProduct(name, category, price, quantity);
        AnsiConsole.MarkupLine("[green]Product added successfully[/]");
    }

    private static void DeleteProduct(WarehouseService warehouse)
    {
        var name = AnsiConsole.Ask<string>("Product name to delete:");
        warehouse.RemoveProduct(name);
        AnsiConsole.MarkupLine("[green]Product deleted successfully[/]");
    }
}

