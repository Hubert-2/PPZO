using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Podaj liczbę ocen");
        int LiczbaOcen = int.Parse(Console.ReadLine());
        double suma = 0;

        for (int i = 0; i < LiczbaOcen; i++)
        {
            Console.WriteLine("Podaj ocenę: ");
            double ocena = double.Parse(Console.ReadLine());
            suma = suma + ocena;
        }

        double srednia = suma / LiczbaOcen;
        Console.WriteLine("Średnia wynosi: " + srednia);

        if (srednia >= 3.0)
        {
            Console.WriteLine("Uczeń zdał");
        }
        else
        {
            Console.WriteLine("Uczeń nie zdał");
        }

    }
}


