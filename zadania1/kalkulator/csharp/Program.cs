using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Podaj pierwszą liczbę: ");
double liczba1 = double.Parse(Console.ReadLine());

Console.WriteLine("Podaj drugą liczbę: ");
double liczba2 = double.Parse(Console.ReadLine());

Console.WriteLine("Jaką operację chcesz wykonać? (+ - / *)");
string operacja = Console.ReadLine();

if (operacja == "+")
{
    double wynik = liczba1 + liczba2;
    Console.WriteLine("Wynik dodawania: " + wynik);
}

else if (operacja == "-")
{
    double wynik = liczba1 - liczba2;
    Console.WriteLine("Wynik odejmowania: " + wynik);
}

else if (operacja == "/")
{
    if (liczba2 == 0)
    {
        Console.WriteLine("Nie można dzielić przez 0!");
    }

    else
    {
        double wynik = liczba1 / liczba2;
        Console.WriteLine("Wynik dzielenia: " + wynik);
    }
}

else if (operacja == "*")
{
    double wynik = liczba1 * liczba2;
    Console.WriteLine("Wynik mnożenia: " + wynik);
}

else
{
    Console.WriteLine("Niepoprawna operacja!");
}
    }
}
