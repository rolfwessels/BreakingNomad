using BreakingNomad.Ui.Components.MenuMaker.Models;

namespace BreakingNomad.Ui.Components.MenuMaker;

public interface IMenuLookup
{
  Task<TripMenu[]> GetUpComingTrip();
  List<MealRecipe> GetMeals();
}
