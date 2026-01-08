using project_1.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace project_1.Services;

public class EmployeeService
{
    private const string Path = "Data/users.json";
    private List<User> _users;

    public EmployeeService()
    {
        _users = FileService.Load<List<User>>(Path);
    }

    public List<User> GetEmployees() =>
        _users.Where(u => u.Role == Role.Employee).ToList();

    public void Add(string username, string password)
    {
        if (_users.Any(u => u.Username == username))
            throw new Exception("User exists");

        _users.Add(new User
        {
            Username = username,
            Password = password,
            Role = Role.Employee
        });

        FileService.Save(Path, _users);
    }

    public void Remove(string username)
    {
        var emp = _users.FirstOrDefault(u => u.Username == username && u.Role == Role.Employee);
        if (emp == null) throw new Exception("Not found");

        _users.Remove(emp);
        FileService.Save(Path, _users);
    }

    public void EditEmployee(
    string oldUsername,
    string newUsername,
    string newPassword)
    {
        var employee = _users.FirstOrDefault(u =>
            u.Username == oldUsername && u.Role == Role.Employee);

        if (employee == null)
            throw new Exception("Employee not found");

        // check if new username already exists
        if (_users.Any(u => u.Username == newUsername && u.Username != oldUsername))
            throw new Exception("Username already exists");

        employee.Username = newUsername;
        employee.Password = newPassword;

        FileService.Save(Path, _users);
    }

}

