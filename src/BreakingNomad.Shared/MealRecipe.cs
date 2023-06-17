using BreakingNomad.Shared.Services;

namespace BreakingNomad.Ui.Components.MenuMaker.Models;

public record MealRecipe(MealType MealType, string Name,Ingredient[] Ingredients)
{
  public string Key { get; } = $"{Name}".ToLower();
}