using ProtoBuf;

namespace BreakingNomad.Shared;

[ProtoContract(SkipConstructor = true)]
public record PlannedTripsResponse(
  [property: ProtoMember(1)] List<PlannedTripResponse>? Trips
);
