using System.ComponentModel;
using System.Reflection;

namespace ShowTrack.Extensions;

public static class Extensions
{
    public static string Description(this Enum value)
    {
        return
            value.GetType().GetMember(value.ToString()).FirstOrDefault()
                ?.GetCustomAttribute<DescriptionAttribute>()
                ?.Description
            ?? value.ToString();
    }
}