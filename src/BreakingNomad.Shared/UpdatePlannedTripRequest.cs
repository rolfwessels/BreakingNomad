using ProtoBuf;

namespace BreakingNomad.Shared;

[ProtoContract(SkipConstructor = true)]
public record UpdatePlannedTripRequest(
  [property: ProtoMember(1)] string Id,
  [property: ProtoMember(2)] AddPlannedTripRequest Trip);