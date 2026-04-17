skala = input("Podaj skalę (C F): ")

if skala == "C":
    temperatura = float(input("Podaj temperaturę w stopniach Celsjusza: "))
    wynik = temperatura * 1.8 + 32
    print(temperatura,"°C =",wynik,"°F")

elif skala == "F":
    temperatura = float(input("Podaj temperaturę w stopniach Fahrenheita: "))
    wynik = (temperatura - 32) / 1.8
    print(temperatura,"°F =",wynik,"°C" )

else: 
    print("Nieprawidłowe dane!")
