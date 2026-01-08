using System;
using System.Collections.Generic;
using System.Text;

namespace project_1.Models;

public enum Role { Admin, Employee }

public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
}

