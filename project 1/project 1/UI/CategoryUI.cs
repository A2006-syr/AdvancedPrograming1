using System;
using System.Collections.Generic;
using System.Text;

using Spectre.Console;
using WarehouseManagementSystem.Services;

namespace WarehouseManagementSystem.UI;

public static class CategoryUI
{
    public static void Show()
    {
        var warehouse = new WarehouseService();

        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Category Management")
                .AddChoices(
                    "View Categories",
                    "Add Category",
                    "Delete Category",
                    "Back"));

        switch (choice)
        {
            case "View Categories":
                ViewCategories(warehouse);
                break;

            case "Add Category":
                AddCategory(warehouse);
                break;

            case "Delete Category":
                DeleteCategory(warehouse);
                break;
        }
    }

    private static void ViewCategories(WarehouseService warehouse)
    {
        var categories = warehouse.GetCategories();

        if (!categories.Any())
        {
            AnsiConsole.MarkupLine("[yellow]No categories available[/]");
            return;
        }

        categories.ForEach(c => AnsiConsole.MarkupLine($"- {c}"));
    }

    private static void AddCategory(WarehouseService warehouse)
    {
        var category = AnsiConsole.Ask<string>("Category name:");

        if (warehouse.GetCategories().Contains(category))
        {
            AnsiConsole.MarkupLine("[red]Category already exists[/]");
            return;
        }

        // category added implicitly when product is added
        AnsiConsole.MarkupLine(
            "[green]Category will be saved when a product is added to it[/]");
    }

    private static void DeleteCategory(WarehouseService warehouse)
    {
        var category = AnsiConsole.Ask<string>("Category name to delete:");

        var products = warehouse.Products
            .Where(p => p.Category == category)
            .ToList();

        if (!products.Any())
        {
            AnsiConsole.MarkupLine("[red]Category not found[/]");
            return;
        }

        products.ForEach(p => warehouse.Products.Remove(p));
        warehouse.Save();

        AnsiConsole.MarkupLine("[green]Category and its products deleted[/]");
    }
}
