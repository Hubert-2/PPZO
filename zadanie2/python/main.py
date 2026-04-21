#Symulator farmy - 25#
class Farm:
    def __init__(self, barn, weather):
        self.crops = []
        self.barn = barn
        self.weather = weather
    
    def plant_crop(self, crop):
        self.crops.append(crop)

    def next_day(self):
        for crop in self.crops:
            crop.grow(self.weather.weather_type)

    def feed_animals(self):
        self.barn.feed_animals()

    def harvest_all(self):
        total_yield = 0
        for crop in self.crops:
            total_yield += crop.harvest()
        return total_yield
        

class Crop:
    def __init__(self, name, growth, yield_amount, weather_impact):
        self.name = name
        self.growth = growth
        self.yield_amount = yield_amount
        self.weather_impact = weather_impact
    
    def grow(self, weather_type):
        if weather_type == "sunny":
            self.growth += 1
        elif weather_type == "rainy":
            self.growth += 2
        elif weather_type == "stormy":
            self.growth += 0

    def harvest(self):
        if self.growth >= 3:
            return self.yield_amount
        else:
            return 0
    
class Weather:
    def __init__(self, weather_type):
        self.weather_type = weather_type

    def change_weather(self, new_weather):
        self.weather_type = new_weather

class Animal:
    def __init__(self, name, species, hunger, health):
        self.name = name
        self.species = species
        self.hunger = hunger
        self.health = health

    def feed(self):
        self.hunger -= 10
        
    def heal(self):
        self.health +=10

    def produce(self):
        if self.species == "cow":
            return "milk"
        elif self.species == "chicken":
            return "eggs"
        else:
            return None
        
class Barn:
    def __init__(self, capacity):
        self.capacity = capacity
        self.animals = []
    
    def add_animal(self, animal):
        if len(self.animals) < self.capacity:
            self.animals.append(animal)

    def feed_animals(self):
        for animal in self.animals:
            animal.feed()
    
weather = Weather("sunny")
barn = Barn(5)
farm = Farm(barn, weather)

cow = Animal("Krowa", "cow", 50, 80)
chicken = Animal("Kura", "chicken", 30, 90)

barn.add_animal(cow)
barn.add_animal(chicken)

wheat = Crop("Wheat", 0, 5, "rainy")
farm.plant_crop(wheat)

farm.next_day()
farm.feed_animals()

print("Pogoda:", farm.weather.weather_type)
print("Wzrost rośliny:", wheat.growth)
print("Krowa produkuje:", cow.produce())
print("Kura produkuje:", chicken.produce())
print("Zebrany plon:", farm.harvest_all())
