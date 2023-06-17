using BreakingNomad.Shared.Services;

namespace BreakingNomad.Ui.Components.MenuMaker.Models;

public record Ingredient(FoodCategory Category, string Name, ValueWithUnitOfMeasure Value)
{
  public Ingredient For(int tripPeople)
  {
     return this with { Value = Value * tripPeople };
  }
}
