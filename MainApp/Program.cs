using MainApp.Menus;
using Resources.Interfaces;
using Resources.Models;
using Resources.Services;

// IProductService<Product, Product> productService = new ProductService(@"c:\programmering_med_c#\projects\console_files\products.json");


Console.WriteLine("\n\t Hello and welcome to the grocery store inventory!");
Console.Write("\n\t Press any key to continue. ");
Console.ReadKey();

while (true)
{
    MainMenu.PrintMainMenu();
}