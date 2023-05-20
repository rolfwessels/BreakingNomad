namespace BreakingNomad.Ui.Components.MenuMaker.Models;

public class DayMeal
{
  
  public DateTime Day;
  public Recipy? Breakfast;
  public Recipy? Lunch;
  public Recipy? Snacks;
  public Recipy? Dinner;
  public Recipy? Dessert;
  public List<string> Sample { get; set; } = new List<string>() { "Hi" };

  public static DayMeal From(int day, DateTime startDate, DateTime endDate, List<Recipy> allRecipies)
  {
    var startOfTheDay = startDate.AddDays(day - 1).Date;
    var breakFast = startOfTheDay.AddHours(8);
    var snack = startOfTheDay.AddHours(11);
    var dinner = startOfTheDay.AddHours(18);
    return new DayMeal
    {
      Day = startOfTheDay,
      Breakfast = PickMeal(breakFast, startDate, endDate, allRecipies, Meal.Breakfast),
      Snacks = PickMeal(snack, startDate, endDate, allRecipies, Meal.Snack),
      Lunch = PickMeal(snack, startDate, endDate, allRecipies, Meal.Lunch),
      Dinner = PickMeal(dinner, startDate, endDate, allRecipies, Meal.Dinner),
      Dessert = PickMeal(dinner, startDate, endDate, allRecipies, Meal.Dessert)
    };
  }

  private static Recipy PickMeal(DateTime dinner,
    DateTime startDate,
    DateTime endDate,
    List<Recipy> allRecipies,
    object dessert)
  {
    throw new NotImplementedException();
  }

  private static Recipy? PickMeal(DateTime time,
    DateTime startDate,
    DateTime endDate,
    List<Recipy> allRecipies,
    Meal breakfast)
  {
    if (time > startDate && time < endDate)
      return allRecipies
        .OrderBy(x => x.Used)
        .Where(x => x.Meal == breakfast)
        .Select(x => x.MarkUsed())
        .FirstOrDefault();
    return null;
  }
}
