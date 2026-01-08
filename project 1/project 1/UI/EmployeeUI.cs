using System;
using System.Collections.Generic;
using System.Text;
using Spectre.Console;
using project_1.Services;

namespace project_1.UI;

public static class EmployeeUI
{
    public static void Show()
    {
        var service = new EmployeeService();

        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .AddChoices("View Employees", "Add", "Remove","Edit","Back"));

        if (choice == "View Employees")
        {
            var employees = service.GetEmployees();

            if (!employees.Any())
            {
                AnsiConsole.MarkupLine("[yellow]\nNo employees found.[/]");
                return;
            }

            var table = new Table()
                .AddColumn("Username");

            foreach (var e in employees)
            {
                table.AddRow(e.Username);
            }

            AnsiConsole.Write(table);
        }


        if (choice == "Add")
            service.Add(
                AnsiConsole.Ask<string>("Username:"),
                AnsiConsole.Ask<string>("Password:"));

        if (choice == "Remove")
            service.Remove(AnsiConsole.Ask<string>("Username:"));

        if (choice == "Edit")
        {
            var employees = service.GetEmployees();

            if (!employees.Any())
            {
                AnsiConsole.MarkupLine("[yellow]\nNo employees found.[/]");
                return;
            }

            var selectedEmployee = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select employee to edit")
                    .AddChoices(employees.Select(e => e.Username)));

            var newUsername = AnsiConsole.Ask<string>(
                "New username:");

            var newPassword = AnsiConsole.Ask<string>(
                "New password:");

            service.EditEmployee(
                selectedEmployee,
                newUsername,
                newPassword);

            AnsiConsole.MarkupLine("[green]\nEmployee updated successfully[/]");
        }

    }
}

