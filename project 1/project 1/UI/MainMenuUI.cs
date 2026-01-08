using project_1.Models;
using project_1.Services;
using project_1.UI;
using Spectre.Console;

namespace WarehouseManagementSystem.UI;

public static class MainMenuUI
{
    public static void Show(User user)
    {
        var orderService = new OrderService();

        while (true)
        {
            var menu = new List<string>
            {
                "Sales",
                "Reports",
                "Exit"
            };

            if (user.Role == Role.Admin)
            {
                menu.Insert(0, "Product Management");
                menu.Insert(1, "Category Management");
                menu.Insert(2, "Employee Management");
                menu.Insert(3, "Restart old Invoice");
            }


            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title($"\n[bold Cyan1]Welcome {user.Username}[/]")
                    .AddChoices(menu));

            switch (choice)
            {
                case "Employee Management":
                    EmployeeUI.Show();
                    break;

                case "Sales":
                    SalesUI.Show(orderService);
                    break;

                case "Reports":
                    ReportUI.Show(orderService);
                    break;
                case "Product Management":
                    ProductUI.Show();
                    break;

                case "Category Management":
                    CategoryUI.Show();
                    break;
                case "Restart old Invoice":
                    ReInvoiceUI.Show(orderService);
                    break;
                case "Exit":
                    return;
            }
        }
    }
}
