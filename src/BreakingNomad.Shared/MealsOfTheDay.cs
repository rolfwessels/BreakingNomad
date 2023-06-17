using ProtoBuf;

namespace BreakingNomad.Shared;

[ProtoContract(SkipConstructor = true)]
public record MealsOfTheDay(
  [property: ProtoMember(1)] int Day,
  [property: ProtoMember(2)] MealType Meal,
  [property: ProtoMember(3)] List<string>? Options
);
