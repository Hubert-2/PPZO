liczba1 = float(input("Wprowadź liczbę 1: "))
liczba2 = float(input("Wprowadź liczbę 2: "))
operacja = input("Jaką operację chcesz wykonać? (+ - / *): ")

if operacja == "+":
    wynik = liczba1 + liczba2
    print("Wynik dodawania:", wynik)

elif operacja == "-":
    wynik = liczba1 - liczba2
    print("Wynik odejmowania:", wynik)

elif operacja == "*":
    wynik = liczba1 * liczba2
    print("Wynik mnożenia:", wynik)

elif operacja == "/" :
    if liczba2 != 0:
        wynik = liczba1 / liczba2
        print("Wynik dzielenia:", wynik)
    else:
        print("Nie można dzielić przez zero!")
    
else:
    print("Niepoprawna operacja!")

