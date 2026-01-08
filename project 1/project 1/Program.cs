using project_1.Services;
using project_1.UI;
using Spectre.Console;
using WarehouseManagementSystem.UI;

Directory.CreateDirectory("Data");

var auth = new AuthService();

try
{
    var user = LoginUI.Show(auth);
    MainMenuUI.Show(user);
}
catch (Exception ex)
{
    AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
}

