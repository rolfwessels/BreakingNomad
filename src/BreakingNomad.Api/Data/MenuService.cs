using Food;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace BreakingNomad.Api.Data;

public class MenuService : Menu.MenuBase
{
  private readonly JsonDataStore<PlannedTrip> _dataStore;
  

  public MenuService(JsonDataStore<PlannedTrip> jsonDataStore)
  {
    _dataStore = jsonDataStore;
  }

  public override async Task<PlannedTripsResponse> GetPlannedTrips(PlannedTripsRequest request, ServerCallContext context)
  {
    return new PlannedTripsResponse()
    {
      Trips = { await _dataStore.GetAll() }
    };  
  }

  public override async Task<PlannedTrip> AddPlannedTrip(PlannedTrip request, ServerCallContext context)
  {
    return await _dataStore.Add(request);
  }

  #region Overrides of MenuBase

  public override async Task<SuccessOrNotResponse> RemovePlannedTrips(RemovePlannedTripsRequest request, ServerCallContext context)
  {
    return new SuccessOrNotResponse()
    {
      Success =  await _dataStore.Remove(request.Id)
    };
  } 

  #endregion

  private DateTime StartAt(DateTime dateTime, int value)
  {
    return StartOfTheDay(dateTime).AddHours(value).ToUniversalTime();
  }

  private DateTime StartOfTheDay(DateTime from)
  {
    return new DateTime(from.Year, from.Month, from.Day);
  }
}
