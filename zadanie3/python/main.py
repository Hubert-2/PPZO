from abc import ABC, abstractmethod
from datetime import date


class ElectricVehicle(ABC):
    @abstractmethod
    def charge(self):
        pass


class Vehicle(ABC):
    def __init__(self, id, brand="", model="", rental_rate=0):
        self.id = id
        self.brand = brand
        self.model = model
        self.rental_rate = rental_rate
        self.is_available = True

    @property
    def rental_rate(self):
        return self._rental_rate

    @rental_rate.setter
    def rental_rate(self, value):
        if value < 0:
            value = 0
        self._rental_rate = value

    def rent(self, start_date=None):
        if self.is_available:
            self.is_available = False
            if start_date is None:
                print("pojazd wypozyczony")
            else:
                print("pojazd wypozyczony od:", start_date)
        else:
            print("pojazd jest juz wypozyczony")

    def return_vehicle(self):
        self.is_available = True
        print("pojazd zwrocony")

    @abstractmethod
    def vehicle_type(self):
        pass

    def show(self):
        print(self.id, self.brand, self.model, self.vehicle_type(), "stawka:", self.rental_rate, "dostepny:", self.is_available)


class Car(Vehicle):
    def __init__(self, id, brand="", model="", rental_rate=0, fuel_type="", number_of_doors=4):
        super().__init__(id, brand, model, rental_rate)
        self.fuel_type = fuel_type
        self.number_of_doors = number_of_doors

    def vehicle_type(self):
        return "samochod"

    def show(self):
        super().show()
        print("paliwo:", self.fuel_type, "drzwi:", self.number_of_doors)


class Bike(Vehicle):
    def __init__(self, id, brand="", model="", rental_rate=0, has_gears=False):
        super().__init__(id, brand, model, rental_rate)
        self.has_gears = has_gears

    def vehicle_type(self):
        return "rower"

    def show(self):
        super().show()
        print("przerzutki:", self.has_gears)


class Scooter(Vehicle, ElectricVehicle):
    def __init__(self, id, brand="", model="", rental_rate=0, battery_capacity=0):
        super().__init__(id, brand, model, rental_rate)
        self.battery_capacity = battery_capacity

    def vehicle_type(self):
        return "hulajnoga"

    def charge(self):
        print("hulajnoga zostala naladowana")

    def show(self):
        super().show()
        print("bateria:", self.battery_capacity)


class HybridCar(Car, ElectricVehicle):
    def __init__(self, id, brand="", model="", rental_rate=0, fuel_type="hybryda", number_of_doors=4, battery_capacity=0):
        super().__init__(id, brand, model, rental_rate, fuel_type, number_of_doors)
        self.battery_capacity = battery_capacity

    def vehicle_type(self):
        return "samochod hybrydowy"

    def charge(self):
        print("samochod hybrydowy zostal naladowany")

    def show(self):
        super().show()
        print("bateria:", self.battery_capacity)


class User:
    def __init__(self, name, surname):
        self.name = name
        self.surname = surname
        self.history = []

    @property
    def name(self):
        return self._name

    @name.setter
    def name(self, value):
        if value == "":
            value = "brak"
        self._name = value

    @property
    def surname(self):
        return self._surname

    @surname.setter
    def surname(self, value):
        if value == "":
            value = "brak"
        self._surname = value

    def show(self):
        print(self.name, self.surname)


class Rental:
    def __init__(self, user, vehicle, start_date, end_date=None):
        self.user = user
        self.vehicle = vehicle
        self.start_date = start_date
        self.end_date = end_date

    def show(self):
        print(self.user.name, self.user.surname, "-", self.vehicle.brand, self.vehicle.model, "-", self.start_date, "-", self.end_date)


class Fleet:
    def __init__(self):
        self.vehicles = []

    def add_vehicle(self, vehicle):
        self.vehicles.append(vehicle)

    def remove_vehicle(self, vehicle_id):
        for v in self.vehicles:
            if v.id == vehicle_id:
                self.vehicles.remove(v)
                print("usunieto pojazd")
                return
        print("nie znaleziono pojazdu")

    def find_vehicle(self, vehicle_id):
        for v in self.vehicles:
            if v.id == vehicle_id:
                return v
        return None

    def show_all(self):
        for v in self.vehicles:
            v.show()
            print()


class RentalManager:
    rentals = []

    @staticmethod
    def rent_vehicle(user, fleet, vehicle_id, start_date=None):
        vehicle = fleet.find_vehicle(vehicle_id)

        if vehicle is None:
            print("nie znaleziono pojazdu")
            return

        if not vehicle.is_available:
            print("pojazd niedostepny")
            return

        if start_date is None:
            start_date = date.today()

        vehicle.rent(start_date)
        rental = Rental(user, vehicle, start_date)
        RentalManager.rentals.append(rental)
        user.history.append(rental)

    @staticmethod
    def return_vehicle(vehicle_id):
        for rental in RentalManager.rentals:
            if rental.vehicle.id == vehicle_id and rental.end_date is None:
                rental.end_date = date.today()
                rental.vehicle.return_vehicle()
                return

        print("nie znaleziono aktywnego wypozyczenia")

    @staticmethod
    def show_rentals():
        for rental in RentalManager.rentals:
            rental.show()

    @staticmethod
    def report():
        cars = 0
        bikes = 0
        scooters = 0
        hybrids = 0

        for rental in RentalManager.rentals:
            if rental.end_date is None:
                typ = rental.vehicle.vehicle_type()

                if typ == "samochod":
                    cars += 1
                elif typ == "rower":
                    bikes += 1
                elif typ == "hulajnoga":
                    scooters += 1
                elif typ == "samochod hybrydowy":
                    hybrids += 1

        print("wynajete samochody:", cars)
        print("wynajete rowery:", bikes)
        print("wynajete hulajnogi:", scooters)
        print("wynajete hybrydy:", hybrids)


fleet = Fleet()

fleet.add_vehicle(Car(1, "Opel", "Astra", 120, "benzyna", 5))
fleet.add_vehicle(Bike(2, "Kross", "Hexagon", 40, True))
fleet.add_vehicle(Scooter(3, "Xiaomi", "Mi Scooter", 60, 30))
fleet.add_vehicle(HybridCar(4, "Toyota", "Prius", 150, "hybryda", 5, 50))

user = User("Hubert", "Biernat")

wybor = 0

while wybor != 6:
    print("1. pokaz pojazdy")
    print("2. wypozycz pojazd")
    print("3. zwroc pojazd")
    print("4. pokaz wypozyczenia")
    print("5. raport")
    print("6. wyjscie")

    wybor = int(input("wybor: "))

    if wybor == 1:
        fleet.show_all()

    elif wybor == 2:
        id = int(input("podaj id pojazdu: "))
        RentalManager.rent_vehicle(user, fleet, id)

    elif wybor == 3:
        id = int(input("podaj id pojazdu: "))
        RentalManager.return_vehicle(id)

    elif wybor == 4:
        RentalManager.show_rentals()

    elif wybor == 5:
        RentalManager.report()

    elif wybor == 6:
        print("koniec programu")

    else:
        print("zly wybor")
