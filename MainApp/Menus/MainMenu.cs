using Resources.Models;
using Resources.Services;

namespace MainApp.Menus;

internal static class MainMenu
{
    public static void PrintMainMenu()
    {
        Console.Clear();
        Console.WriteLine("\n\t What do you want to do today?");
        Console.WriteLine("\n\t 1 - Add product to inventory");
        Console.WriteLine("\t 2 - List all products");
        Console.WriteLine("\t 3 - Remove a product from inventory");
        Console.WriteLine("\t 4 - Update existing product name and price");
        Console.WriteLine("\t 0 - Exit");

        Console.Write("\n\t Enter option: ");

        var result = MenuOptions(Console.ReadLine() ?? "");
        if (!result)
        {
            Console.Clear();
            Console.WriteLine("\n\t Invalid option! Please pick a menu option between 1-4, " +
                "\n\t or exit the application with 0.");
            Console.ReadKey();
        }
    }

    private static void ExitApplicationMenu()
    {
        Console.Clear();
        Console.WriteLine("\n\t Are you sure you want to exit the inventory? (y/n)");
        var response = Console.ReadLine() ?? "n";
        if (response.ToLower() == "y")
        {
            Environment.Exit(0);
        }
    }

    private static bool MenuOptions(string selectedOption)
    {
        if (int.TryParse(selectedOption, out int option))
        {
            switch (option)
            {
                case 1:
                    ProductMenu.CreateProductMenu();
                    Console.ReadKey();
                    break;

                case 2:
                    ProductMenu.GetAllProductsFromListMenu();
                    Console.ReadKey();
                    break;

                case 3:
                    ProductMenu.DeleteProductMenu();
                    Console.ReadKey();
                    break;

                case 4:
                    break;

                case 0:
                    ExitApplicationMenu();
                    break;

                default:
                    return false;
            }

            return true;
        }

        return false;
    }

}
