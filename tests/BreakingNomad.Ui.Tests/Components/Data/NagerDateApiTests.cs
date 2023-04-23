using BreakingNomad.Ui.Components.Data;
using Bumbershoot.Utilities.Helpers;
using FluentAssertions;

namespace BreakingNomad.Ui.Tests.Components.Data;

public class NagerDateApiTests
{
  [Test]
  [Explicit]
  [Category("Integration")]
  public async Task GetPublicHolidaysAsync_Given2023SouthAfrica_ReturnsValidData()
  {
    // action
    var publicHolidays = await NagerDateApi.GetPublicHolidays("za",2023);
    // assert
    publicHolidays.Dump("publicHolidays");
    
    publicHolidays.Should().NotBeEmpty();
    publicHolidays.Should().HaveCount(12);
    publicHolidays.Should().Contain(holiday => holiday.Name == "New Year's Day");
    publicHolidays.Should().Contain(holiday => holiday.Name == "Human Rights Day");

  }


}
