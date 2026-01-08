using project_1.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace project_1.Services;

public class OrderService
{
    private const string Path = "Data/orders.json";
    private List<Order> _orders;

    private List<Coupon> _coupons = new()
    {
        new Coupon { Code = "SAVE10", DiscountPercent = 10 },
        new Coupon { Code = "SAVE20", DiscountPercent = 20 }
    };

    public OrderService()
    {
        _orders = FileService.Load<List<Order>>(Path);
    }

    public decimal ApplyCoupon(string code, decimal total)
    {
        var c = _coupons.FirstOrDefault(x => x.Code == code);
        return c == null ? total : total - total * c.DiscountPercent / 100;
    }

    public void AddOrder(Order order)
    {
        _orders.Add(order);
        FileService.Save(Path, _orders);
    }

    public List<Order> GetOrders() => _orders;
}

