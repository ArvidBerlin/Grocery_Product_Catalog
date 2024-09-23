using Resources.Models;
using Resources.Services;

namespace MainApp.Menus;

public class ProductMenu
{
    private static readonly ProductService _productService = new();

    internal static void CreateProductMenu()
    {
        var product = new Product();

        Console.Clear();
        Console.WriteLine("\n\t Please type in the product you want to add to the inventory.");

        Console.Write("\n\t Product name: ");
        product.Name = Console.ReadLine() ?? "";

        Console.Write("\t Product price: ");
        decimal.TryParse(Console.ReadLine(), out decimal price);
        product.Price = price;

        var response = _productService.CreateProduct(product);
        Console.WriteLine(response.Message);
    }

    internal static void GetAllProductsFromListMenu()
    {
        Console.Clear();
        var products = _productService.GetAllProductsFromList();

        if (!products.Succeeded)
        {
            var response = _productService.GetAllProductsFromList();
            Console.WriteLine(response.Message);
        }
        else
        {
            foreach (var product in products.Content as IEnumerable<Product>)
            {
                Console.WriteLine($"\t Product ID: {product.Id}" +
                $"\n\t Product: {product.Name}" +
                $"\n\t Price: {product.Price}");
            }
        }
    }

    internal static void DeleteProductMenu()
    {
        var product = new Product();

        Console.Clear();
        Console.WriteLine("\t Please type the product name you want to remove from the inventory.");
        Console.Write("\n\t Product name: ");
        product.Name = Console.ReadLine() ?? "";

        var response = _productService.DeleteProduct(product);
        Console.WriteLine(response.Message);
    }
}