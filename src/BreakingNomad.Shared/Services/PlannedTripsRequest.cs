using ProtoBuf;

namespace BreakingNomad.Shared.Services;

[ProtoContract(SkipConstructor = true)]
public record PlannedTripsRequest;