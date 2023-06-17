using BreakingNomad.Shared.Services;
using BreakingNomad.Ui.Helpers;

namespace BreakingNomad.Ui.Components.MenuMaker.Models;

public class Dinner
{
  public static MealRecipe ChickenEspetada = new MealRecipe(
     MealType.Dinner,
    "Chicken Espetada",
    
    new []
    {
      BasicIngredients.From("Chicken Espetada", 0.5m, "pack")
    }
  );

  public static MealRecipe Salmon = new MealRecipe(
     MealType.Dinner,
    "Salmon",
    
    new []
    {
      BasicIngredients.From("Salmon", 200, "gram")
    }
  );

  public static MealRecipe PortFillet = new MealRecipe(
     MealType.Dinner,
    "Port Fillet",
    
    new []
    {
      BasicIngredients.From("Port Fillet", 200, "gram")
    }
  );

  public static MealRecipe RoastChicken = new MealRecipe(
     MealType.Dinner,
    "Roast chicken",
    
    new []
    {
      BasicIngredients.From("Chicken", 0.5m, "unit")
    }
  );

  public static MealRecipe Fillet = new MealRecipe(
     MealType.Dinner,
    "Fillet",
    
    new []
    {
      BasicIngredients.From("Fillet", 250, "gram")
    }
  );

  public static MealRecipe Wors = new MealRecipe(
     MealType.Dinner,
    "Wors",
    
    new []
    {
      BasicIngredients.From("Wors", 250, "gram")
    }
  );


  public static MealRecipe AppricotPork = new MealRecipe(
     MealType.Dinner,
    "Appricot Pork",
    
    new []
    {
      BasicIngredients.From("Pork", 200, "gram"),
      BasicIngredients.From("Appricot-Jam", 0.15m, "tin"),
      BasicIngredients.From("Mayo", 0.10m, "bottle"),
      BasicIngredients.From("Onion", 0.15m, "unit"),
      BasicIngredients.From("Garlic", 0.1m, "unit"),
      BasicIngredients.From("Chutney", 0.15m, "bottle")
    }
  );

  public static MealRecipe Tuna = new MealRecipe(
     MealType.Dinner,
    "Tuna",
    
    new []
    {
      BasicIngredients.From("Tuna", 200, "gram")
    }
  );


  public static MealRecipe PorkBelly = new MealRecipe(
     MealType.Dinner,
    "Pork Belly",
    
    new []
    {
      BasicIngredients.From("Pork Belly", 250, "gram")
    }
  );

  public static MealRecipe Burgers = new MealRecipe(
     MealType.Dinner,
    "Burgers",
    
    new []
    {
      BasicIngredients.From("Burgers Patty", 250, "gram"),
      BasicIngredients.From("Burger Bun", 1, "unit"),
      BasicIngredients.Cheese(),
      BasicIngredients.SaladLeaves(),
      BasicIngredients.Tomato(0.3m)
    }
  );

  public static MealRecipe ApricotChicken = new MealRecipe(
     MealType.Dinner,
    "Apricot chicken",
    
    new []
    {
      BasicIngredients.From("Chicken With Skin", 200, "gram"),
      BasicIngredients.From("Mayo", 0.10m, "bottle"),
      BasicIngredients.From("Apricot Yam", 124, "ml"),
      BasicIngredients.From("Garlic", 1, "unit"),
      BasicIngredients.From("Onion", 0.25m, "unit"),
      BasicIngredients.From("Peri Peri", 0.10m, "bottle"),
      BasicIngredients.From("Worcestor Sauce", 0.10m, "bottle")
    }
  );

  public static MealRecipe ChickenBurger = new MealRecipe(
     MealType.Dinner,
    "Chicken Burgers",
    
    new []
    {
      BasicIngredients.From("Chicken breasts", 250, "gram"),
      BasicIngredients.Flour(100m),
      BasicIngredients.From("Lightly salted chips", 100, "gram"),
      BasicIngredients.Eggs(),

      BasicIngredients.From("Burger bread", 1, "unit"),
      BasicIngredients.From("Tin of pineapple", 0.2m, "tin"),
      BasicIngredients.From("Mayo", 0.10m, "bottle"),
      BasicIngredients.From("PeriPeri", 50, "ml"),
      BasicIngredients.Cheese(),
      BasicIngredients.SaladLeaves(),
      BasicIngredients.Tomato(0.3m)
    }
  );


  public static MealRecipe Steak = new MealRecipe(
     MealType.Dinner,
    "Steak + Mushroom sauce",
    
    new []
    {
      BasicIngredients.From("Beef Steak", 250, "gram"),
      BasicIngredients.From("Mushroom sauce", 100, "ml")
    }
  );

  public static MealRecipe Bread = new MealRecipe(
     MealType.Dinner,
    "Bread",
    
    new []
    {
      BasicIngredients.Flour(),
      BasicIngredients.Yeast(),
      BasicIngredients.Salt(),
      BasicIngredients.Sugar()
    }
  );


  public static MealRecipe Pizza = new MealRecipe(
     MealType.Dinner,
    "Pizza",
    
    new []
    {
      BasicIngredients.From("Tomatoe paste", 1, "Sachets"),
      BasicIngredients.From("Tin of pineapple", 0.5m, "tin"),
      BasicIngredients.Cheese(100),
      BasicIngredients.Ham(100),
      BasicIngredients.Flour(),
      BasicIngredients.Yeast(),
      BasicIngredients.Salt(),
      BasicIngredients.Sugar()
    }
  );


  public static IEnumerable<MealRecipe> All()
  {
    return typeof(Dinner).GetStaticProperties<MealRecipe>();
  }
}
