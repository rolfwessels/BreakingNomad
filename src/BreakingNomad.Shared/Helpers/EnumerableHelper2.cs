namespace BreakingNomad.Shared.Helpers;

public static class EnumerableHelper2
{
  public static List<T> OrEmpty<T>(this List<T>? resultTrips)
  {
    return resultTrips??new List<T>();
  }
}
