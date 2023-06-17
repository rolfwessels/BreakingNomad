using BreakingNomad.Shared;

namespace BreakingNomad.Ui.Components.MenuMaker.Models;

public class Recipy
{
  public List<SimpleRoundedItem> Items = new();
  public MealType MealType;
  public string Name;
  public int Used;

  public Recipy As(MealType mealTypeType)
  {
    MealType = mealTypeType;
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
      MealType = b.MealType,
      Items = b.Items.Concat(c.Items).ToList(),
      Used = 0
    };
  }
}
