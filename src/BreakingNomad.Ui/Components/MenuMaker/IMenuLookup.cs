using BreakingNomad.Ui.Components.MenuMaker.Models;

namespace BreakingNomad.Ui.Components.MenuMaker;


public interface IMenuLookup
{
  Task<TripMenu[]> GetUpComingTrip();
}

class MenuLookup : IMenuLookup
{

  public Task<TripMenu[]> GetUpComingTrip()
  {
    var tripMenus = new[]
    {
      TripMenu.From("aa1",DateTime.Now.AddDays(1),DateTime.Now.AddDays(4)),
      TripMenu.From("aa2",DateTime.Now.AddDays(4),DateTime.Now.AddDays(8))
    };
    foreach (var tripMenu in tripMenus)
    {
      AddAll(tripMenu);
      tripMenu.Calculate();
    }
    return Task.FromResult(tripMenus);
  }

  private void AddAll(TripMenu trip)
  {
    trip.AddDrinks("Beer", 8, "Cans");
    trip.AddDrinks("Red Wine", 0.5m, "Bottle");
    trip.AddDrinks("Whiskey", 0.05m, "Bottle");
    //trip.AddDrinks("Gin", 0.05m, "Bottle");
    //trip.AddDrinks("Tonic water", 0.4m, "Bottle");
    trip.AddDrinks("Coke Can", 1, "Can");
    trip.AddDrinks("Pack of chips", 0.7m, "Pack");
    trip.AddDrinks("Milk", 0.25m, "Litre");
    trip.AddDrinks("Water", 2.5m, "Litre");
    trip.AddDrinks("Orange juice", 0.2m, "Litre");
    trip.AddDrinks("Koffee", 41, "Gram");
    //trip.AddDrinks("Tea", 1, "Bag");
    //trip.AddDrinks("Hot Chocolate", 0.3m, "Saches");


    trip.AddMealOption(BreakFast.FrenchToast);
    trip.AddMealOption(BreakFast.Rusks);
    trip.AddMealOption(BreakFast.FruitSalad);

    trip.AddMealOption(BreakFast.Rusks);
    trip.AddMealOption(BreakFast.Cereal);
    trip.AddMealOption(BreakFast.Rusks);
    trip.AddMealOption(BreakFast.Cereal);
    trip.AddMealOption(BreakFast.BaconEggs);

    //trip.AddMealOption(BreakFast.Cereal);
    //trip.AddMealOption(BreakFast.BaconEggs);
    //trip.AddMealOption(BreakFast.Rusks);

    trip.AddMealOption(Side.BraaiBroodtjies);
    trip.AddMealOption(Snack.Biltong);
    trip.AddMealOption(Side.BraaiBroodtjies);
    trip.AddMealOption(Snack.DroeWors);
    trip.AddMealOption(Snack.CheesySnacks);
    //trip.AddMealOption(Snack.Chips + Snack.Cheeses);


    trip.AddMealOption(Dinner.Salmon + Side.Salad);
    trip.AddMealOption(Dinner.ChickenBurger + Side.Chips);
    trip.AddMealOption(Dinner.Fillet + Side.Chips + Side.DeniseSalad);


    trip.AddMealOption(Dinner.ApricotChicken + Side.Rice);
    trip.AddMealOption(Dinner.Burgers + Side.Chips);
    //trip.AddMealOption(Dinner.ChickenEspetada + Side.BraaiBroodtjies + Side.Salad);
    trip.AddMealOption(Dinner.PorkBelly + Side.Potatoes + Side.Salad);
    //trip.AddMealOption(Dinner.Tuna + Side.BraaiBroodtjies + Side.Salad);
    trip.AddMealOption(Dinner.Steak + Side.Salad);
    trip.AddMealOption(Dinner.Fillet + Side.Salad);
    trip.AddMealOption(Dinner.Wors + Side.BraaiBroodtjies + Side.Salad);


    //trip.AddMealOption(Dinner.Catered("Graeme"));
    //trip.AddMealOption( (Side.Salad+ Side.Chips).As(Meal.Dinner));


    //
    //trip.AddMealOption(Dinner.Tuna + Side.Salad);
    //trip.AddMealOption(Dinner.Pizza);
    //trip.AddMealOption(Dinner.Burgers + Side.Chips);	
    // // mushrooms sause

    trip.AddMealOption(Dinner.AppricotPork + Side.Salad + Dinner.Bread);
    trip.AddMealOption(Dinner.RoastChicken + Side.Potatoes + Side.Salad);


  }
}
