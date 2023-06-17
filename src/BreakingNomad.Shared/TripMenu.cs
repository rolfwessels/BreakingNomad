using BreakingNomad.Shared.Services;

namespace BreakingNomad.Ui.Components.MenuMaker.Models;

public class TripMenu
{
  public string Id { get; set; } = null!;
  public string Name { get; set; } = "";
  public DateTime EndDate = DateTime.Now.AddDays(3);
  public DateTime StartDate = DateTime.Now.AddDays(1);
  public int People { get; set; } = 1;
  public List<MealsOfTheDay> MealsOfTheDay { get; set; } = new();
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
      var ofTheDay = new MealsOfTheDay(day,meal,new List<string>());
      MealsOfTheDay.Add(ofTheDay);
      return ofTheDay;
    }
    return mealsOfTheDay;
  }
}
