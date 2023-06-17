<Query Kind="Program" />

void Main()
{
	var trip = new Trip { people = 2, startDate = new DateTime(2023, 03, 30, 15, 00, 00), endDate = new DateTime(2023, 04, 02, 14, 00, 00) };
	

	trip.AddDrinks("Beer", 8, "Cans");
	trip.AddDrinks("Red Wine", 0.5m, "Bottle");
	trip.AddDrinks("Whiskey", 0.05m, "Bottle");
	//trip.AddDrinks("Gin", 0.05m, "Bottle");
	//trip.AddDrinks("Tonic water", 0.4m, "Bottle");
	trip.AddDrinks("Coke Can", 1, "Can");
	trip.AddDrinks("Pack of chips", 0.7m, "Pack");
	trip.AddDrinks("Milk", 0.25m, "Litre");
	trip.AddDrinks("Water", 2.5m, "Litre");
	trip.AddDrinks("Orange juice", 0.2m, "Litre");
	trip.AddDrinks("Koffee", 41, "Gram");
	//trip.AddDrinks("Tea", 1, "Bag");
	//trip.AddDrinks("Hot Chocolate", 0.3m, "Saches");
	
	
	trip.AddMealOption(BreakFast.FrenchToast);
	trip.AddMealOption(BreakFast.Rusks);
	trip.AddMealOption(BreakFast.FruitSalad);
	
	trip.AddMealOption(BreakFast.Rusks);
	trip.AddMealOption(BreakFast.Cereal);
	trip.AddMealOption(BreakFast.Rusks);
	trip.AddMealOption(BreakFast.Cereal);
	trip.AddMealOption(BreakFast.BaconEggs);
	
	//trip.AddMealOption(BreakFast.Cereal);
	//trip.AddMealOption(BreakFast.BaconEggs);
	//trip.AddMealOption(BreakFast.Rusks);
	
	trip.AddMealOption(Side.BraaiBroodtjies);
	trip.AddMealOption(Snack.Biltong);
	trip.AddMealOption(Side.BraaiBroodtjies);
	trip.AddMealOption(Snack.DroeWors);
	trip.AddMealOption(Snack.CheesySnacks);
	//trip.AddMealOption(Snack.Chips + Snack.Cheeses);


	trip.AddMealOption(Dinner.Salmon + Side.Salad);
	trip.AddMealOption(Dinner.ChickenBurger+ Side.Chips);
	trip.AddMealOption(Dinner.Fillet +Side.Chips + Side.DeniseSalad);
		
	
	trip.AddMealOption(Dinner.ApricotChicken+ Side.Rice);	 
	trip.AddMealOption(Dinner.Burgers+ Side.Chips);	
	//trip.AddMealOption(Dinner.ChickenEspetada + Side.BraaiBroodtjies + Side.Salad);
	trip.AddMealOption(Dinner.PorkBelly + Side.Potatoes + Side.Salad);
	//trip.AddMealOption(Dinner.Tuna + Side.BraaiBroodtjies + Side.Salad);
	trip.AddMealOption(Dinner.Steak + Side.Salad);
	trip.AddMealOption(Dinner.Fillet + Side.Salad);
	trip.AddMealOption(Dinner.Wors + Side.BraaiBroodtjies + Side.Salad);
	
	
	//trip.AddMealOption(Dinner.Catered("Graeme"));
	//trip.AddMealOption( (Side.Salad+ Side.Chips).As(Meal.Dinner));
	
	
	//
	//trip.AddMealOption(Dinner.Tuna + Side.Salad);
	//trip.AddMealOption(Dinner.Pizza);
	//trip.AddMealOption(Dinner.Burgers + Side.Chips);	
	// // mushrooms sause
	
	trip.AddMealOption(Dinner.AppricotPork + Side.Salad + Dinner.Bread);
	trip.AddMealOption(Dinner.RoastChicken + Side.Potatoes + Side.Salad);	
	
	
	
	//trip.AddMealOption(Dessert.HotChocolate);
	//trip.AddMealOption(Dessert.BananaChocolate);
	//trip.AddMealOption(Dessert.CamembertBread);
	//
	trip.Calculate();
	trip.PrintDetails();
}

