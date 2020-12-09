namespace Expenses.Web.Business.Extensions
{
  public static class StructExtensions
  {
    public static bool IsNullOrDefault<T>(this T value)
      where T : struct
    {
      bool isNullOrDefault = value.Equals(default(T));
      return isNullOrDefault;
    }
  }
}
