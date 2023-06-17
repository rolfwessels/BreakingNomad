using BreakingNomad.Shared.Services;

namespace BreakingNomad.Shared;

public record MealRecipe(MealType MealType, string Name,Ingredient[] Ingredients)
{
  public string Key { get; } = $"{Name}".ToLower();
}