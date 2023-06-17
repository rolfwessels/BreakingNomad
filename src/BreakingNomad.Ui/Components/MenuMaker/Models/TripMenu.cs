using BreakingNomad.Shared;
using Bumbershoot.Utilities.Helpers;

namespace BreakingNomad.Ui.Components.MenuMaker.Models;

public class TripMenu
{
  private string? _name;
  public DateTime EndDate = DateTime.Now.AddDays(3);

  public List<ShoppingListItem> ShoppingListItem = new();
  public DateTime StartDate = DateTime.Now.AddDays(1);
  public string Id { get; set; } = null!;
  public List<IngredientPerDay> GetAllIngredientsPerDay { get; set; } = new();
  public int People { get; set; } = 1;
  public List<MealsOfTheDay> MealsOfTheDay { get; set; } = new();

  public string Name
  {
    get => _name ?? "";
    set => _name = value;
  }

  public int Days => (int)Math.Ceiling((EndDate - StartDate).TotalDays);

  public IEnumerable<(int,DateTime)> Dates
  {
    get
    {
      for (var i = 0; i < Days+1; i++) yield return (i,StartDate.AddDays(i));
    }
  }

  public string Description()
  {
    return $"{Days} days away with {People} people";
  }


  public void Calculate()
  {
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
      new ShoppingListItem(d.Ingredient.Category, d.Ingredient.Name, d.CalculatePerDay(Days, People))));
  }

  public void AddIngredientsPerDay(List<IngredientPerDay> getAllIngredientsPerDay)
  {
    GetAllIngredientsPerDay.AddRange(getAllIngredientsPerDay);
  }

  public static TripMenu From(string id, string name, DateTime startDate, DateTime endDate, int people = 1)
  {
    var tripMenu = new TripMenu
    {
      Id = id,
      Name = name,
      StartDate = startDate,
      EndDate = endDate,
      People = people
    };
    return tripMenu;
  }

  public MealsOfTheDay GetOrAdd(int day, MealType meal)
  {
    var mealsOfTheDay = MealsOfTheDay.FirstOrDefault(x=>x.Day == day && x.Meal == meal);
    if (mealsOfTheDay == null)
    {
      var ofTheDay = new MealsOfTheDay() { Day = day, Meal = meal, Options = {  }};
      MealsOfTheDay.Add(ofTheDay);
      return ofTheDay;
    }

    Console.Out.WriteLine($"mealsOfTheDay:{mealsOfTheDay.Day} {mealsOfTheDay.Meal} {mealsOfTheDay.Options.StringJoin()}");
    return mealsOfTheDay;
  }
}
