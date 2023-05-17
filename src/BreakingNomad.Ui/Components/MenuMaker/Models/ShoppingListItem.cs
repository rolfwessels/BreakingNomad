namespace BreakingNomad.Ui.Components.MenuMaker.Models;

public class ShoppingListItem
{
  public string Category;
  public string Name;
  public string Unit;
  public decimal UnitValue;

  public ShoppingListItem(string category, string name, decimal unitValue, string unit)
  {
    Category = category;
    Name = name;
    UnitValue = unitValue;
    Unit = unit;
  }
}
