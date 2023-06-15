using BreakingNomad.Ui.Components.MenuMaker.Models;
using Food;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;

namespace BreakingNomad.Ui.Components.MenuMaker;

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
    meals.AddRange(Side.All());
    meals.AddRange(Dinner.All());
    meals.AddRange(Dessert.All());
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
    yield return new IngredientPerDay(0.5m, new Ingredient(FoodCategory.Snack, "CheesySnacks", Unit.Pack));
  }

  public async Task<TripMenu[]> GetUpComingTrip()
  {
    using var channel = GrpcChannel.ForAddress("http://localhost:5200/", new GrpcChannelOptions
    {
      HttpHandler = new GrpcWebHandler(new HttpClientHandler())
    });
    var client = new Menu.MenuClient(channel);
    var result  = await client.GetPlannedTripsAsync(new PlannedTripsRequest());

    var tripMenus = result.Trips.Select(ToMenu).ToArray();
    
    foreach (var tripMenu in tripMenus)
    {
      tripMenu.AddIngredientsPerDay(GetAllIngredientsPerDay());
      tripMenu.Calculate();
    }

    return tripMenus;
  }

  private TripMenu ToMenu(PlannedTripResponse x)
  {
    var startDate = x.StartDate.ToDateTime();
    return TripMenu.From(x.Id, x.Name, startDate, startDate.Add(x.Duration.ToTimeSpan()), x.People);
  }

  public async Task<TripMenu> GetUpComingTrip(string id)
  {
    using var channel = GrpcChannel.ForAddress("http://localhost:5200/", new GrpcChannelOptions
    {
      HttpHandler = new GrpcWebHandler(new HttpClientHandler())
    });
    var client = new Menu.MenuClient(channel);
    var plannedTrip = await client.GetPlannedTripAsync(new PlannedTripByIdRequest
    {
      Id = id
    });
    return ToMenu(plannedTrip);
  }

  public async Task Add(TripMenu trip)
  {
    using var channel = GrpcChannel.ForAddress("http://localhost:5200/", new GrpcChannelOptions
    {
      HttpHandler = new GrpcWebHandler(new HttpClientHandler())
    });
    var client = new Menu.MenuClient(channel);
    var addPlannedTripAsync = await client.AddPlannedTripAsync(ToAdd(trip));
    trip.Id = addPlannedTripAsync.Id;

  }

  private static AddPlannedTripRequest ToAdd(TripMenu trip)
  {
    return new AddPlannedTripRequest
    {
      Name = trip.Name,
      StartDate = Timestamp.FromDateTime(trip.StartDate.ToUniversalTime()),
      Duration = Duration.FromTimeSpan(TimeSpan.FromDays(trip.Days)),
      People = trip.People
    };
  }

  public async Task Update(TripMenu trip)
  {
    using var channel = GrpcChannel.ForAddress("http://localhost:5200/", new GrpcChannelOptions
    {
      HttpHandler = new GrpcWebHandler(new HttpClientHandler())
    });
    var client = new Menu.MenuClient(channel);
    var addPlannedTripAsync = await client.UpdatePlannedTripAsync(new UpdatePlannedTripRequest () { Id = trip.Id,
      Trip = ToAdd(trip)
      } 
    );
    trip.Id = addPlannedTripAsync.Id;

  }
  
}
