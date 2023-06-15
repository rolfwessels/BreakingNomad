using Food;
using Grpc.Core;

namespace BreakingNomad.Api.Data;

public class MenuService : Menu.MenuBase
{
  private readonly JsonDataStore<PlannedTripResponse> _dataStore;


  public MenuService(JsonDataStore<PlannedTripResponse> jsonDataStore)
  {
    _dataStore = jsonDataStore;
  }

  public override async Task<PlannedTripsResponse> GetPlannedTrips(PlannedTripsRequest request,
    ServerCallContext context)
  {
    return new PlannedTripsResponse
    {
      Trips = { await _dataStore.GetAll() }
    };
  }

  public override async Task<PlannedTripResponse> AddPlannedTrip(AddPlannedTripRequest request,
    ServerCallContext context)
  {
    var plannedTripResponse = new PlannedTripResponse
    {
      Name = request.Name,
      StartDate = request.StartDate,
      Duration = request.Duration,
      People = request.People
    };
    return await _dataStore.Add(plannedTripResponse);
  }

  public override async Task<PlannedTripResponse> UpdatePlannedTrip(UpdatePlannedTripRequest request,
    ServerCallContext context)
  {
    var all = await _dataStore.GetAll();
    var found = all.FirstOrDefault(x => x.Id == request.Id) ?? throw new Exception("Not Found");
    found.Name = request.Trip.Name;
    found.StartDate = request.Trip.StartDate;
    found.Duration = request.Trip.Duration;
    found.People = request.Trip.People;
    return found;

  }

  public override async Task<PlannedTripResponse> GetPlannedTrip(PlannedTripByIdRequest request,
    ServerCallContext context)
  {
    var plannedTripResponses = await _dataStore.GetAll();
    return plannedTripResponses.FirstOrDefault(x => x.Id == request.Id) ?? throw new Exception("Not Found");
  }

  public override async Task<SuccessOrNotResponse> RemovePlannedTrips(PlannedTripByIdRequest request,
    ServerCallContext context)
  {
    return new SuccessOrNotResponse
    {
      Success = await _dataStore.Remove(request.Id)
    };
  }

  private DateTime StartAt(DateTime dateTime, int value)
  {
    return StartOfTheDay(dateTime).AddHours(value).ToUniversalTime();
  }

  private DateTime StartOfTheDay(DateTime from)
  {
    return new DateTime(from.Year, from.Month, from.Day);
  }
}
