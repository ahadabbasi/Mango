namespace Mango.Common.Shared.Extensions;

public static class ToIntExtension
{
    public static int ToInt(this decimal value)
    {
        return int.Parse(value.ToString(format: "N0"));
    }
}