// Define other methods and classes here

class Trip
{
	public int people = 1;
	public DateTime startDate = DateTime.Now.AddDays(1);
	public DateTime endDate = DateTime.Now.AddDays(3);
	public int days = 1;
	public List<SimpleRoundedItem> drinkOptions = new List<SimpleRoundedItem>();
	public List<Recipy> allRecipies = new List<Recipy>();
	public List<ShoppingListItem> shoppingListItem = new List<ShoppingListItem>();
	public List<DayMeal> dayMeals = new List<DayMeal>();

	internal void AddDrinks(string name, decimal unitValue, string unit)
	{
		drinkOptions.Add(new SimpleRoundedItem(name, unitValue, unit));
	}

	internal void AddMealOption(Recipy recipy)
	{
		allRecipies.Add(recipy);
	}

	internal void AddMealOption(object p)
	{
		throw new NotImplementedException();
	}

	internal void Calculate()
	{
		days = (int) Math.Ceiling((endDate - startDate).TotalDays);

		dayMeals.AddRange(Enumerable.Range(1, days + 1)
			.Select(day => DayMeal.From(day, startDate, endDate, allRecipies)));
		
		
		shoppingListItem.AddRange(allRecipies
					.SelectMany(x => x.items.Select(r => new { x.meal,  dr = r.Times(x.used * people) }))
					.GroupBy(g => new { g.dr.name, g.dr.unit, g.dr.inUnit })
					.Select(x => new ShoppingListItem(x.First().meal.ToString(), x.Key.name, Math.Ceiling( x.Sum(r => r.dr.unitValue) / x.Key.inUnit), x.Key.unit ))
					.Where(x=>x.UnitValue > 0m)
					.OrderBy(x => x.Category)
					.ThenBy(x => x.Name));

		shoppingListItem.AddRange(drinkOptions.Select(d => new ShoppingListItem("Drinks", d.name, d.CalculatePerDay(days), d.unit)));
	}

	internal void PrintDetails()
	{
		people.Dump("people");
		startDate.Dump("startData");
		endDate.Dump("endDate");
		days.Dump("days");
		dayMeals.Select(x => new
		{
			x.Day,
			BreakFast = x.BreakFast?.name,
			Snack = x.Snack?.name,
			Dinner = x.Dinner?.name,
			Dessert = x.Dessert?.name
		}).Dump("Menu");
		shoppingListItem.Dump("Shopping");
		allRecipies.Dump("Recipies");
	}
}

class DayMeal
{
	public string Day;
	public Recipy BreakFast;
	public Recipy Snack;
	public Recipy Dinner;
	public Recipy Dessert;

	internal static DayMeal From(int day, DateTime startDate, DateTime endDate, List<Recipy> allRecipies)
	{
		var startOfTheDay = startDate.AddDays(day-1).Date;
		var breakFast = startOfTheDay.AddHours(8);
		var snack = startOfTheDay.AddHours(11);
		var dinner = startOfTheDay.AddHours(18);
		return new DayMeal() {
			Day = startOfTheDay.ToString("ddd dd MMM"),
			BreakFast = DayMeal.PickMeal(breakFast,startDate,endDate,allRecipies,Meal.Breakfast),
			Snack = DayMeal.PickMeal(snack,startDate,endDate,allRecipies,Meal.Snack),
			Dinner = DayMeal.PickMeal(dinner,startDate,endDate,allRecipies,Meal.Dinner),
			Dessert = DayMeal.PickMeal(dinner,startDate,endDate,allRecipies,Meal.Dessert)
		};
	}

	static Recipy PickMeal(DateTime dinner, DateTime startDate, DateTime endDate, List<Recipy> allRecipies, object Dessert)
	{
		throw new NotImplementedException();
	}

	private static Recipy PickMeal(DateTime time, DateTime startDate, DateTime endDate, List<Recipy> allRecipies, Meal breakfast)
	{
		if (time > startDate && time < endDate ) {
			return allRecipies.OrderBy(x=>x.used).Where(x=>x.meal == breakfast).Select(x=>x.MarkUsed()).FirstOrDefault();
		}
		return null;
	}
}

