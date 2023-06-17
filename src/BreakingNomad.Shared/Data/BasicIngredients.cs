using BreakingNomad.Shared.Services;

namespace BreakingNomad.Ui.Components.MenuMaker.Models;

public class BasicIngredients
{
  public static Ingredient Eggs(decimal amount = 1)
  {
    return From("Eggs", amount, "Carton");
  }

  public static Ingredient From(string name, decimal amount, string defaultFor1)
  {
    return new Ingredient(FoodCategory.Produce,name,Unit.ByName(defaultFor1) + amount);
  }

  public static Ingredient Bacon(decimal amount = 1)
  {
    return From("Bacon", amount, "pack");
  }

  public static Ingredient BreadSlice(decimal amount = 2)
  {
    return From("Bread", amount, "slice");
  }

  public static Ingredient Honey(decimal amount = 0.01m)
  {
    return From("Honey", amount, "Bottle");
  }

  public static Ingredient Cheese(decimal amount = 20m)
  {
    return From("Cheese", amount, "Gram");
  }

  public static Ingredient TomatoSause(decimal amount = 0.01m)
  {
    return From("Tomato sauce", amount, "Bottle");
  }

  public static Ingredient Yogurt(decimal amount = 1m)
  {
    return From("Yogurt", amount, "pack");
  }

  public static Ingredient FruitSaladSmall(decimal amount = 1m)
  {
    return From("Fruit salad small", amount, "Pack");
  }

  public static Ingredient Tomato(decimal amount = 1m)
  {
    return From("Tomato", amount, "Unit");
  }

  public static Ingredient Milk(decimal amount = 0.1m)
  {
    return From("Milk", amount, "Litre");
  }

  public static Ingredient Cereal(decimal amount = 0.2m)
  {
    return From("Cereal", amount, "Pack");
  }

  public static Ingredient Rusks(decimal amount = 1)
  {
    return From("Rusks", amount, "Pack");
  }

  public static Ingredient Ham(decimal amount = 1)
  {
    return From("Ham", amount, "Gram");
  }

  public static Ingredient Potato(decimal amount = 2)
  {
    return From("Potatoes", amount, "unit");
  }

  public static Ingredient Flour(decimal amount = 250m)
  {
    return From("Flour", amount, "gram");
  }

  public static Ingredient Yeast(decimal amount = 1)
  {
    return From("Yeast", amount, "Pack");
  }

  public static Ingredient Salt(decimal amount = 0.5m)
  {
    return From("Salt", amount, "gram");
  }

  public static Ingredient Sugar(decimal amount = 0.5m)
  {
    return From("Sugar", amount, "gram");
  }

  public static Ingredient SaladLeaves(decimal amount = 0.5m)
  {
    return From("Salad Leaves", amount, "pack");
  }

  public static Ingredient Butter(decimal amount = 20m)
  {
    return From("Butter", amount, "gram");
  }

  public static Ingredient Foil()
  {
    return new Ingredient(FoodCategory.Produce,"Foil",Unit.AUnit+1);
  }

  public static Ingredient Oil()
  {
    return new Ingredient(FoodCategory.Produce,"Oil",Unit.Bottle750 +0.10m);
  }

  public static Ingredient Water()
  {
    return new Ingredient(FoodCategory.Drink,"Water",Unit.Litre +0.10m);
  }
}
