using project_1.Models;
using project_1.Services;

namespace WarehouseManagementSystem.Services;

public class WarehouseService
{
    private const string Path = "Data/products.json";
    public List<Product> Products { get; }

    public WarehouseService()
    {
        Products = FileService.Load<List<Product>>(Path);
    }

    public List<string> GetCategories()
        => Products.Select(p => p.Category).Distinct().ToList();

    public void AddProduct(string name, string category, decimal price, int quantity)
    {
        if (Products.Any(p => p.Name == name))
            throw new Exception("Product already exists");

        Products.Add(new Product
        {
            Name = name,
            Category = category,
            Price = price,
            Quantity = quantity
        });

        Save();
    }

    public void RemoveProduct(string name)
    {
        var product = Products.FirstOrDefault(p => p.Name == name);
        if (product == null)
            throw new Exception("Product not found");

        Products.Remove(product);
        Save();
    }

    public void Save()
    {
        FileService.Save(Path, Products);
    }
}
