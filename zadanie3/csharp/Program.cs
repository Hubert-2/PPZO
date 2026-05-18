using System;
using System.Collections;

abstract class Vehicle
{
    public int id;
    public string brand;
    public string model;
    private double rental_rate;
    public bool is_available;

    public double RentalRate
    {
        get { return rental_rate; }
        set
        {
            if (value < 0)
                rental_rate = 0;
            else
                rental_rate = value;
        }
    }

    public Vehicle(int id)
    {
        this.id = id;
        this.brand = "";
        this.model = "";
        this.RentalRate = 0;
        this.is_available = true;
    }

    public Vehicle(int id, string brand, string model, double rental_rate)
    {
        this.id = id;
        this.brand = brand;
        this.model = model;
        this.RentalRate = rental_rate;
        this.is_available = true;
    }

    public void rent()
    {
        if (is_available)
        {
            is_available = false;
            Console.WriteLine("pojazd wypozyczony");
        }
        else
        {
            Console.WriteLine("pojazd jest juz wypozyczony");
        }
    }

    public void rent(DateTime start_date)
    {
        if (is_available)
        {
            is_available = false;
            Console.WriteLine("pojazd wypozyczony od: " + start_date.ToShortDateString());
        }
        else
        {
            Console.WriteLine("pojazd jest juz wypozyczony");
        }
    }

    public void return_vehicle()
    {
        is_available = true;
        Console.WriteLine("pojazd zwrocony");
    }

    public abstract string vehicle_type();

    public virtual void show()
    {
        Console.WriteLine(id + " " + brand + " " + model + " " + vehicle_type() + " stawka: " + RentalRate + " dostepny: " + is_available);
    }
}

interface ElectricVehicle
{
    void charge();
}

class Car : Vehicle
{
    public string fuel_type;
    public int number_of_doors;

    public Car(int id) : base(id)
    {
        fuel_type = "";
        number_of_doors = 4;
    }

    public Car(int id, string brand, string model, double rental_rate, string fuel_type, int number_of_doors)
        : base(id, brand, model, rental_rate)
    {
        this.fuel_type = fuel_type;
        this.number_of_doors = number_of_doors;
    }

    public override string vehicle_type()
    {
        return "samochod";
    }

    public override void show()
    {
        base.show();
        Console.WriteLine("paliwo: " + fuel_type + " drzwi: " + number_of_doors);
    }
}

class Bike : Vehicle
{
    public bool has_gears;

    public Bike(int id, string brand, string model, double rental_rate, bool has_gears)
        : base(id, brand, model, rental_rate)
    {
        this.has_gears = has_gears;
    }

    public override string vehicle_type()
    {
        return "rower";
    }

    public override void show()
    {
        base.show();
        Console.WriteLine("przerzutki: " + has_gears);
    }
}

class Scooter : Vehicle, ElectricVehicle
{
    public int battery_capacity;

    public Scooter(int id, string brand, string model, double rental_rate, int battery_capacity)
        : base(id, brand, model, rental_rate)
    {
        this.battery_capacity = battery_capacity;
    }

    public override string vehicle_type()
    {
        return "hulajnoga";
    }

    public void charge()
    {
        Console.WriteLine("hulajnoga zostala naladowana");
    }

    public override void show()
    {
        base.show();
        Console.WriteLine("bateria: " + battery_capacity);
    }
}

class HybridCar : Car, ElectricVehicle
{
    public int battery_capacity;

    public HybridCar(int id, string brand, string model, double rental_rate, string fuel_type, int number_of_doors, int battery_capacity)
        : base(id, brand, model, rental_rate, fuel_type, number_of_doors)
    {
        this.battery_capacity = battery_capacity;
    }

    public override string vehicle_type()
    {
        return "samochod hybrydowy";
    }

    public void charge()
    {
        Console.WriteLine("samochod hybrydowy zostal naladowany");
    }

    public override void show()
    {
        base.show();
        Console.WriteLine("bateria: " + battery_capacity);
    }
}

class User
{
    private string name;
    private string surname;
    public ArrayList history;

    public string Name
    {
        get { return name; }
        set
        {
            if (value == "")
                name = "brak";
            else
                name = value;
        }
    }

    public string Surname
    {
        get { return surname; }
        set
        {
            if (value == "")
                surname = "brak";
            else
                surname = value;
        }
    }

    public User(string name, string surname)
    {
        Name = name;
        Surname = surname;
        history = new ArrayList();
    }

    public void show()
    {
        Console.WriteLine(Name + " " + Surname);
    }
}

class Rental
{
    public User user;
    public Vehicle vehicle;
    public DateTime start_date;
    public DateTime? end_date;

    public Rental(User user, Vehicle vehicle, DateTime start_date)
    {
        this.user = user;
        this.vehicle = vehicle;
        this.start_date = start_date;
        this.end_date = null;
    }

