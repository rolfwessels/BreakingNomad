using ProtoBuf;

namespace BreakingNomad.Shared;

[ProtoContract(SkipConstructor = true)]
public record PlannedTripResponse(
    [property: ProtoMember(1)] string Id,
    [property: ProtoMember(2)] string Name,
    [property: ProtoMember(3)] int People,
    [property: ProtoMember(4)] DateTime StartDate,
    [property: ProtoMember(5)] int Days,
    [property: ProtoMember(6)] List<MealsOfTheDay>? MealsOfTheDay
  )
  : IWithId
{
  public static PlannedTripResponse ForAdd()
  {
    return new PlannedTripResponse(Guid.NewGuid().ToString("n"), "", 1, DateTime.Now.AddDays(1), 1,
      new List<MealsOfTheDay>());
  }
}
