namespace BreakingNomad.Ui.Components.MenuMaker.Models;

public class ShoppingListItem
{
  public FoodCategory Category;
  public string Name;
  public string Unit;
  public ValueWithUnitOfMeasure UnitValue;

  public ShoppingListItem(FoodCategory category, string name, ValueWithUnitOfMeasure unitValue)
  {
    Category = category;
    Name = name;
    UnitValue = unitValue;
  }
}
