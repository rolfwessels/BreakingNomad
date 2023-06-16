using BreakingNomad.Ui.Pages;
using Microsoft.AspNetCore.Components.Web;

namespace BreakingNomad.Ui.Components.Data;

public class HolidayLookup
{
  private readonly List<Holiday> _publicHolidays;


  public HolidayLookup()
  {
    _publicHolidays = StaticPublicHolidaySample.GetPublicHolidays();
  }

  public HolidayLookup(List<Holiday> publicHolidays)
  {
    _publicHolidays = publicHolidays;
  }

  public IEnumerable<HolidayLookup.Weekend> GetUpcomingLongWeekends(DateTime fromDate)
  {
    
    var publicHolidays = _publicHolidays;
    for (var index = 0; index < publicHolidays.Count; index++)
    {
      var publicHoliday = publicHolidays[index];
      if (publicHoliday.Date > fromDate && publicHoliday.Date.DayOfWeek is DayOfWeek.Thursday or DayOfWeek.Friday)
      {
        var firstSunday = publicHoliday.Date.AddDays(7 - (int)publicHoliday.Date.DayOfWeek);
        if (publicHolidays.Select(x=>x.Date).Contains(firstSunday.AddDays(1)) )
        {
          firstSunday = firstSunday.AddDays(1);
          index++;
        }

        var endDate = firstSunday;
        yield return new HolidayLookup.Weekend(publicHoliday.Date, endDate, publicHoliday.Date.DayOfWeek is DayOfWeek.Thursday, publicHoliday.Name);
      }
      if (publicHoliday.Date > fromDate && publicHoliday.Date.DayOfWeek is DayOfWeek.Monday or DayOfWeek.Tuesday)
      {
        var firstFridayBefore = publicHoliday.Date.AddDays(-1 * ((int)publicHoliday.Date.DayOfWeek + 2));
        
        yield return new HolidayLookup.Weekend(firstFridayBefore, publicHoliday.Date, publicHoliday.Date.DayOfWeek is DayOfWeek.Tuesday, publicHoliday.Name);
      }
    }
  }

  public record Holiday(DateTime Date, string Day, string Name);


  public record Weekend(DateTime StartDate, DateTime EndDate, bool RequiresExtraDay, string Name)
  {
    public int Days => (int)(EndDate - StartDate).TotalDays+1;
  }
}
