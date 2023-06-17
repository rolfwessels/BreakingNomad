using BreakingNomad.Shared;
using BreakingNomad.Shared.Data;
using BreakingNomad.Shared.Helpers;
using BreakingNomad.Shared.Services;
using Bumbershoot.Utilities.Helpers;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using ProtoBuf.Grpc.Client;

namespace BreakingNomad.Ui.Components.MenuMaker;

internal class MenuLookup : IMenuLookup
{


  public MenuLookup()
  {

  }

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
    meals.AddRange(Side.All());
    meals.AddRange(Dinner.All());
    meals.AddRange(Dessert.All());
    return meals;
  }


  private IEnumerable<IngredientPerDay> IngredientsPerDayPerPerson()
  {
    yield return new IngredientPerDay(2, new Ingredient(FoodCategory.Alcohol, "Beer", Unit.CanInSixPack));
    yield return new IngredientPerDay(0.5m, new Ingredient(FoodCategory.Alcohol, "Red Wine", Unit.Bottle750));
    yield return new IngredientPerDay(0.05m, new Ingredient(FoodCategory.Alcohol, "Whiskey", Unit.Bottle750));
    yield return new IngredientPerDay(0.05m, new Ingredient(FoodCategory.Alcohol, "Gin", Unit.Bottle750));
    yield return new IngredientPerDay(0.2m, new Ingredient(FoodCategory.Drink, "Tonic", Unit.Bottle750));

    yield return new IngredientPerDay(1, new Ingredient(FoodCategory.Drink, "Coke Can", Unit.CanInSixPack));
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
    yield return new IngredientPerDay(0.5m, new Ingredient(FoodCategory.Snack, "Ice cream", Unit.AUnit));
    yield return new IngredientPerDay(0.5m, new Ingredient(FoodCategory.Snack, "CheesySnacks", Unit.Pack));
  }

  public async Task<TripMenu[]> GetUpComingTrip()
  {
    var result = await Call(m=> m.GetPlannedTrips(new PlannedTripsRequest()));
    var tripMenus = result.Trips.OrEmpty().Select(ToMenu).ToArray();
    return tripMenus;
  }

  private TripMenu ToMenu(PlannedTripResponse tripData)
  {
    var tripMenu = TripMenu.From(tripData.Id, tripData.Name, tripData.StartDate,
      tripData.StartDate.AddDays(tripData.Days),
      tripData.People);
    tripMenu.MealsOfTheDay = tripData.MealsOfTheDay.OrEmpty().ToList();
 
    return tripMenu;
  }

  public async Task<TripMenu> GetUpComingTrip(string id)
  {
    var plannedTrip = await Call(m=> m.GetPlannedTrip(new PlannedTripByIdRequest(id)));
    return ToMenu(plannedTrip);
  }

  private async Task<T> Call<T>(Func<IMenuService, Task<T>> func)
  {
    var httpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));
    var channel = GrpcChannel.ForAddress("http://localhost:5200", new GrpcChannelOptions { HttpClient = httpClient });

    var menuService = channel.CreateGrpcService<IMenuService>();
    return await func(menuService);

  }

  public async Task Add(TripMenu trip)
  {
    var addPlannedTripAsync = await Call(m=> m.AddPlannedTrip(ToAdd(trip)));
    trip.Id = addPlannedTripAsync.Id;
  }

  private AddPlannedTripRequest ToAdd(TripMenu trip)
  {
    var add = new AddPlannedTripRequest
    (
      trip.Name,
      trip.People,
      trip.StartDate,
      trip.Days,
      trip.MealsOfTheDay.Where(x => x.Options.Count > 0).ToList()
    );
    return add;
  }


  public async Task Update(TripMenu trip)
  {
    var tripRequest = new UpdatePlannedTripRequest(trip.Id, ToAdd(trip));
    var addPlannedTripAsync = await Call(m=> m.UpdatePlannedTrip(tripRequest));
    trip.Id = addPlannedTripAsync.Id;
  }
}
