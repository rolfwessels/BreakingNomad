namespace BreakingNomad.Ui.Components.MenuMaker.Models;

public class SimpleRoundedItem
{
  public SimpleRoundedItem(string name, decimal unitValue, string unit, decimal inUnit = 1)
  {
    Name = name;
    UnitValue = unitValue;
    Unit = unit;
    InUnit = inUnit;
    Key = $"{Name}-{Unit}".ToLower();
  }

  public string Name { get; }
  public decimal UnitValue { get; }
  public string Unit { get; }
  public decimal InUnit { get; }

  public decimal CalculatePerDay(int days)
  {
    return Math.Ceiling(UnitValue * days / InUnit);
  }

  public decimal OfUnit()
  {
    return Math.Ceiling(UnitValue / InUnit);
  }

  public SimpleRoundedItem Times(int amount)
  {
    return new SimpleRoundedItem(Name, UnitValue * amount, Unit, InUnit);
  }

  public string Key { get; }

}

public record Ingredient(FoodCategory Category, string Name, ValueWithUnitOfMeasure Value)
{ 
}

public record MealRecipe(MealType MealType, string Name,Ingredient[] Ingredients)
{
  public string Key { get; } = $"{Name}".ToLower();
}

public record IngredientPerDay(decimal Amount, Ingredient Ingredient)
{
  public ValueWithUnitOfMeasure CalculatePerDay(int days, int people)
  {
    var perDay= Ingredient.Value + Amount;
    return perDay * days * people;
  }
}
