using ProtoBuf;

namespace BreakingNomad.Shared;

[ProtoContract(SkipConstructor = true)]
public record PlannedTripByIdRequest(
  [property: ProtoMember(1)] string Id
);