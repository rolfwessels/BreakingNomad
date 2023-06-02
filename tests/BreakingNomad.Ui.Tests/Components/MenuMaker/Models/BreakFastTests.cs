using BreakingNomad.Ui.Components.MenuMaker.Models;
using Bumbershoot.Utilities.Helpers;
using ConsoleTables;

namespace BreakingNomad.Ui.Tests.Components.MenuMaker.Models;

public class BreakFastTests
{
  [Test]
  public void MealRecipes_GivenWhenRequestingAll_ShouldReturnAll()
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

  [Test]
  public void Side_GivenWhenRequestingAll_ShouldReturnAll()
  {
    // arrange
    var mealRecipes = Side.All().ToArray();
    // action
    
    // assert
    var table = new ConsoleTable("Type", "Name", "Ingredients");
    foreach (var mealRecipe in mealRecipes)
    {
      table.AddRow(mealRecipe.MealType.ToString(), mealRecipe.Name, mealRecipe.Ingredients.Select(x=>x.Name +" "+x.Value).StringJoin());  
    }
    table.Write();
  }

  [Test]
  public void Dinner_GivenWhenRequestingAll_ShouldReturnAll()
  {
    // arrange
    var mealRecipes = Dinner.All().ToArray();
    // action
    
    // assert
    var table = new ConsoleTable("Type", "Name", "Ingredients");
    foreach (var mealRecipe in mealRecipes)
    {
      table.AddRow(mealRecipe.MealType.ToString(), mealRecipe.Name, mealRecipe.Ingredients.Select(x=>x.Name +" "+x.Value).StringJoin());  
    }
    table.Write();
  }

  [Test]
  public void Dessert_GivenWhenRequestingAll_ShouldReturnAll()
  {
    // arrange
    var mealRecipes = Dessert.All().ToArray();
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
