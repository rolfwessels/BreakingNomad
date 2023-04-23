using System.Globalization;
using CsvHelper;

namespace BreakingNomad.Ui.Components.Data;

public class StaticPublicHolidaySample
{
  public static List<HolidayLookup.Holiday> GetPublicHolidays()
  {
    const string Holidays = @"Date,Day,Holiday
2023-01-02,Monday,New Year's Day
2023-03-21,Tuesday,Human Rights Day
2023-04-07,Friday,Good Friday
2023-04-10,Monday,Family Day
2023-04-27,Thursday,Freedom Day
2023-05-01,Monday,Workers' Day
2023-06-16,Friday,Youth Day
2023-08-09,Wednesday,National Women's Day
2023-09-25,Monday,Heritage Day
2023-12-16,Saturday,Day of Reconciliation
2023-12-25,Monday,Christmas Day
2023-12-26,Tuesday,Day of Goodwill";

    using var reader = new StringReader(Holidays);
    using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
      
    var holidays = new List<HolidayLookup.Holiday>();

    csv.Read();
    csv.ReadHeader();
    while (csv.Read())
    {
      var date = csv.GetField<DateTime>("Date");
      var day = csv.GetField<string>("Day");
      var name = csv.GetField<string>("Holiday");
      holidays.Add(new HolidayLookup.Holiday(date, day!, name!));
    }

    return holidays;
  }
}
