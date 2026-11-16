using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Core.View;

public static class ConsoleInput
{
    public static string ReadString(string prompt)
    {
        while (true)
        {
            Console.Write($"{prompt}: ");
            string s = Console.ReadLine()?.Trim() ?? "";
            if (s.Length > 0) return s;
            Console.WriteLine("Polje ne sme biti prazno.");
        }
    }

    public static string? ReadOptional(string prompt)
    {
        Console.Write($"{prompt} (Enter = preskoci): ");
        string s = Console.ReadLine()?.Trim() ?? "";
        return s.Length == 0 ? null : s;
    }

    public static int ReadInt(string prompt, int min = int.MinValue, int max = int.MaxValue)
    {
        while (true)
        {
            Console.Write($"{prompt}: ");
            if (int.TryParse(Console.ReadLine(), out int v) && v >= min && v <= max) return v;
            Console.WriteLine($"Unesite ceo broj ({min} do {max}).");
        }
    }

    public static decimal ReadDecimal(string prompt)
    {
        while (true)
        {
            Console.Write($"{prompt}: ");
            if (decimal.TryParse(Console.ReadLine(), NumberStyles.Number,
                    CultureInfo.InvariantCulture, out decimal v) && v >= 0) return v;
            Console.WriteLine("Unesite nenegativan broj (npr. 999.50).");
        }
    }

    public static DateTime ReadDate(string prompt)
    {
        while (true)
        {
            Console.Write($"{prompt} (dd.MM.yyyy): ");
            if (DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime d)) return d;
            Console.WriteLine("Neispravan datum.");
        }
    }

    public static bool Confirm(string prompt)
    {
        Console.Write($"{prompt} (d/n): ");
        string s = Console.ReadLine()?.Trim().ToLower() ?? "";
        return s == "d" || s == "da";
    }
}
