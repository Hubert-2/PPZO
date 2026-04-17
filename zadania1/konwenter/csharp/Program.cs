using System;
class Program
{
static void Main(string[] args)
    {
        Console.WriteLine("Podaj skalę (C F): ");
        string skala = Console.ReadLine();

            if (skala == "C")
            {
                Console.WriteLine("Podaj temperature w °C");
                double temperatura = double.Parse(Console.ReadLine());
                double konwersja = temperatura * 1.8 + 32;
                Console.WriteLine(temperatura + "°C = " + konwersja.ToString("F1") + "°F");
            }

            else if (skala == "F")
            {
                Console.WriteLine("Podaj temperaturę w °F");
                double temperatura = double.Parse(Console.ReadLine());
                double konwersja = (temperatura - 32) / 1.8;
                Console.WriteLine(temperatura + "°F = " + konwersja.ToString("F1") + "°C");
            }

            else
            {
                Console.WriteLine("Niepoprawna skala!");
            }
    }
}
