namespace RestaurantReservation.Outputter;

public class ConsoleDisplay
{
    public static void MessageDisplay(string message, bool newLine = true)
    {
        if (newLine)
        {
            Console.WriteLine(message);
        }
        else
        {
            Console.Write(message);
        }
    }
}