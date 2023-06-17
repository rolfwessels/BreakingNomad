using BreakingNomad.Shared.Services;
using Bumbershoot.Utilities.Helpers;

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
      .Where(x => x != null)
      .SelectMany(mealRecipe=>mealRecipe!.Ingredients.Select(r=>r.For(trip.People)))
     
      .Select(x=> new Item(x.Category,x.Name,x.Value))
      ;
      
    Items.AddRange(recipes);
    
    
  }
  public List<Item> Items { get; }
  public record Item(FoodCategory Category, string Name, ValueWithUnitOfMeasure UnitValue);
}
