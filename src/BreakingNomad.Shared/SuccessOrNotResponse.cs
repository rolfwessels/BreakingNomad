using ProtoBuf;

namespace BreakingNomad.Shared;

[ProtoContract(SkipConstructor = true)]
public record SuccessOrNotResponse(
  [property: ProtoMember(1)] bool Success
);