class BasicItems
{
	public static SimpleRoundedItem Eggs(decimal amount = 1) { return new SimpleRoundedItem("Eggs", amount, "eggs", 1); }
	public static SimpleRoundedItem Bacon(decimal amount = 1) { return new SimpleRoundedItem("Bacon", amount, "pack", 2); }
	public static SimpleRoundedItem BreadSlice(decimal amount = 2) { return new SimpleRoundedItem("Bread", amount, "loaf", 24); }
	public static SimpleRoundedItem Honey(decimal amount = 0.01m) { return new SimpleRoundedItem("Honey", amount, "bottle"); }
	public static SimpleRoundedItem Cheese(decimal amount = 20m) { return new SimpleRoundedItem("Cheese", amount, "grams"); }
	public static SimpleRoundedItem TomatoSause(decimal amount = 0.01m) { return new SimpleRoundedItem("Tomato sause", amount, "bottle"); }
	public static SimpleRoundedItem Yogurt(decimal amount = 1m) { return new SimpleRoundedItem("Yogurt", amount, "pack"); }
	public static SimpleRoundedItem FruitSaladSmall(decimal amount = 1m) { return new SimpleRoundedItem("Fruit salad small", amount, "Pack"); }
	public static SimpleRoundedItem Tomato(decimal amount = 1m) { return new SimpleRoundedItem("Tomato", amount, "Tomato"); }
	public static SimpleRoundedItem Milk(decimal amount = 0.1m) { return new SimpleRoundedItem("Milk", amount, "Litre"); }
	public static SimpleRoundedItem Cereal(decimal amount = 0.2m) { return new SimpleRoundedItem("Cereal", amount, "Pack"); }
	public static SimpleRoundedItem Rusks(decimal amount = 1) { return new SimpleRoundedItem("Rusks", amount, "Pack",20); }
	public static SimpleRoundedItem Ham(decimal amount = 1) { return new SimpleRoundedItem("Ham", amount, "Grams"); }
	public static SimpleRoundedItem Potatoe(decimal amount = 2) { return new SimpleRoundedItem("Potatoes", amount, "Potatoes"); }
	public static SimpleRoundedItem Flour(decimal amount = 0.25m) { return new SimpleRoundedItem("Flour", amount, "KG"); }
	public static SimpleRoundedItem Yeast(decimal amount = 1) { return new SimpleRoundedItem("Yeast", amount, "Pack"); }
	public static SimpleRoundedItem Salt(decimal amount = 0.5m) { return new SimpleRoundedItem("Salt", amount, "grams"); }
	public static SimpleRoundedItem Sugar(decimal amount = 0.5m) { return new SimpleRoundedItem("Sugar", amount, "grams"); }
	public static SimpleRoundedItem SaladLeaves(decimal amount = 0.5m) { return new SimpleRoundedItem("Salad Leaves", amount, "pack"); }
	public static SimpleRoundedItem Butter(decimal amount = 20m) { return new SimpleRoundedItem("Butter", amount, "grams"); }
}

class Snack
{
	public static Recipy Biltong = new Recipy()
	{
		name = "Biltong",
		meal = Meal.Snack,
		items = {
			new SimpleRoundedItem("Biltong", 100, "grams")
		}
	};

	public static Recipy DroeWors = new Recipy()
	{
		name = "Droëwors",
		meal = Meal.Snack,
		items = {
			new SimpleRoundedItem("Droëwors", 100, "grams")
		}
	};

	public static Recipy Cheeses = new Recipy()
	{
		name = "Cheeses",
		meal = Meal.Snack,
		items = {
			new SimpleRoundedItem("Camembert", 0.5m, "pack")
		}
	};

	public static Recipy CheesySnacks = new Recipy()
	{
		name = "Cheesy snacks",
		meal = Meal.Snack,
		items = {
			new SimpleRoundedItem("CheesySnacks", 2, "pack")
		}
	};
	
