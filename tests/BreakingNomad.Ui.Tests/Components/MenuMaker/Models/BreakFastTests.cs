using BreakingNomad.Ui.Components.MenuMaker.Models;
using Bumbershoot.Utilities.Helpers;
using ConsoleTables;

namespace BreakingNomad.Ui.Tests.Components.MenuMaker.Models;

public class BreakFastTests
{
  [Test]
  public void MealRecipes_GivenGivenValues_ShouldReturnAll()
  {
    // arrange
    var mealRecipes = BreakFast.All().ToArray();
    // action
    
    // assert
    var table = new ConsoleTable("Type", "Name", "Ingredients");
    foreach (var mealRecipe in mealRecipes)
    {
      table.AddRow(mealRecipe.MealType.ToString(), mealRecipe.Name, mealRecipe.Ingredients.Select(x=>x.Name +" "+x.Value).StringJoin());  
    }
    table.Write();
  }


}
