namespace BreakingNomad.Ui.Components.MenuMaker.Models;

public class TripMenu
{
  public string Id { get; set; } = null!;
  public List<IngredientPerDay> GetAllIngredientsPerDay {get;set; } = new();
  public List<DayMeal> DayMeals = new();
  
  public DateTime EndDate = DateTime.Now.AddDays(3);

  public int People { get; set; } = 1;

  public List<ShoppingListItem> ShoppingListItem = new();
  public DateTime StartDate = DateTime.Now.AddDays(1);
  private string? _name;

  

  public string Name
  {
    get => _name??"";
    set => _name = value;
  }

  public string Description()
  {
    return $"{Days} days away with {People} people";
  }

  public int Days => (int)Math.Ceiling((EndDate - StartDate).TotalDays);

 



  public static TripMenu From(string id,string name, DateTime startDate, DateTime endDate, int people = 1)
  {
    var tripMenu = new TripMenu()
    {
      Id = id,
      Name = name,
      StartDate = startDate,
      EndDate = endDate,
      People = people,
    };
    tripMenu.DayMeals.AddRange(Enumerable.Range(1, tripMenu.Days + 1)
      .Select(day => DayMeal.From(day,  tripMenu.StartDate,  tripMenu.EndDate, new List<MealRecipe>())));
    return tripMenu;
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
      new ShoppingListItem(d.Ingredient.Category, d.Ingredient.Name, d.CalculatePerDay(Days,People))));
  }


  public void AddIngredientsPerDay(List<IngredientPerDay> getAllIngredientsPerDay)
  {
    GetAllIngredientsPerDay.AddRange(getAllIngredientsPerDay);
  }
}
