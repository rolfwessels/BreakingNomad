using BreakingNomad.Shared;
using BreakingNomad.Ui.Components.MenuMaker.Models;

namespace BreakingNomad.Ui.Components;

public static class ColorPicker
{
  public static string ToColor(this MealType tagMealType)
  {
    return tagMealType switch
    {
      MealType.Breakfast => "#77DD77!important",
      MealType.Side => "#89cff0!important",
      MealType.Dinner => "#99c5c4!important",
      MealType.Dessert => "#9adedb!important",
      MealType.Lunch => "#bdb0d0!important",
      _ => "#cb99c9!important"
    };
  }
}
