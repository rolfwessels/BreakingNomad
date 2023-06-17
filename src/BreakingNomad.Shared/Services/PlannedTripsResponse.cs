using ProtoBuf;

namespace BreakingNomad.Shared.Services;

[ProtoContract(SkipConstructor = true)]
public record PlannedTripsResponse(
  [property: ProtoMember(1)] List<PlannedTripResponse>? Trips
);
