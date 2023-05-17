namespace BreakingNomad.Ui.Components.MenuMaker.Models;

public class Dessert
{
  public static Recipy HotChocolate = new()
  {
    Name = "Hot Chocolate",
    Meal = Meal.Dessert,
    Items =
    {
      new SimpleRoundedItem("Hot Chocolate", 1, "pack")
    }
  };

  public static Recipy BananaChocolate = new()
  {
    Name = "Banana Chocolate",
    Meal = Meal.Dessert,
    Items =
    {
      new SimpleRoundedItem("Banana", 1, "Banana"),
      new SimpleRoundedItem("Chocolate", 4, "blocks")
    }
  };

  public static Recipy CamembertBread = new()
  {
    Name = "Camembert + Bread",
    Meal = Meal.Dessert,
    Items =
    {
      new SimpleRoundedItem("Camembert", 0.5m, "pack"),
      new SimpleRoundedItem("Bread Roll", 0.5m, "Roll")
    }
  };
}
