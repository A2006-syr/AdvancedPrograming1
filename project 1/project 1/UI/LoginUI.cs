using project_1.Models;
using project_1.Services;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text;

namespace project_1.UI;

public static class LoginUI
{
    public static User Show(AuthService auth)
    {
        var figlet = new FigletText("Welcome to the System :)").Centered();
        AnsiConsole.Write(figlet);
        var u = AnsiConsole.Ask<string>("Enter your [green]username[/]: ");
        var p = AnsiConsole.Prompt(new TextPrompt<string>("Enter your [green]password[/]:").PromptStyle("red").Secret());
        return auth.Login(u, p);
    }
}