	public static Recipy Chips = new Recipy()
	{
		name = "Chips",
		meal = Meal.Snack,
		items = {
			new SimpleRoundedItem("Chips", 1, "pack")
		}
	};
}

class Side
{
	public static Recipy Potatoes = new Recipy()
	{
		name = "Potatoes",
		meal = Meal.Snack,
		items = {
			BasicItems.Potatoe(2)
		}
	};
	public static Recipy SweetPotatoes = new Recipy()
	{
		name = "Sweet Potatoe",
		meal = Meal.Snack,
		items = {
			new SimpleRoundedItem("Sweet Potatoes", 1m, "item")
		}
	};

	
	public static Recipy Salad = new Recipy()
	{
		name = "Salad",
		meal = Meal.Snack,
		items = {
			new SimpleRoundedItem("Woolies Salad", 0.5m, "pack")
		}
	};
	public static Recipy BraaiBroodtjies = new Recipy()
	{
		name = "Braai broodtjies",
		meal = Meal.Snack,
		items = {
			BasicItems.Butter(),	
			BasicItems.BreadSlice(4),			
			BasicItems.Cheese(50),
			BasicItems.Ham(20),
		}
	};

	public static Recipy DuckFatPotatoes= new Recipy()
	{
		name = "Duck fat potatoes",
		meal = Meal.Snack,
		items = {
			BasicItems.Potatoe(2),
			new SimpleRoundedItem("Duck fat", 100, "ml")
		}
	};


	public static Recipy Chips = new Recipy()
	{
		name = "Chips",
		meal = Meal.Snack,
		items = {
			BasicItems.Potatoe(2)
		}
	};

	public static Recipy Rice = new Recipy()
	{
		name = "Rice",
		meal = Meal.Snack,
		items = {
			new SimpleRoundedItem("Rice", 125, "grams")
		}
	};
	
	public static Recipy DeniseSalad = new Recipy()
	{
		name = "Denise Salad",
		meal = Meal.Snack,
		items = {
			new SimpleRoundedItem("Sweet basil", 0.25m, "punnet"),
			new SimpleRoundedItem("Spanspek", 0.25m, "fruit",1),
			new SimpleRoundedItem("Mozzarella balls", 1, "balls",1),
			new SimpleRoundedItem("Balsamic glaze", 0.01m, "bottle",1),
			new SimpleRoundedItem("Cherry tomatoes", 0.25m, "punnet",1),
			new SimpleRoundedItem("Prosciutto", 0.25m, "pack",1),
		}
	};
}

class Dessert
{
	public static Recipy HotChocolate = new Recipy()
	{
		name = "Hot Chocolate",
		meal = Meal.Dessert,
		items = {
			new SimpleRoundedItem("Hot Chocolate", 1, "pack")
		}
	};
	public static Recipy BananaChocolate = new Recipy()
	{
		name = "Banana Chocolate",
		meal = Meal.Dessert,
		items = {
			new SimpleRoundedItem("Banana", 1, "Banana"),
			new SimpleRoundedItem("Chocolate", 4, "blocks")
		}
	};
	public static Recipy CamembertBread = new Recipy()
	{
		name = "Camembert + Bread",
		meal = Meal.Dessert,
		items = {
			new SimpleRoundedItem("Camembert", 0.5m, "pack"),
			new SimpleRoundedItem("Bread Roll", 0.5m, "Roll")
		}
	};


}

class Dinner
{

	public static Recipy Catered(String Name)
	{
		return new Recipy()
		{
			name = "C-"+Name,
			meal = Meal.Dinner,
			items = {}
		};
	}

	public static Recipy ChickenEspetada = new Recipy()
	{
		name = "Chicken Espetada",
		meal = Meal.Dinner,
		items = {
			new SimpleRoundedItem("Chicken Espetada", 0.5m, "pack")
		}
	};
	
	public static Recipy Salmon = new Recipy()
	{
		name = "Salmon",
		meal = Meal.Dinner,
		items = {
			new SimpleRoundedItem("Salmon", 200, "grams")
		}
	};

	public static Recipy PortFillet = new Recipy()
	{
		name = "Port Fillet",
		meal = Meal.Dinner,
		items = {
			new SimpleRoundedItem("Port Fillet", 200, "grams")
		}
	};

