using Resources.Interfaces;
using Resources.Models;
using Resources.Services;

namespace MainApp.Menus;

internal class ProductMenu
{
    
    private static readonly string _filePath = @"c:\\programmering_med_c#\\projects\\console_files\\products.json";
    private static readonly IProductService<Product, Product> _productService = new ProductService(_filePath);

    /*
    private static readonly IProductService<Product, Product> _productService;

    public ProductMenu(IProductService<Product, Product> productService)
    {
        _productService = productService;
    }
    */
    

    public static void CreateProductMenu()
    {
        var product = new Product();

        Console.Clear();
        Console.WriteLine("\n\t Please type in the product you want to add to the inventory.");

        Console.Write("\n\t Product name: ");
        product.Name = Console.ReadLine() ?? "";

        Console.Write("\t Product price: ");
        decimal.TryParse(Console.ReadLine(), out decimal price);
        price = Math.Round(price, 2);
        product.Price = price;

        var response = _productService.CreateProduct(product);
        Console.WriteLine(response.Message);
        Console.Write("\n\t Press any key to continue. ");
    }

    public static void GetAllProductsFromListMenu()
    {
        var products = _productService.GetAllProductsFromList();

        Console.Clear();
        Console.WriteLine("\n\t The inventory: ");

        if (!products.Succeeded)
        {
            var response = _productService.GetAllProductsFromList();
            Console.WriteLine(response.Message);
        }
        else
        {
            foreach (var product in (IEnumerable<Product>)products.Result!)
            {
                Console.WriteLine($"\n\t Product ID: {product.Id}" +
                $"\n\t Product: {product.Name}" +
                $"\n\t Price: {product.Price} kr");
            }
        }
        Console.Write("\n\t Press any key to continue. ");
    }

    public static void DeleteProductMenu()
    {
        var products = _productService.GetAllProductsFromList();

        Console.Clear();
        Console.WriteLine("\n\t The inventory: ");

        if (!products.Succeeded)
        {
            var getAllResponse = _productService.GetAllProductsFromList();
            Console.WriteLine(getAllResponse.Message);
        }
        else
        {
            foreach (var printProduct in (IEnumerable<Product>)products.Result!)
            {
                Console.WriteLine($"\n\t Product ID: {printProduct.Id}" +
                $"\n\t Product: {printProduct.Name}" +
                $"\n\t Price: {printProduct.Price} kr");
            }
           
            Console.WriteLine("\n\t Please type or copy the product ID you want to remove from the inventory.");
            Console.Write("\n\t Product ID: ");
            var productId = Console.ReadLine() ?? "";

            var response = _productService.DeleteProduct(productId);
            Console.Clear();
            Console.WriteLine(response.Message);
            Console.Write("\n\t Press any key to continue. ");
        }
    }

    public static void UpdateProductMenu()
    {
        var products = _productService.GetAllProductsFromList();

        Console.Clear();
        Console.WriteLine("\n\t The inventory: ");

        if (!products.Succeeded)
        {
            var getAllResponse = _productService.GetAllProductsFromList();
            Console.WriteLine(getAllResponse.Message);
        }
        else
        {
            foreach (var printProduct in (IEnumerable<Product>)products.Result!)
            {
                Console.WriteLine($"\n\t Product ID: {printProduct.Id}" +
                $"\n\t Product: {printProduct.Name}" +
                $"\n\t Price: {printProduct.Price} kr");
            }

            Console.WriteLine("\n\t Please type or copy the product ID you want to update.");
            Console.Write("\n\t Product ID: ");
            string productId = Console.ReadLine() ?? "";

            var product = new Product();

            Console.Clear();
            Console.WriteLine("\n\t Please type in the new name and price for the product.");

            Console.Write("\n\t Product name: ");
            product.Name = Console.ReadLine() ?? "";

            Console.Write("\t Product price: ");
            decimal.TryParse(Console.ReadLine(), out decimal price);
            product.Price = price;

            var updatedProduct = product;

            var response = _productService.UpdateProduct(productId, updatedProduct);
            Console.WriteLine(response.Message);
            Console.Write("\n\t Press any key to continue. ");
        }
    }
}