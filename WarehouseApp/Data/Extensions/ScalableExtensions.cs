using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Data;

namespace WarehouseApp.Data.Extensions
{
    internal class ScalableExtensions
    {
    }
}


public static class ScalableExtensions
{
    /// <summary>
    /// Adds NorthwindContext to the specified IServiceCollection. Uses the Sqlite database provider.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="relativePath">Set to override the default of ".."</param>
    /// <returns>An IServiceCollection that can be used to add more services.</returns>
    public static void WriteToConsole(this Scalable scalable, string label = "")
    {
        var consoleColor = Console.ForegroundColor;

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($"{label} l:");
        Console.ForegroundColor = consoleColor;
        Console.Write(scalable.Length);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(" w:");
        Console.ForegroundColor = consoleColor;
        Console.Write(scalable.Width);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(" h:");
        Console.ForegroundColor = consoleColor;
        Console.Write(scalable.Height);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(" weight:");
        Console.ForegroundColor = consoleColor;
        Console.Write(scalable.Weight);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(" exp.date:");
        Console.ForegroundColor = consoleColor;
        Console.Write($"{scalable.ExpirationDate:dd.MM.yyyy}");

        Console.ForegroundColor = consoleColor;
        Console.WriteLine();
    }
}
