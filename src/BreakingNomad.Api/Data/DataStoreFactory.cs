

using BreakingNomad.Shared;

namespace BreakingNomad.Api.Data;

public class DataStoreFactory
{
  private readonly string _folder;

  public DataStoreFactory(string folder)
  {
    _folder = folder;
  }
  
  public JsonDataStore<PlannedTripResponse> PlannedTrips => new(_folder, (id,trip) => trip.Id = id, trip => trip.Id);
}

