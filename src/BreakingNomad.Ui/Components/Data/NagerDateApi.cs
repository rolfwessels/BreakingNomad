using System.Text.Json;

namespace BreakingNomad.Ui.Components.Data;

public class NagerDateApi
{
  public record PublicHoliday(DateTime Date, string LocalName, string Name, string CountryCode, bool Fixed, bool Global, string Type);

  public static async Task<List<PublicHoliday>> GetPublicHolidays(string countryCode, int year)
  {
    var url = $"https://date.nager.at/api/v3/PublicHolidays/{year}/{countryCode}";
    using var httpClient = new HttpClient();
    using var response = await httpClient.GetAsync(url);
    var content = await response.Content.ReadAsStringAsync();
    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    var holidays = JsonSerializer.Deserialize<List<PublicHoliday>>(content, options)!;
    return holidays;
  }

  public static List<HolidayLookup.Holiday> ToSimpleList(List<PublicHoliday> holidays)
  {
    return holidays.Select(holiday => new HolidayLookup.Holiday(holiday.Date, holiday.Date.DayOfWeek.ToString(), holiday.LocalName)).ToList();
  }

  public static async Task<List<HolidayLookup.Holiday>> GetPublicHolidaysAsHolidayRecords(string countryCode, int year)
  {
    var publicHolidays = await GetPublicHolidays(countryCode, year);
    return ToSimpleList(publicHolidays);
  }
}
