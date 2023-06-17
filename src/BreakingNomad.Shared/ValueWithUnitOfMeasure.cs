
using BreakingNomad.Ui.Helpers;

namespace BreakingNomad.Ui.Components.MenuMaker.Models;

public static class Unit
{
  public static ValueWithUnitOfMeasure Carton = new ValueWithUnitOfMeasure(0, "Carton")
    .RoundUpTo(6, 1, "Half Carton")
    .RoundUpTo(12, 1, "Carton");

  public static ValueWithUnitOfMeasure Loaf = new ValueWithUnitOfMeasure(0, "Slice")
    .RoundUpTo(12, 1, "Half Loaf").RoundUpTo(24, 1, "Loaf");

  public static ValueWithUnitOfMeasure SixPack = new ValueWithUnitOfMeasure(0, "SixPack")
    .RoundUpTo(1, 1, "Can")
    .RoundUpTo(2, 2, "Cans")
    .RoundUpTo(3, 3, "Cans")
    .RoundUpTo(3, 3, "Cans")
    .RoundUpTo(4, 4, "Cans")
    .RoundUpTo(5, 5, "Cans")
    .RoundUpTo(6, 1, "Six Pack")
    .RoundUpTo(12.1m, 2, "Six Packs")
    .RoundUpTo(18.1m, 3, "Six Packs")
    .RoundUpTo(24, 1, "Slab");

  public static ValueWithUnitOfMeasure Bottle750 = new ValueWithUnitOfMeasure(0, "Bottle")
    .RoundUpTo(0.5m, 1, "Half jack").RoundUpTo(1m, 1, "Bottle");

  public static ValueWithUnitOfMeasure Litre = new ValueWithUnitOfMeasure(0, "Litre")
    .RoundUpTo(0.5m, 1, "Half Litre").RoundUpTo(1, 1, "Litre");

  public static ValueWithUnitOfMeasure Pack = new(0, "Pack");

  public static ValueWithUnitOfMeasure Gram = new ValueWithUnitOfMeasure(0, "Gram")
    .RoundUpTo(1, 1, "Gram")
    .RoundUpTo(250, 250, "Grams")
    .RoundUpTo(500, 500, "Grams")
    .RoundUpTo(750, 750, "Grams")
    .RoundUpTo(1000, 1, "kg");

  public static ValueWithUnitOfMeasure Rand = new ValueWithUnitOfMeasure(0, "Rand");
  public static ValueWithUnitOfMeasure TeaBag = new ValueWithUnitOfMeasure(0, "Tea bag");
  public static ValueWithUnitOfMeasure Sachets = new ValueWithUnitOfMeasure(0, "Sachets");
  public static ValueWithUnitOfMeasure AUnit = new ValueWithUnitOfMeasure(0, "Unit");
  public static ValueWithUnitOfMeasure Ml = new ValueWithUnitOfMeasure(0, "Ml");
  public static ValueWithUnitOfMeasure Punnet = new ValueWithUnitOfMeasure(0, "Punnet");
  public static ValueWithUnitOfMeasure Tin = new ValueWithUnitOfMeasure(0, "tin");
  
  private static ValueWithUnitOfMeasure[]? _all;

  public static ValueWithUnitOfMeasure ByName(string name)
  {
    var propertyInfos = All();

    return propertyInfos.FirstOrDefault(measure=>measure.Name.ToLower() == name.ToLower()) ?? throw new Exception($"Could not match {name}");
  }

  public static ValueWithUnitOfMeasure[] All()
  {
    return _all ??= typeof(Unit).GetStaticProperties<ValueWithUnitOfMeasure>()
      .ToArray();
  }
}

public record ValueWithUnitOfMeasure(decimal Value, string Name)
{
  public RoundUpToValue[] RoundsUp { get; set; } = Array.Empty<RoundUpToValue>();

  public static ValueWithUnitOfMeasure operator +(ValueWithUnitOfMeasure left, ValueWithUnitOfMeasure right)
  {
    dynamic leftValue = left.Value;
    dynamic rightValue = right.Value;
    var sumValue = leftValue + rightValue;
    return left with { Value = sumValue };
  }

  public static ValueWithUnitOfMeasure operator +(ValueWithUnitOfMeasure left, decimal rightValue)
  {
    dynamic leftValue = left.Value;
    var sumValue = leftValue + rightValue;
    return left with { Value = sumValue };
  }

  public static ValueWithUnitOfMeasure operator *(ValueWithUnitOfMeasure left, decimal rightValue)
  {
    dynamic leftValue = left.Value;
    var sumValue = leftValue * rightValue;
    return left with { Value = sumValue };
  }

  public ValueWithUnitOfMeasure RoundUpTo(decimal rightValue, decimal displayAmount, string name)
  {
    return this with
    {
      RoundsUp = RoundsUp.Concat(new[] { new RoundUpToValue(rightValue, displayAmount, name) }).ToArray()
    };
  }

  public record RoundUpToValue(decimal Amount, decimal DisplayAmount, string Name);

  public override string ToString()
  {
    if (RoundsUp.Any())
      for (var i = 0; i < RoundsUp.Length; i++)
      {
        var roundUp = RoundsUp[i];

        if (Value == roundUp.Amount) return $"{roundUp.DisplayAmount} {roundUp.Name}";
        if (Value < roundUp.Amount) return $"{roundUp.DisplayAmount} {roundUp.Name} ({Value})";

        if (i == RoundsUp.Length - 1)
        {
          var roundUpDisplayAmount = Math.Ceiling(roundUp.DisplayAmount / roundUp.Amount * Value);
          return $"{roundUpDisplayAmount} {roundUp.Name} ({Value})";
        }
      }

    return $"{Value} {Name}";
  }
}
