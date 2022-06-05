using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WarehouseApp.Data
{
    public class Scalable
    {
        public virtual int Id { get; protected set; }
        public int Height { get; }
        public int Width { get; }
        public int Length { get; }
        public virtual int Weight { get; }
        public virtual int Volume { get { return Height * Width * Length; } }
        public virtual DateTime ExpirationDate { get; protected set; }
        public Scalable(int height, int width, int length, int weight)
        {
            Height  = height;
            Width   = width;
            Length  = length;
            Weight  = weight;
        }
        public void WriteElement(string label = "")
        {
            ConsoleColor consoleColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{label} l:");
            Console.ForegroundColor = consoleColor;
            Console.Write(Length);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" w:");
            Console.ForegroundColor = consoleColor;
            Console.Write(Width);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" h:");
            Console.ForegroundColor = consoleColor;
            Console.Write(Height);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" weight:");
            Console.ForegroundColor = consoleColor;
            Console.Write(Weight);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" exp.date:");
            Console.ForegroundColor = consoleColor;
            Console.Write($"{ExpirationDate:dd.MM.yyyy}");

            Console.ForegroundColor = consoleColor;
            Console.WriteLine();
        }
        
    }
}