	public static Recipy RoastChicken = new Recipy()
	{
		name = "Roast chicken",
		meal = Meal.Dinner,
		items = {
			new SimpleRoundedItem("Chicken", 1, "half chicken")
		}
	};
	
	public static Recipy Fillet = new Recipy()
	{
		name = "Fillet",
		meal = Meal.Dinner,
		items = {
			new SimpleRoundedItem("Fillet", 250, "grams")
		}
	};

	public static Recipy Wors = new Recipy()
	{
		name = "Wors",
		meal = Meal.Dinner,
		items = {
			new SimpleRoundedItem("Wors", 250, "grams")
		}
	};
	

	public static Recipy AppricotPork = new Recipy()
	{
		name = "Appricot Pork",
		meal = Meal.Dinner,
		items = {
				new SimpleRoundedItem("Pork", 200, "grams"),
				new SimpleRoundedItem("Appricot-Jam", 0.15m, "tin"),
				new SimpleRoundedItem("Mayo", 0.10m, "bottle"),
				new SimpleRoundedItem("Onion", 0.15m, "Onion"),
				new SimpleRoundedItem("Garlic", 0.1m, "Garlic"),
				new SimpleRoundedItem("Chutney", 0.15m, "bottle")
			}
	};
	public static Recipy Tuna = new Recipy()
	{
		name = "Tuna",
		meal = Meal.Dinner,
		items = {
			new SimpleRoundedItem("Tuna", 200, "grams")
		}
	};


	public static Recipy PorkBelly = new Recipy()
	{
		name = "Pork Belly",
		meal = Meal.Dinner,
		items = {
			new SimpleRoundedItem("Pork Belly", 250, "grams",1)
		}
	};
	
	public static Recipy Burgers = new Recipy()
	{
		name = "Burgers",
		meal = Meal.Dinner,
		items = {
			new SimpleRoundedItem("Burgers Patty", 250, "grams"),
			new SimpleRoundedItem("Burger Bun", 1, "Bun"),
			BasicItems.Cheese(),
			BasicItems.SaladLeaves(),
			BasicItems.Tomato(0.3m),
		}
	};

	public static Recipy ApricotChicken = new Recipy()
	{
		name = "Apricot chicken",
		meal = Meal.Dinner,
		items = {
			new SimpleRoundedItem("Chicken With Skin", 200, "grams"),
			new SimpleRoundedItem("Mayo",  0.10m, "bottle"),
			new SimpleRoundedItem("Apricot Yam", 124, "ml"),
			new SimpleRoundedItem("Garlic", 1, "table spoon"),
			new SimpleRoundedItem("Onion", 0.25m, "onion"),
			new SimpleRoundedItem("Peri Peri", 0.10m, "bottle"),
			new SimpleRoundedItem("Worcestor Sauce",0.10m, "bottle"),
		}
	};
	
	public static Recipy ChickenBurger = new Recipy()
	{
		name = "Chicken Burgers",
		meal = Meal.Dinner,
		items = {
			new SimpleRoundedItem("Chicken breasts", 250, "grams"),
			BasicItems.Flour(0.1m),
			new SimpleRoundedItem("Lightly salted chips", 100, "grams"),
			BasicItems.Eggs(1),
			
			new SimpleRoundedItem("Burger bread", 1, "Bun"),
			new SimpleRoundedItem("Tin of pineapple", 0.2m, "can"),
			new SimpleRoundedItem("Mayo",  0.10m, "bottle"),
			new SimpleRoundedItem("PeriPeri", 50, "ml"),
			BasicItems.Cheese(),
			BasicItems.SaladLeaves(),
			BasicItems.Tomato(0.3m),
		}
	};
	


	public static Recipy Steak = new Recipy()
	{
		name = "Steak + Mushroom sauce",
		meal = Meal.Dinner,
		items = {
			new SimpleRoundedItem("Beef Steak", 250, "grams"),
			new SimpleRoundedItem("Mushroom sauce", 100, "ml")
		}
	};

