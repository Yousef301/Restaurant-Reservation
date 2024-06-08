namespace RestaurantReservation.ConsoleIO.ConsoleOutput;

public class Menus
{
    public static void PrintEntityMenu()
    {
        Console.WriteLine("1. CRUD On Customers");
        Console.WriteLine("2. List Managers");
        Console.WriteLine("3. Get Reservations for a Customer");
        Console.WriteLine("4. List Orders and Menu Items");
        Console.WriteLine("5. List Ordered Menu Items");
        Console.WriteLine("6. Calculate Average Order Amount for and Employee");
        Console.WriteLine("7. Views Queries");
        Console.WriteLine("8. Calculate Total Revenue for a Restaurant");
        Console.WriteLine("9. Find Customers based on Party Size");
        Console.WriteLine("10. Exit");
    }

    public static void PrintCRUDMenu(string entity)
    {
        Console.WriteLine($"1. Add {entity}");
        Console.WriteLine($"2. Update {entity}");
        Console.WriteLine($"3. Delete {entity}");
        Console.WriteLine($"4. Get {entity}");
        Console.WriteLine("5. Exit");
    }

    public static void PrintViewsMenu()
    {
        Console.WriteLine("1. Get Reservations Details");
        Console.WriteLine("2. Get Employees Details");
        Console.WriteLine("3. Exit");
    }
}