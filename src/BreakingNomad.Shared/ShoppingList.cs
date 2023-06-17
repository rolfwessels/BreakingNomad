using BreakingNomad.Shared.Services;

namespace BreakingNomad.Ui.Components.MenuMaker.Models;

public class ShoppingList
{
  public ShoppingList(List<MealRecipe> allowedValues, TripMenu trip, List<IngredientPerDay> ingredientPerDays)
  {
    Items = new List<Item>();
    Items.AddRange(ingredientPerDays.Select(d =>
      new Item(d.Ingredient.Category, d.Ingredient.Name, d.CalculatePerDay(trip.Days, trip.People))));
    var mealRecipes = allowedValues.ToDictionary(x => x.Key)!;
    var recipes = trip.MealsOfTheDay.SelectMany(x => x.Options)
      .Select(x => mealRecipes!.GetValueOrDefault(x, null))
      .Where(x => x != null);

    //Items.AddRange()
  }


  public List<Item> Items { get; }

  public class Item
  {
    public FoodCategory Category;
    public string Name;
    public string Unit;
    public ValueWithUnitOfMeasure UnitValue;

    public Item(FoodCategory category, string name, ValueWithUnitOfMeasure unitValue)
    {
      Category = category;
      Name = name;
      UnitValue = unitValue;
    }
  }
}
