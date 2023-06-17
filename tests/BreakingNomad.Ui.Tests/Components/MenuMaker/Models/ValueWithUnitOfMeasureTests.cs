using BreakingNomad.Shared;
using FluentAssertions;

namespace BreakingNomad.Ui.Tests.Components.MenuMaker.Models;

public class ValueWithUnitOfMeasureTests
{
  [Test]
  public void All_GivenRequest_ShouldHaveUniqueName()
  {
    // action 
    var enumerable = Unit.All().Select(x=>x.Name);
    // assert
    enumerable.Should().OnlyHaveUniqueItems();
  }



  [Test]
  [TestCase(1, "1 Half Carton (1)")]
  [TestCase(5, "1 Half Carton (5)")]
  [TestCase(6, "1 Half Carton")]
  [TestCase(7, "1 Carton (7)")]
  [TestCase(12, "1 Carton")]
  [TestCase(13, "2 Carton (13)")]
  public void Carton_GivenDifferentValues_ShouldHaveCompleteValueAndDisplayValue(decimal value,string expected)
  {
    // arrange
    var egg = Unit.Carton + value ;
    // action
    var stringValue = egg.ToString();
    // assert
    stringValue.Should().Be(expected);
  }
  
  [Test]
  [TestCase(1, "1 Half Loaf (1)")]
  [TestCase(5, "1 Half Loaf (5)")]
  [TestCase(12, "1 Half Loaf")]
  [TestCase(13, "1 Loaf (13)")]
  [TestCase(24, "1 Loaf")]
  public void Loaf_GivenDifferentValues_ShouldHaveCompleteValueAndDisplayValue(decimal value,string expected)
  {
    // arrange
    var egg = Unit.Loaf + value ;
    // action
    var stringValue = egg.ToString();
    // assert
    stringValue.Should().Be(expected);
  }

  [Test]
  [TestCase(1, "1 Can")]
  [TestCase(5, "5 Cans")]
  [TestCase(12, "2 Six Packs (12)")]
  [TestCase(13, "3 Six Packs (13)")]
  [TestCase(24, "1 Slab")]
  public void SixPack_GivenDifferentValues_ShouldHaveCompleteValueAndDisplayValue(decimal value,string expected)
  {
    // arrange
    var egg = Unit.CanInSixPack + value ;
    // action
    var stringValue = egg.ToString();
    // assert
    stringValue.Should().Be(expected);
  }
}
