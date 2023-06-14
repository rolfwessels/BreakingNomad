using Food;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace BreakingNomad.Api.Data;

public class MenuService : Menu.MenuBase
{
  public override Task<PlannedTripsResponse> GetPlannedTrips(PlannedTripsRequest request, ServerCallContext context)
  {
    return Task.FromResult(new PlannedTripsResponse
    {
      Trips =
      {
        new PlannedTrip
        {
          Id = "1",
          StartDate = Timestamp.FromDateTime(StartAt(DateTime.Now.AddDays(1), 6)),
          Duration = Duration.FromTimeSpan(TimeSpan.FromDays(1)),
          People = 1,
          Name = "Test Trip"
        },
        new PlannedTrip
        {
          Id = "2",
          StartDate = Timestamp.FromDateTime(StartAt(DateTime.Now.AddDays(50), 6)),
          Duration = Duration.FromTimeSpan(TimeSpan.FromDays(3)),
          People = 2,
          Name = "Sample"
        }
      }
    });
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
