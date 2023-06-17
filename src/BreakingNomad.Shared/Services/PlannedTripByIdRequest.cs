using ProtoBuf;

namespace BreakingNomad.Shared.Services;

[ProtoContract(SkipConstructor = true)]
public record PlannedTripByIdRequest(
  [property: ProtoMember(1)] string Id
);