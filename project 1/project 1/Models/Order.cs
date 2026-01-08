using System;
using System.Collections.Generic;
using System.Text;

namespace project_1.Models;

public class Order
{
    public DateTime Date { get; set; } = DateTime.Now;
    public List<OrderItem> Items { get; set; } = new();
    public decimal Total { get; set; }
}

