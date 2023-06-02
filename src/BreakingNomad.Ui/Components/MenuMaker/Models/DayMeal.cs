namespace BreakingNomad.Ui.Components.MenuMaker.Models;

public class DayMeal
{
  public DateTime Day;
  public Recipy? Breakfast;
  public Recipy? Lunch;
  public Recipy? Dinner;

  public static DayMeal From(int day, DateTime startDate, DateTime endDate, List<Recipy> allRecipes)
  {
    var startOfTheDay = startDate.AddDays(day - 1).Date;
    var breakFast = startOfTheDay.AddHours(8);
    var snack = startOfTheDay.AddHours(11);
    var dinner = startOfTheDay.AddHours(18);
    return new DayMeal
    {
      Day = startOfTheDay,
      Breakfast = PickMeal(breakFast, startDate, endDate, allRecipes, Meal.Breakfast),
      Lunch = PickMeal(snack, startDate, endDate, allRecipes, Meal.Lunch),
      Dinner = PickMeal(dinner, startDate, endDate, allRecipes, Meal.Dinner),
    };
  }

  private static Recipy? PickMeal(DateTime time,
    DateTime startDate,
    DateTime endDate,
    List<Recipy> allRecipes,
    Meal breakfast)
  {
    if (time > startDate && time < endDate)
      return allRecipes
        .OrderBy(x => x.Used)
        .Where(x => x.Meal == breakfast)
        .Select(x => x.MarkUsed())
        .FirstOrDefault();
    return null;
  }
}
