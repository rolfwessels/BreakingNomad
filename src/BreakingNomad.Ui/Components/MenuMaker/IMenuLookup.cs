using BreakingNomad.Ui.Components.MenuMaker.Models;

namespace BreakingNomad.Ui.Components.MenuMaker;

public interface IMenuLookup
{
  Task<TripMenu[]> GetUpComingTrip();
}

internal class MenuLookup : IMenuLookup
{
  public List<IngredientPerDay> GetAllIngredientsPerDay()
  {
    var allIngredients = new List<IngredientPerDay>();
    allIngredients.AddRange(IngredientsPerDayPerPerson());

    return allIngredients;
  }

  public List<MealRecipe> GetMeals()
  {
    var meals = new List<MealRecipe>();
    meals.AddRange(BreakFast.All());
    return meals;
  }

  private IEnumerable<IngredientPerDay> IngredientsPerDayPerPerson()
  {
    yield return new IngredientPerDay(2, new Ingredient(FoodCategory.Alcohol, "Beer", Unit.SixPack));
    yield return new IngredientPerDay(0.5m, new Ingredient(FoodCategory.Alcohol, "Red Wine", Unit.Bottle750));
    yield return new IngredientPerDay(0.05m, new Ingredient(FoodCategory.Alcohol, "Whiskey", Unit.Bottle750));
    yield return new IngredientPerDay(0.05m, new Ingredient(FoodCategory.Alcohol, "Gin", Unit.Bottle750));
    yield return new IngredientPerDay(0.2m, new Ingredient(FoodCategory.Drink, "Tonic", Unit.Bottle750));
    
    yield return new IngredientPerDay(1, new Ingredient(FoodCategory.Drink, "Coke Can", Unit.SixPack));
    yield return new IngredientPerDay(0.25m, new Ingredient(FoodCategory.Drink, "Milk", Unit.Litre));
    yield return new IngredientPerDay(2m, new Ingredient(FoodCategory.Drink, "Water", Unit.Litre));
    yield return new IngredientPerDay(0.2m, new Ingredient(FoodCategory.Drink, "Orange juice", Unit.Litre));
    yield return new IngredientPerDay(41, new Ingredient(FoodCategory.Drink, "Koffee", Unit.Gram));
    yield return new IngredientPerDay(0.5m, new Ingredient(FoodCategory.Drink, "Tea", Unit.TeaBag));
    yield return new IngredientPerDay(0.3m, new Ingredient(FoodCategory.Drink, "Hot Chocolate", Unit.Sachets));
    

    yield return new IngredientPerDay(0.4m, new Ingredient(FoodCategory.Snack, "Pack of chips", Unit.Pack));
    yield return new IngredientPerDay(25m, new Ingredient(FoodCategory.Snack, "Biltong", Unit.Rand));
    yield return new IngredientPerDay(25m, new Ingredient(FoodCategory.Snack, "Droë wors", Unit.Rand));
    yield return new IngredientPerDay(40m, new Ingredient(FoodCategory.Snack, "Nuts", Unit.Gram));
    yield return new IngredientPerDay(0.2m, new Ingredient(FoodCategory.Snack, "Pack of Sweets", Unit.Pack));
  }

  public Task<TripMenu[]> GetUpComingTrip()
  {
    var tripMenus = new[]
    {
      TripMenu.From("aa1", DateTime.Now.AddDays(1), DateTime.Now.AddDays(2),2),
      TripMenu.From("aa2", DateTime.Now.AddDays(4), DateTime.Now.AddDays(8))
    };
    foreach (var tripMenu in tripMenus)
    {
      AddAll(tripMenu);
      tripMenu.AddIngredientsPerDay(GetAllIngredientsPerDay());
      tripMenu.Calculate();
    }

    return Task.FromResult(tripMenus);
  }

  private void AddAll(TripMenu trip)
  {



    /*



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
    trip.AddMealOption(Dinner.RoastChicken + Side.Potatoes + Side.Salad);*/
  }
}


