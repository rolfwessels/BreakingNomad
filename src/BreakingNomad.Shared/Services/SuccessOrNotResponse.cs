using ProtoBuf;

namespace BreakingNomad.Shared.Services;

[ProtoContract(SkipConstructor = true)]
public record SuccessOrNotResponse(
  [property: ProtoMember(1)] bool Success
);