using BreakingNomad.Shared.Services;
using BreakingNomad.Ui.Components.MenuMaker.Models;
using FluentAssertions;

namespace BreakingNomad.Shared.Tests;

public class ShoppingListTests
{
  [Test]
  public void Constructor_GivenNoInput_ShouldGiveEmptyList()
  {
    // arrange
    // action
    var shoppingList = new ShoppingList(new List<MealRecipe>(),new TripMenu(),new List<IngredientPerDay>());
    // assert
    shoppingList.Items.Should().BeEmpty();
  }

  [Test]
  public void Constructor_GivenDailyIngredients_ShouldDisplayByDayAndPerson()
  {
    // arrange
    var startDate = DateTime.Now.Date;
    var tripMenu = TripMenu.From("","",startDate,startDate.AddDays(2),3);
    var ingredientPerDays = new List<IngredientPerDay>() { new(1,new Ingredient(FoodCategory.Alcohol,"Beer",Unit.CanInSixPack))};
    // action
    var shoppingList = new ShoppingList(new List<MealRecipe>(),tripMenu,ingredientPerDays);
    // assert
    shoppingList.Items.Should().HaveCount(1);
    shoppingList.Items[0].Name.Should().Be("Beer");
    shoppingList.Items[0].UnitValue.Value.Should().Be(6);
  }


  [Test]
  public void Constructor_GivenSingleMeal_ShouldContainItems()
  {
    // arrange
    var startDate = DateTime.Now.Date;
    var tripMenu = TripMenu.From("","",startDate,startDate.AddDays(2),3);
    var ingredientPerDays = new List<IngredientPerDay>();
    var allowedValues = new List<MealRecipe>() { new(MealType.Breakfast,"Omelette", new [] 
      {
        BasicIngredients.Eggs(2),
        BasicIngredients.Cheese(150),
        BasicIngredients.Ham()
      }
    )};
    tripMenu.MealsOfTheDay.Add(new MealsOfTheDay(1,MealType.Breakfast, new List<string>(){allowedValues[0].Key}));
    // action
    var shoppingList = new ShoppingList(allowedValues,tripMenu,ingredientPerDays);
    // assert
    shoppingList.Items.Should().HaveCount(3);
    var shoppingListItem = shoppingList.Items.First(x=>x.Name == "Eggs");
    shoppingListItem.UnitValue.Value.Should().Be(6);
  }

  [Test]
  public void Constructor_GivenSingleMealOverTwoDays_ShouldContainItems()
  {
    // arrange
    var startDate = DateTime.Now.Date;
    var tripMenu = TripMenu.From("","",startDate,startDate.AddDays(2),3);
    var ingredientPerDays = new List<IngredientPerDay>();
    var allowedValues = new List<MealRecipe>() { new(MealType.Breakfast,"Omelette", new [] 
      {
        BasicIngredients.Eggs(2),
        BasicIngredients.Cheese(150),
        BasicIngredients.Ham()
      }
    )};
    tripMenu.MealsOfTheDay.Add(new MealsOfTheDay(1,MealType.Breakfast, new List<string>(){allowedValues[0].Key}));
    tripMenu.MealsOfTheDay.Add(new MealsOfTheDay(1,MealType.Lunch, new List<string>(){allowedValues[0].Key}));
    // action
    var shoppingList = new ShoppingList(allowedValues,tripMenu,ingredientPerDays);
    // assert
    shoppingList.Items.Should().HaveCount(3);
    var shoppingListItem = shoppingList.Items.First(x=>x.Name == "Eggs");
    shoppingListItem.UnitValue.Value.Should().Be(12);
  }

}
