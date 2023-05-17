namespace BreakingNomad.Ui.Components.MenuMaker.Models;

public class BasicItems
{
  public static SimpleRoundedItem Eggs(decimal amount = 1)
  {
    return new SimpleRoundedItem("Eggs", amount, "eggs");
  }

  public static SimpleRoundedItem Bacon(decimal amount = 1)
  {
    return new SimpleRoundedItem("Bacon", amount, "pack", 2);
  }

  public static SimpleRoundedItem BreadSlice(decimal amount = 2)
  {
    return new SimpleRoundedItem("Bread", amount, "loaf", 24);
  }

  public static SimpleRoundedItem Honey(decimal amount = 0.01m)
  {
    return new SimpleRoundedItem("Honey", amount, "bottle");
  }

  public static SimpleRoundedItem Cheese(decimal amount = 20m)
  {
    return new SimpleRoundedItem("Cheese", amount, "grams");
  }

  public static SimpleRoundedItem TomatoSause(decimal amount = 0.01m)
  {
    return new SimpleRoundedItem("Tomato sause", amount, "bottle");
  }

  public static SimpleRoundedItem Yogurt(decimal amount = 1m)
  {
    return new SimpleRoundedItem("Yogurt", amount, "pack");
  }

  public static SimpleRoundedItem FruitSaladSmall(decimal amount = 1m)
  {
    return new SimpleRoundedItem("Fruit salad small", amount, "Pack");
  }

  public static SimpleRoundedItem Tomato(decimal amount = 1m)
  {
    return new SimpleRoundedItem("Tomato", amount, "Tomato");
  }

  public static SimpleRoundedItem Milk(decimal amount = 0.1m)
  {
    return new SimpleRoundedItem("Milk", amount, "Litre");
  }

  public static SimpleRoundedItem Cereal(decimal amount = 0.2m)
  {
    return new SimpleRoundedItem("Cereal", amount, "Pack");
  }

  public static SimpleRoundedItem Rusks(decimal amount = 1)
  {
    return new SimpleRoundedItem("Rusks", amount, "Pack", 20);
  }

  public static SimpleRoundedItem Ham(decimal amount = 1)
  {
    return new SimpleRoundedItem("Ham", amount, "Grams");
  }

  public static SimpleRoundedItem Potatoe(decimal amount = 2)
  {
    return new SimpleRoundedItem("Potatoes", amount, "Potatoes");
  }

  public static SimpleRoundedItem Flour(decimal amount = 0.25m)
  {
    return new SimpleRoundedItem("Flour", amount, "KG");
  }

  public static SimpleRoundedItem Yeast(decimal amount = 1)
  {
    return new SimpleRoundedItem("Yeast", amount, "Pack");
  }

  public static SimpleRoundedItem Salt(decimal amount = 0.5m)
  {
    return new SimpleRoundedItem("Salt", amount, "grams");
  }

  public static SimpleRoundedItem Sugar(decimal amount = 0.5m)
  {
    return new SimpleRoundedItem("Sugar", amount, "grams");
  }

  public static SimpleRoundedItem SaladLeaves(decimal amount = 0.5m)
  {
    return new SimpleRoundedItem("Salad Leaves", amount, "pack");
  }

  public static SimpleRoundedItem Butter(decimal amount = 20m)
  {
    return new SimpleRoundedItem("Butter", amount, "grams");
  }
}
