using BreakingNomad.Ui.Helpers;

namespace BreakingNomad.Ui.Components.MenuMaker.Models;

public class Dessert
{
  public static MealRecipe HotChocolate = new(
  MealType.Dessert,
     "Ice cream + Hot Chocolate",
    
    new []
    {
      BasicIngredients.From("Hot Chocolate Scoop", 1, "Unit"),
      BasicIngredients.From("Ice cream", 0.5m, "Pack")
    }
 );
  
  public static MealRecipe BananaChocolate = new(
  MealType.Dessert,
     "Banana Chocolate",
    
    new []
    {
      BasicIngredients.From("Banana", 1, "Unit"),
      BasicIngredients.From("Chocolate Block", 4, "Unit")
    }
  );
  
  public static MealRecipe CamembertBread = new(
  MealType.Dessert,
     "Camembert with Bread",
    
    new []
    {
      BasicIngredients.From("Camembert", 0.5m, "pack"),
      BasicIngredients.From("Bread Roll", 0.5m, "Unit")
    }
  );

  public static IEnumerable<MealRecipe> All()
  {
    return typeof(Dessert).GetStaticProperties<MealRecipe>();
  }
}
