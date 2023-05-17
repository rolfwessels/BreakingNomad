namespace BreakingNomad.Ui.Components.MenuMaker.Models;

public class BreakFast
{
  public static Recipy FrenchToast = new()
  {
    Name = "French toast",
    Meal = Meal.Breakfast,
    Items =
    {
      BasicItems.Eggs(2),
      BasicItems.BreadSlice(),
      BasicItems.Honey(),
      BasicItems.Cheese(),
      BasicItems.TomatoSause(),
      BasicItems.Bacon()
    }
  };

  public static Recipy BaconEggs = new()
  {
    Name = "Bacon Eggs",
    Meal = Meal.Breakfast,
    Items =
    {
      BasicItems.Eggs(2),
      BasicItems.BreadSlice(),
      BasicItems.Tomato(0.2m),
      BasicItems.Cheese(150),
      BasicItems.TomatoSause(),
      BasicItems.Bacon()
    }
  };

  public static Recipy Cereal = new()
  {
    Name = "Cereal",
    Meal = Meal.Breakfast,
    Items =
    {
      BasicItems.Milk(),
      BasicItems.Cereal()
    }
  };

  public static Recipy FruitSalad = new()
  {
    Name = "Fruit Salad",
    Meal = Meal.Breakfast,
    Items =
    {
      BasicItems.Yogurt(),
      BasicItems.FruitSaladSmall(),
      BasicItems.Honey()
    }
  };

  public static Recipy Rusks = new()
  {
    Name = "Rusks",
    Meal = Meal.Breakfast,
    Items =
    {
      BasicItems.Rusks(2)
    }
  };
}
