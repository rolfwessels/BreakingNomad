using System.Reflection;
using BreakingNomad.Ui.Helpers;

namespace BreakingNomad.Ui.Components.MenuMaker.Models;

public class Side
{
  public static MealRecipe Potatoes = new(
    MealType.Side,
    "Baked Potatoes",
    new []
    {
      BasicIngredients.Potato(),
      BasicIngredients.Butter(),
      BasicIngredients.Foil()
    }
  );
  
  public static MealRecipe SweetPotatoes = new(
    MealType.Side,
    "Sweet Baked Potatoe",
    new []
    {
      BasicIngredients.From("Sweet Potatoes", 1m, "unit"),
      BasicIngredients.Butter(),
      BasicIngredients.Foil()
    }
  );
  
  
  public static MealRecipe Salad = new(
    MealType.Side,
    "Woolies Salad",
    new []
    {
      BasicIngredients.From("Woolies Salad", 0.5m, "pack")
    }
  );
  
  public static MealRecipe BraaiBroodtjies = new(
    MealType.Side,
    "Braai broodtjies",
    new []
    {
      BasicIngredients.Butter(),
      BasicIngredients.BreadSlice(4),
      BasicIngredients.Cheese(50),
      BasicIngredients.Ham(20)
    }
  );
  
  public static MealRecipe DuckFatPotatoes = new(
    MealType.Side,
    "Duck fat potatoes",
    new []
    {
      BasicIngredients.Potato(),
      BasicIngredients.From("Duck fat", 100, "ml")
    }
  );
  
  
  public static MealRecipe Chips = new(
    MealType.Side,
    "Potato Chips",
    new []
    {
      BasicIngredients.Potato(),
      BasicIngredients.Oil(),
      BasicIngredients.Salt()
    }
  );
  
  public static MealRecipe Rice = new(
    MealType.Side,
    "Just Rice",
    new []
    {
      BasicIngredients.From("Rice", 125, "gram"),
      BasicIngredients.Water(),
      BasicIngredients.Salt()
    }
  );
  
  public static MealRecipe DeniseSalad = new(
    MealType.Side,
    "Denise Salad",
    new []
    {
      BasicIngredients.From("Sweet basil", 0.25m, "punnet"),
      BasicIngredients.From("Spanspek", 0.25m, "Unit"),
      BasicIngredients.From("Mozzarella balls", 1, "Unit"),
      BasicIngredients.From("Balsamic glaze", 0.01m, "Bottle"),
      BasicIngredients.From("Cherry tomatoes", 0.25m, "punnet"),
      BasicIngredients.From("Prosciutto", 0.25m, "pack")
    }
  );

  public static IEnumerable<MealRecipe> All()
  {
    return typeof(Side).GetStaticProperties<MealRecipe>();
  }
}
