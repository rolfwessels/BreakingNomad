using BreakingNomad.Shared.Services;

namespace BreakingNomad.Shared;

public class ShoppingList
{
  public ShoppingList(List<MealRecipe> allowedValues, TripMenu trip, List<IngredientPerDay> ingredientPerDays)
  {
    Items = new List<Item>();
    var daily = ingredientPerDays.Select(d =>
      new Item(d.Ingredient.Category, d.Ingredient.Name, d.CalculatePerDay(trip.Days, trip.People)));
    
    var mealRecipes = allowedValues.ToDictionary(x => x.Key)!;
    var meals =
      trip.MealsOfTheDay.SelectMany(x => x.Options)
      .Select(x => mealRecipes!.GetValueOrDefault(x, null))
      .Where(x => x != null)
      .SelectMany(mealRecipe=>mealRecipe!.Ingredients.Select(r=>r.For(trip.People)))
      .Select(x=> new Item(x.Category,x.Name,x.Value));
    var total = meals.Concat(daily)
      .GroupBy(x=> new {x.Category,x.Name})
      .Select(x=> new Item(x.Key.Category,x.Key.Name,ValueWithUnitOfMeasure.Sum(x.Select(r=>r.UnitValue))))
      .OrderBy(x=>x.Category);
      
    Items.AddRange(total);
    
    
  }
  public List<Item> Items { get; }
  public record Item(FoodCategory Category, string Name, ValueWithUnitOfMeasure UnitValue);
}
