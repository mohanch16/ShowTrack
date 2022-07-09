using System.ComponentModel;
using System.Reflection;
using System.Collections.Generic;

namespace ShowTrack.Shared.Extensions;

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

    public static string[] GetEnumDescriptions<T>(T[] excludeItems) where T : Enum
    {
         var enumValues = (T[])Enum.GetValues(typeof(T));
         return enumValues
                        .Where(st => !excludeItems
                                .Any(itemValue => itemValue.Equals(st)))
                                .Select(t => t.Description()).ToArray();
    }

    public static T[] GetFilteredEnumValues<T>(T[]? excludeItems = null) where T : Enum
    {
         var enumValues = (T[])Enum.GetValues(typeof(T));

         if (excludeItems == null) 
         {
            return enumValues;
         }

         return enumValues.Where(st => !excludeItems
                                .Any(itemValue => itemValue.Equals(st))).ToArray();
    }

    public static bool EnumListEquals<T>(List<T> one, List<T> two) where T : struct, IConvertible
    {
        if (!typeof(T).IsEnum)
        {
            throw new ArgumentException("T must be an enumerated type");
        }
        return (one.Count() == two.Count() &&
                one.Count(two.Contains) == two.Count());
    }
}