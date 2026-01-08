using project_1.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace project_1.Services;

public class AuthService
{
    private List<User> _users;
    private const string Path = "Data/users.json";

    public AuthService()
    {
        _users = FileService.Load<List<User>>(Path);

        if (_users.Count == 0)
        {
            _users.Add(new User
            {
                Username = "admin",
                Password = "1234",
                Role = Role.Admin
            });
            FileService.Save(Path, _users);
        }
    }

    public User Login(string username, string password)
    {
        var user = _users.FirstOrDefault(u =>
            u.Username == username && u.Password == password);

        if (user == null)
            throw new Exception("\nInvalid login");

        AnsiConsole.MarkupLine($"\nHello, [yellow]{username}[/]! You have successfully logged in.");
        return user;
    }
}
