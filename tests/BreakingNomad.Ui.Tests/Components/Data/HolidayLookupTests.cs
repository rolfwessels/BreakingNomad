using System.Diagnostics;
using BreakingNomad.Ui.Components.Data;
using FluentAssertions;

namespace BreakingNomad.Ui.Tests.Components.Data;

public class HolidayLookupTests
{

  [Test]
  public void Parse_GivenValidCsv_ShouldReturnCorrectRecords()
  {
    
    // act
    var actual = StaticPublicHolidaySample.GetPublicHolidays();

    // assert
    actual.Should().HaveCountGreaterThan(2);

    actual[0].Date.Should().Be(new DateTime(2023, 1, 2));
    actual[0].Name.Should().Be("New Year's Day");

    actual[1].Date.Should().Be(new DateTime(2023, 3, 21));
    actual[1].Name.Should().Be("Human Rights Day");
  }

  [Test]
  public void GetUpcomingLongWeekends_GivenHolidays_ShouldReturnOnlyUpComingLongWeekends()
  {
    // action
    var upcomingLongWeekends = new HolidayLookup().GetUpcomingLongWeekends(new DateTime(2023,04,23));
    // assert
    var weekend = upcomingLongWeekends.First();
    weekend.StartDate.Should().Be(new DateTime(2023 , 04 , 27));
    weekend.EndDate.Should().Be(new DateTime(2023, 05 , 01));
    weekend.RequiresExtraDay.Should().Be(true);
  }

  [Test]
  public void GetUpcomingLongWeekends_GivenUpcomingLongWeekend_ShouldNotReturnMondayIfItWasUsed()
  {
    // action
    var upcomingLongWeekends = new HolidayLookup().GetUpcomingLongWeekends(new DateTime(2023, 04, 23));
    // assert
    var weekend = upcomingLongWeekends.Skip(1).First();
    new DateTime(2023, 05, 01).Should().NotBe(weekend.StartDate);
    new DateTime(2023, 05, 01).Should().NotBe(weekend.EndDate);
  }
  
  [Test]
  public void GetUpcomingLongWeekends_GivenTuesdayLongWeekend_ShouldNotReturnMondayIfItWasUsed()
  {
    // action
    var upcomingLongWeekends = new HolidayLookup().GetUpcomingLongWeekends(new DateTime(2023,03,15));
    // assert
    var weekend = upcomingLongWeekends.First();
    weekend.StartDate.Should().Be(new DateTime(2023, 03, 17));
    weekend.EndDate.Should().Be(new DateTime(2023, 03, 21));
    weekend.RequiresExtraDay.Should().Be(true);
  }

  [Test]
  [Explicit]
  [Category("Integration")]
  public async Task GetUpcomingLongWeekends_GivenRequest_ShouldShowAll()
  {
    // action
    var holidays = await NagerDateApi.GetPublicHolidays("za", 2023);
    var upcomingLongWeekends = new HolidayLookup(NagerDateApi.ToSimpleList(holidays)).GetUpcomingLongWeekends(new DateTime(2023, 03, 15));
    // assert
    
    Console.Out.WriteLine("Date,Day,Holiday");
    foreach (var we in holidays)
    {
      Console.WriteLine($"{we.Date:yyyy-MM-dd},{we.Date.DayOfWeek},{we.LocalName}");
    }

    foreach (var we in upcomingLongWeekends)
    {
      Console.Out.WriteLine($"{we.StartDate:dd/MM} {we.StartDate.DayOfWeek} {we.EndDate:dd/MM} {we.EndDate.DayOfWeek} {we.RequiresExtraDay} {we.Name}");
    }
  }


}