	public static Recipy Bread = new Recipy()
	{
		name = "Bread",
		meal = Meal.Dinner,
		items = {			
			BasicItems.Flour(),
			BasicItems.Yeast(),
			BasicItems.Salt(),
			BasicItems.Sugar()
		}
	};

	

	public static Recipy Pizza = new Recipy()
	{
		name = "Pizza",
		meal = Meal.Dinner,
		items = {
			new SimpleRoundedItem("Tomatoe paste", 0.2m, "small can"),
			new SimpleRoundedItem("Tin of pineapple", 0.5m, "can"),
			BasicItems.Cheese(100),
			BasicItems.Ham(100),
			BasicItems.Flour(),
			BasicItems.Yeast(),
			BasicItems.Salt(),
			BasicItems.Sugar()
		}
	};
}

class BreakFast
{
	public static Recipy FrenchToast = new Recipy()
	{
		name = "French toast",
		meal = Meal.Breakfast,
		items = {
			BasicItems.Eggs(2),
			BasicItems.BreadSlice(2),
			BasicItems.Honey(),
			BasicItems.Cheese(),
			BasicItems.TomatoSause(),
			BasicItems.Bacon()
		}
	};

	public static Recipy BaconEggs = new Recipy()
	{
		name = "Bacon Eggs",
		meal = Meal.Breakfast,
		items = {
			BasicItems.Eggs(2),
			BasicItems.BreadSlice(2),
			BasicItems.Tomato(0.2m),
			BasicItems.Cheese(150),
			BasicItems.TomatoSause(),
			BasicItems.Bacon()
		}
	};

	public static Recipy Cereal = new Recipy()
	{
		name = "Cereal",
		meal = Meal.Breakfast,
		items = {
			
			
			BasicItems.Milk(),
			BasicItems.Cereal(),
		}
	}; 
	public static Recipy FruitSalad = new Recipy()
	{
		name = "Fruit Salad",
		meal = Meal.Breakfast,
		items = {
			BasicItems.Yogurt(),
			BasicItems.FruitSaladSmall(),
			BasicItems.Honey(),
		}
	};

	public static Recipy Rusks = new Recipy()
	{
		name = "Rusks",
		meal = Meal.Breakfast,
		items = {
			BasicItems.Rusks(2),
		}
	};
	
}

class ShoppingListItem
{
	public string Category;
	public string Name;
	public decimal UnitValue;
	public string Unit;

	public ShoppingListItem(string category, string name, decimal unitValue, string unit)
	{
		this.Category = category;
		this.Name = name;
		this.UnitValue = unitValue;
		this.Unit = unit;
	}
}

class SimpleRoundedItem
{
	public string name { get ; }
	public decimal unitValue { get ; }
	public string unit { get ; }
	public decimal inUnit { get ; }

	public SimpleRoundedItem(string name, decimal unitValue, string unit, decimal inUnit = 1)
	{
		this.name = name;
		this.unitValue = unitValue;
		this.unit = unit;
		this.inUnit = inUnit;
	}

	internal decimal CalculatePerDay(int days)
	{
		return Math.Ceiling((unitValue * days) / inUnit);
	}

	internal decimal OfUnit()
	{
		return Math.Ceiling((unitValue) / inUnit);
	}
	
	internal SimpleRoundedItem Times(int amount)
	{
		return new SimpleRoundedItem(this.name, this.unitValue*amount, this.unit, this.inUnit);
	}
}
enum Meal
{
	Breakfast,
	Snack,
	Dinner,
	Dessert
}

class Recipy
{
	public String name;
	public Meal meal;
	public List<SimpleRoundedItem> items = new List<SimpleRoundedItem>();
	public int used = 0;

	internal Recipy As(Meal mealType)
	{
		this.meal = mealType;
		return this;
		
	}

	internal Recipy MarkUsed()
	{
		used++;
		return this;
	}

	public static Recipy operator +(Recipy b, Recipy c)
	{
		return new Recipy()
		{
			name = b.name + " + " + c.name,
			meal = b.meal,
			items = b.items.Concat(c.items).ToList(),
			used = 0
		};
	}
}