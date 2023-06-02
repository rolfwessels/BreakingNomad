namespace BreakingNomad.Ui.Components.MenuMaker.Models;

public class Dinner
{
  public static Recipy ChickenEspetada = new()
  {
    Name = "Chicken Espetada",
    MealType = MealType.Dinner,
    Items =
    {
      new SimpleRoundedItem("Chicken Espetada", 0.5m, "pack")
    }
  };

  public static Recipy Salmon = new()
  {
    Name = "Salmon",
    MealType = MealType.Dinner,
    Items =
    {
      new SimpleRoundedItem("Salmon", 200, "grams")
    }
  };

  public static Recipy PortFillet = new()
  {
    Name = "Port Fillet",
    MealType = MealType.Dinner,
    Items =
    {
      new SimpleRoundedItem("Port Fillet", 200, "grams")
    }
  };

  public static Recipy RoastChicken = new()
  {
    Name = "Roast chicken",
    MealType = MealType.Dinner,
    Items =
    {
      new SimpleRoundedItem("Chicken", 1, "half chicken")
    }
  };

  public static Recipy Fillet = new()
  {
    Name = "Fillet",
    MealType = MealType.Dinner,
    Items =
    {
      new SimpleRoundedItem("Fillet", 250, "grams")
    }
  };

  public static Recipy Wors = new()
  {
    Name = "Wors",
    MealType = MealType.Dinner,
    Items =
    {
      new SimpleRoundedItem("Wors", 250, "grams")
    }
  };


  public static Recipy AppricotPork = new()
  {
    Name = "Appricot Pork",
    MealType = MealType.Dinner,
    Items =
    {
      new SimpleRoundedItem("Pork", 200, "grams"),
      new SimpleRoundedItem("Appricot-Jam", 0.15m, "tin"),
      new SimpleRoundedItem("Mayo", 0.10m, "bottle"),
      new SimpleRoundedItem("Onion", 0.15m, "Onion"),
      new SimpleRoundedItem("Garlic", 0.1m, "Garlic"),
      new SimpleRoundedItem("Chutney", 0.15m, "bottle")
    }
  };

  public static Recipy Tuna = new()
  {
    Name = "Tuna",
    MealType = MealType.Dinner,
    Items =
    {
      new SimpleRoundedItem("Tuna", 200, "grams")
    }
  };


  public static Recipy PorkBelly = new()
  {
    Name = "Pork Belly",
    MealType = MealType.Dinner,
    Items =
    {
      new SimpleRoundedItem("Pork Belly", 250, "grams")
    }
  };

  public static Recipy Burgers = new()
  {
    Name = "Burgers",
    MealType = MealType.Dinner,
    Items =
    {
      new SimpleRoundedItem("Burgers Patty", 250, "grams"),
      new SimpleRoundedItem("Burger Bun", 1, "Bun"),
      BasicItems.Cheese(),
      BasicItems.SaladLeaves(),
      BasicItems.Tomato(0.3m)
    }
  };

  public static Recipy ApricotChicken = new()
  {
    Name = "Apricot chicken",
    MealType = MealType.Dinner,
    Items =
    {
      new SimpleRoundedItem("Chicken With Skin", 200, "grams"),
      new SimpleRoundedItem("Mayo", 0.10m, "bottle"),
      new SimpleRoundedItem("Apricot Yam", 124, "ml"),
      new SimpleRoundedItem("Garlic", 1, "table spoon"),
      new SimpleRoundedItem("Onion", 0.25m, "onion"),
      new SimpleRoundedItem("Peri Peri", 0.10m, "bottle"),
      new SimpleRoundedItem("Worcestor Sauce", 0.10m, "bottle")
    }
  };

  public static Recipy ChickenBurger = new()
  {
    Name = "Chicken Burgers",
    MealType = MealType.Dinner,
    Items =
    {
      new SimpleRoundedItem("Chicken breasts", 250, "grams"),
      BasicItems.Flour(0.1m),
      new SimpleRoundedItem("Lightly salted chips", 100, "grams"),
      BasicItems.Eggs(),

      new SimpleRoundedItem("Burger bread", 1, "Bun"),
      new SimpleRoundedItem("Tin of pineapple", 0.2m, "can"),
      new SimpleRoundedItem("Mayo", 0.10m, "bottle"),
      new SimpleRoundedItem("PeriPeri", 50, "ml"),
      BasicItems.Cheese(),
      BasicItems.SaladLeaves(),
      BasicItems.Tomato(0.3m)
    }
  };


  public static Recipy Steak = new()
  {
    Name = "Steak + Mushroom sauce",
    MealType = MealType.Dinner,
    Items =
    {
      new SimpleRoundedItem("Beef Steak", 250, "grams"),
      new SimpleRoundedItem("Mushroom sauce", 100, "ml")
    }
  };

  public static Recipy Bread = new()
  {
    Name = "Bread",
    MealType = MealType.Dinner,
    Items =
    {
      BasicItems.Flour(),
      BasicItems.Yeast(),
      BasicItems.Salt(),
      BasicItems.Sugar()
    }
  };


  public static Recipy Pizza = new()
  {
    Name = "Pizza",
    MealType = MealType.Dinner,
    Items =
    {
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

  public static Recipy Catered(string name)
  {
    return new Recipy
    {
      Name = "C-" + name,
      MealType = MealType.Dinner
    };
  }
}
