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
      Breakfast = PickMeal(breakFast, startDate, endDate, allRecipes, MealType.Breakfast),
      Lunch = PickMeal(snack, startDate, endDate, allRecipes, MealType.Lunch),
      Dinner = PickMeal(dinner, startDate, endDate, allRecipes, MealType.Dinner),
    };
  }

  private static Recipy? PickMeal(DateTime time,
    DateTime startDate,
    DateTime endDate,
    List<Recipy> allRecipes,
    MealType breakfast)
  {
    if (time > startDate && time < endDate)
      return allRecipes
        .OrderBy(x => x.Used)
        .Where(x => x.MealType == breakfast)
        .Select(x => x.MarkUsed())
        .FirstOrDefault();
    return null;
  }
}
