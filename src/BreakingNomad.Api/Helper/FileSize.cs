namespace BreakingNomad.Api.Helper
{
  public static class FileSize
  {
    public static int Mb(this int i)
    {
      return i * 1024 * 1024;
    }

  }
}
