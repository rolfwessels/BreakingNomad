using BreakingNomad.Ui.Components.MenuMaker.Models;

namespace BreakingNomad.Ui.Components;

public static class ColorPicker
{
  public static string ToColor(this Meal tagMeal)
  {
    return tagMeal switch
    {
      Meal.Breakfast => "#77DD77!important",
      // Meal.Snack => "#89cff0!important",
      Meal.Dinner => "#99c5c4!important",
      // Meal.Dessert => "#9adedb!important",
      Meal.Lunch => "#bdb0d0!important",
      _ => "#cb99c9!important"
    };
  }
}
