liczba_ocen = int(input("Podaj liczbę ocen: "))
suma = 0

for i in range(liczba_ocen):
    ocena = float(input("Podaj ocenę: "))
    suma = suma + ocena

srednia = suma / liczba_ocen
print("Średnia: ", srednia)

if srednia >= 3.0:
    print("Uczeń zdał")
else:
    print("Uczeń nie zdał")
