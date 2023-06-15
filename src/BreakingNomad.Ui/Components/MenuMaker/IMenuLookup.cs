using BreakingNomad.Ui.Components.MenuMaker.Models;

namespace BreakingNomad.Ui.Components.MenuMaker;

public interface IMenuLookup
{
  Task<TripMenu[]> GetUpComingTrip();
  Task<TripMenu> GetUpComingTrip(string id);
  List<MealRecipe> GetMeals();
  Task Add(TripMenu trip);
  Task Update(TripMenu trip);
}