    public void show()
    {
        Console.WriteLine(user.Name + " " + user.Surname + " - " + vehicle.brand + " " + vehicle.model + " - " + start_date.ToShortDateString() + " - " + end_date);
    }
}

class Fleet
{
    public ArrayList vehicles;

    public Fleet()
    {
        vehicles = new ArrayList();
    }

    public void add_vehicle(Vehicle vehicle)
    {
        vehicles.Add(vehicle);
    }

    public void remove_vehicle(int vehicle_id)
    {
        foreach (Vehicle v in vehicles)
        {
            if (v.id == vehicle_id)
            {
                vehicles.Remove(v);
                Console.WriteLine("usunieto pojazd");
                return;
            }
        }

        Console.WriteLine("nie znaleziono pojazdu");
    }

    public Vehicle find_vehicle(int vehicle_id)
    {
        foreach (Vehicle v in vehicles)
        {
            if (v.id == vehicle_id)
                return v;
        }

        return null;
    }

    public void show_all()
    {
        foreach (Vehicle v in vehicles)
        {
            v.show();
            Console.WriteLine();
        }
    }
}

class RentalManager
{
    public static ArrayList rentals = new ArrayList();

    public static void rent_vehicle(User user, Fleet fleet, int vehicle_id)
    {
        Vehicle vehicle = fleet.find_vehicle(vehicle_id);

        if (vehicle == null)
        {
            Console.WriteLine("nie znaleziono pojazdu");
            return;
        }

        if (!vehicle.is_available)
        {
            Console.WriteLine("pojazd niedostepny");
            return;
        }

        vehicle.rent(DateTime.Today);

        Rental rental = new Rental(user, vehicle, DateTime.Today);
        rentals.Add(rental);
        user.history.Add(rental);
    }

    public static void return_vehicle(int vehicle_id)
    {
        foreach (Rental r in rentals)
        {
            if (r.vehicle.id == vehicle_id && r.end_date == null)
            {
                r.end_date = DateTime.Today;
                r.vehicle.return_vehicle();
                return;
            }
        }

        Console.WriteLine("nie znaleziono aktywnego wypozyczenia");
    }

    public static void show_rentals()
    {
        foreach (Rental r in rentals)
        {
            r.show();
        }
    }

    public static void report()
    {
        int cars = 0;
        int bikes = 0;
        int scooters = 0;
        int hybrids = 0;

        foreach (Rental r in rentals)
        {
            if (r.end_date == null)
            {
                string typ = r.vehicle.vehicle_type();

                if (typ == "samochod")
                    cars++;

                else if (typ == "rower")
                    bikes++;

                else if (typ == "hulajnoga")
                    scooters++;

                else if (typ == "samochod hybrydowy")
                    hybrids++;
            }
        }

        Console.WriteLine("wynajete samochody: " + cars);
        Console.WriteLine("wynajete rowery: " + bikes);
        Console.WriteLine("wynajete hulajnogi: " + scooters);
        Console.WriteLine("wynajete hybrydy: " + hybrids);
    }
}

class Program
{
    static void Main()
    {
        Fleet fleet = new Fleet();

        fleet.add_vehicle(new Car(1, "Opel", "Astra", 120, "benzyna", 5));
        fleet.add_vehicle(new Bike(2, "Kross", "Hexagon", 40, true));
        fleet.add_vehicle(new Scooter(3, "Xiaomi", "Mi Scooter", 60, 30));
        fleet.add_vehicle(new HybridCar(4, "Toyota", "Prius", 150, "hybryda", 5, 50));

        User user = new User("Hubert", "Biernat");

        int wybor = 0;

        while (wybor != 6)
        {
            Console.WriteLine("1. pokaz pojazdy");
            Console.WriteLine("2. wypozycz pojazd");
            Console.WriteLine("3. zwroc pojazd");
            Console.WriteLine("4. pokaz wypozyczenia");
            Console.WriteLine("5. raport");
            Console.WriteLine("6. wyjscie");

            Console.Write("wybor: ");
            wybor = int.Parse(Console.ReadLine());

            if (wybor == 1)
            {
                fleet.show_all();
            }

            else if (wybor == 2)
            {
                Console.Write("podaj id pojazdu: ");
                int id = int.Parse(Console.ReadLine());

                RentalManager.rent_vehicle(user, fleet, id);
            }

            else if (wybor == 3)
            {
                Console.Write("podaj id pojazdu: ");
                int id = int.Parse(Console.ReadLine());

                RentalManager.return_vehicle(id);
            }

            else if (wybor == 4)
            {
                RentalManager.show_rentals();
            }

            else if (wybor == 5)
            {
                RentalManager.report();
            }

            else if (wybor == 6)
            {
                Console.WriteLine("koniec programu");
            }

            else
            {
                Console.WriteLine("zly wybor");
            }

            Console.WriteLine();
        }
    }
}
