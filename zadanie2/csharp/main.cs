using System;
using System.Collections.Generic;

class Crop
{
    public string Name;
    public int Growth;
    public int YieldAmount;
    public string WeatherImpact;

    public Crop(string name, int growth, int yieldAmount, string weatherImpact)
    {
        Name = name;
        Growth = growth;
        YieldAmount = yieldAmount;
        WeatherImpact = weatherImpact;
    }

    public void Grow(string weatherType)
    {
        if (weatherType == "sunny")
        {
            Growth += 1;
        }
        else if (weatherType == "rainy")
        {
            Growth += 2;
        }
        else if (weatherType == "stormy")
        {
            Growth += 0;
        }
    }

    public int Harvest()
    {
        if (Growth >= 3)
        {
            return YieldAmount;
        }
        else
        {
            return 0;
        }
    }
}

class Weather
{
    public string WeatherType;

    public Weather(string weatherType)
    {
        WeatherType = weatherType;
    }

    public void ChangeWeather(string newWeather)
    {
        WeatherType = newWeather;
    }
}

class Animal
{
    public string Name;
    public string Species;
    public int Hunger;
    public int Health;

    public Animal(string name, string species, int hunger, int health)
    {
        Name = name;
        Species = species;
        Hunger = hunger;
        Health = health;
    }

    public void Feed()
    {
        Hunger -= 10;
    }

    public void Heal()
    {
        Health += 10;
    }

    public string Produce()
    {
        if (Species == "cow")
        {
            return "milk";
        }
        else if (Species == "chicken")
        {
            return "eggs";
        }
        else
        {
            return "nothing";
        }
    }
}

class Barn
{
    public int Capacity;
    public List<Animal> Animals;

    public Barn(int capacity)
    {
        Capacity = capacity;
        Animals = new List<Animal>();
    }

    public void AddAnimal(Animal animal)
    {
        if (Animals.Count < Capacity)
        {
            Animals.Add(animal);
        }
    }

    public void FeedAnimals()
    {
        foreach (Animal animal in Animals)
        {
            animal.Feed();
        }
    }
}

class Farm
{
    public List<Crop> Crops;
    public Barn Barn;
    public Weather Weather;

    public Farm(Barn barn, Weather weather)
    {
        Crops = new List<Crop>();
        Barn = barn;
        Weather = weather;
    }

    public void PlantCrop(Crop crop)
    {
        Crops.Add(crop);
    }

    public void NextDay()
    {
        foreach (Crop crop in Crops)
        {
            crop.Grow(Weather.WeatherType);
        }
    }

    public void FeedAnimals()
    {
        Barn.FeedAnimals();
    }

    public int HarvestAll()
    {
        int totalYield = 0;

        foreach (Crop crop in Crops)
        {
            totalYield += crop.Harvest();
        }

        return totalYield;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Weather weather = new Weather("sunny");
        Barn barn = new Barn(5);
        Farm farm = new Farm(barn, weather);

        Animal cow = new Animal("Krowa", "cow", 50, 80);
        Animal chicken = new Animal("Kura", "chicken", 30, 90);

        barn.AddAnimal(cow);
        barn.AddAnimal(chicken);

        Crop wheat = new Crop("Wheat", 0, 5, "rainy");
        farm.PlantCrop(wheat);

        farm.NextDay();
        farm.NextDay();
        farm.NextDay();

        farm.FeedAnimals();

        Console.WriteLine("Pogoda: " + farm.Weather.WeatherType);
        Console.WriteLine("Wzrost rośliny: " + wheat.Growth);
        Console.WriteLine("Krowa produkuje: " + cow.Produce());
        Console.WriteLine("Kura produkuje: " + chicken.Produce());
        Console.WriteLine("Zebrany plon: " + farm.HarvestAll());
    }
}
