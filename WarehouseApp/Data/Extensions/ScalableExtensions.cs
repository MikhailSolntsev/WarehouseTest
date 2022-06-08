using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Data;

namespace WarehouseApp.Data.Extensions;

public static class ScalableExtensions
{
    /// <summary>
    /// Extension to write Scalable classes to System.Console.
    /// </summary>
    /// <param name="scalable">Object inherited from Scalable</param>
    /// <param name="label">String that writing before scalable: \t. class name etc.</param>
    /// <returns>void</returns>
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
