using Bumbershoot.Utilities.Helpers;
using System.Linq;

namespace BreakingNomad.Ui.Components.MenuMaker.Models;

public class TripMenu
{
  public string Id { get; set; } = null!;
  public List<IngredientPerDay> GetAllIngredientsPerDay {get;set; } = new();
  public List<Recipy> AllRecipes = new();
  public List<DayMeal> DayMeals = new();
  
  public DateTime EndDate = DateTime.Now.AddDays(3);

  public int People { get; set; } = 1;

  public List<ShoppingListItem> ShoppingListItem = new();
  public DateTime StartDate = DateTime.Now.AddDays(1);
  private string? _name;

  

  public string Name
  {
    get => _name??$"{Days} days away with {People} people";
    set => _name = value;
  }

  public int Days => (int)Math.Ceiling((EndDate - StartDate).TotalDays);

 

  public void AddMealOption(Recipy recipe)
  {
    AllRecipes.Add(recipe);
  }

  public static TripMenu From(string id, DateTime startDate, DateTime endDate, int people = 1)
  {
    return new TripMenu()
    {
      Id = id,
      StartDate = startDate,
      EndDate = endDate,
      People = people,
    };
  }

  public void Calculate()
  {
    AllRecipes.ForEach(x => x.MarkUsed(false));
    DayMeals.Clear();
    DayMeals.AddRange(Enumerable.Range(1, Days + 1)
      .Select(day => DayMeal.From(day, StartDate, EndDate, AllRecipes)));

    ShoppingListItem.Clear();
    // ShoppingListItem.AddRange(AllRecipes
    //   .SelectMany(x => x.Items.Select(r => new { meal = x.Meal, dr = r.Times(x.Used * People) }))
    //   .GroupBy(g => new { name = g.dr.Name, unit = g.dr.Unit, inUnit = g.dr.InUnit })
    //   .Select(x => new ShoppingListItem(x.First().meal.ToString(), x.Key.name,
    //     Math.Ceiling(x.Sum(r => r.dr.UnitValue) / x.Key.inUnit), x.Key.unit))
    //   .Where(x => x.UnitValue > 0m)
    //   .OrderBy(x => x.Category)
    //   .ThenBy(x => x.Name));

    ShoppingListItem.AddRange(GetAllIngredientsPerDay.Select(d =>
      new ShoppingListItem(d.Ingredient.Category, d.Ingredient.Name, d.CalculatePerDay(Days,People))));
  }


  public void AddIngredientsPerDay(List<IngredientPerDay> getAllIngredientsPerDay)
  {
    GetAllIngredientsPerDay.AddRange(getAllIngredientsPerDay);
  }
}
