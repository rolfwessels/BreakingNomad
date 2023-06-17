namespace BreakingNomad.Ui.Components.MenuMaker.Models;

public record IngredientPerDay(decimal Amount, Ingredient Ingredient)
{
  public ValueWithUnitOfMeasure CalculatePerDay(int days, int people)
  {
    var perDay= Ingredient.Value + Amount;
    return perDay * days * people;
  }
}