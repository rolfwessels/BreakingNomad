using BreakingNomad.Ui.Helpers;

namespace BreakingNomad.Ui.Components.MenuMaker.Models;

public class BreakFast
{
  public static MealRecipe FrenchToast = new(MealType.Breakfast,"French toast",new []{
    BasicIngredients.Eggs(2),
    BasicIngredients.BreadSlice(),
    BasicIngredients.Honey(),
    BasicIngredients.Cheese(),
    BasicIngredients.TomatoSause(),
    BasicIngredients.Bacon()
  });
  

  public static MealRecipe Omelette = new(MealType.Breakfast,"Omelette", new [] 
    {
      BasicIngredients.Eggs(2),
      BasicIngredients.Cheese(150),
      BasicIngredients.Ham()
    }
  );

  public static MealRecipe BaconEggs = new(MealType.Breakfast,"Bacon Eggs", new [] 
    {
      BasicIngredients.Eggs(2),
      BasicIngredients.BreadSlice(),
      BasicIngredients.Tomato(0.2m),
      BasicIngredients.Cheese(150),
      BasicIngredients.TomatoSause(),
      BasicIngredients.Bacon()
    }
  );

  public static MealRecipe Cereal = new(MealType.Breakfast,"Cereal", new [] 
    {
      BasicIngredients.Milk(),
      BasicIngredients.Cereal()
    }
  );

  public static MealRecipe FruitSalad = new(MealType.Breakfast,"Fruit Salad", new [] 
    {
      BasicIngredients.Yogurt(),
      BasicIngredients.FruitSaladSmall(),
      BasicIngredients.Honey()
    }
  );

  public static MealRecipe Rusks = new(MealType.Breakfast,"Rusks", new [] 
    {
      BasicIngredients.Rusks(2)
    }
  );

  public static IEnumerable<MealRecipe> All()
  {
    return typeof(BreakFast).GetStaticProperties<MealRecipe>();
  }
}
