using System.Reflection;

namespace BreakingNomad.Shared.Helpers;

internal static class ReflectionHelper
{
  public static IEnumerable<T> GetStaticProperties<T>(this Type type)
  {
    return type
      .GetFields(BindingFlags.Public | BindingFlags.Static)
      .Where(x => x.FieldType == typeof(T))
      .Select(x => (T)x.GetValue(null)!)
      .ToArray();
  }
}
