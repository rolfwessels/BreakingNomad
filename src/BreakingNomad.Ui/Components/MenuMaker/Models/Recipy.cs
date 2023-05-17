namespace BreakingNomad.Ui.Components.MenuMaker.Models;

public class Recipy
{
  public List<SimpleRoundedItem> Items = new();
  public Meal Meal;
  public string Name;
  public int Used;

  public Recipy As(Meal mealType)
  {
    Meal = mealType;
    return this;
  }

  public Recipy MarkUsed(bool mark = true)
  {
    if (!mark)
    {
      Used = 0;
    }
    else
    {
      Used++;
    }

    return this;
  }

  public static Recipy operator +(Recipy b, Recipy c)
  {
    return new Recipy
    {
      Name = b.Name + " + " + c.Name,
      Meal = b.Meal,
      Items = b.Items.Concat(c.Items).ToList(),
      Used = 0
    };
  }
}
