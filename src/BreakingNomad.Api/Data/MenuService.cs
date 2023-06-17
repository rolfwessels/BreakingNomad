using BreakingNomad.Shared;
using ProtoBuf.Grpc;

namespace BreakingNomad.Api.Data;

public class MenuService : IMenuService
{
  private readonly JsonDataStore<PlannedTripResponse> _dataStore;


  public MenuService(JsonDataStore<PlannedTripResponse> jsonDataStore)
  {
    _dataStore = jsonDataStore;
  }


  public  async Task<PlannedTripsResponse> GetPlannedTrips(PlannedTripsRequest request,
    CallContext context)
  {
    return new PlannedTripsResponse( await _dataStore.GetAll());
    
  }
  
  public  async Task<PlannedTripResponse> AddPlannedTrip(AddPlannedTripRequest request,
    CallContext context)
  {
    var plannedTripResponse = PlannedTripResponse.ForAdd();
    return await _dataStore.Add(Apply(plannedTripResponse, request));
  }
  
  public  async Task<PlannedTripResponse> UpdatePlannedTrip(UpdatePlannedTripRequest request,
    CallContext context)
  {
    var all = await _dataStore.GetAll();
    var found = all.FirstOrDefault(x => x.Id == request.Id) ?? throw new Exception("Not Found");
    var plannedTripResponse = await _dataStore.Update(request.Id, Apply(found, request.Trip));
    return plannedTripResponse;
  }
  
  private static PlannedTripResponse Apply(PlannedTripResponse found, AddPlannedTripRequest addPlannedTripRequest)
  {
    return found with {
      Name = addPlannedTripRequest.Name,
      StartDate = addPlannedTripRequest.StartDate.Date,
      Days = addPlannedTripRequest.Days,
      People = addPlannedTripRequest.People,
      MealsOfTheDay = addPlannedTripRequest.MealsOfTheDay
    };
  }
  
  public  async Task<PlannedTripResponse> GetPlannedTrip(PlannedTripByIdRequest request,
    CallContext context)
  {
    var plannedTripResponses = await _dataStore.GetAll();
    return plannedTripResponses.FirstOrDefault(x => x.Id == request.Id) ?? throw new Exception("Not Found");
  }
  
  public  async Task<SuccessOrNotResponse> RemovePlannedTrips(PlannedTripByIdRequest request,
    CallContext context)
  {
    return new SuccessOrNotResponse(await _dataStore.Remove(request.Id));
  }


}
