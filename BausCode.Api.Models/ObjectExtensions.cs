using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ServiceStack;

namespace BausCode.Api.Models
{
    public static class ObjectExtensions
    {
        public static Dictionary<string, object> ToDictionaryOfObjectsFromPropertiesWithAttribute<T, TA>(this T source)
            where T : class
            where TA : Attribute
        {
            return source.GetType().GetPublicProperties()
                .Where(x => x.GetCustomAttributes<TA>().Any())
                .ToDictionary(x =>
                {
                    var v = x.GetProperty(source);
                    return new KeyValuePair<string, object>(x.Name, v);
                });
        }

        public static bool DeepEquals<T>(this T source, T compare)
            where T : class
        {
            return source.ToObjectDictionary().EquivalentTo(compare.ToObjectDictionary(), (a, b) => a.Equals(b));
        }

        public static List<Tuple<string, object, object>> ToListOfInequivalentValues<T>(this T source, T compare)
            where T : Dictionary<string, object>
        {
            var result = new List<Tuple<string, object, object>>();

            foreach (var x in compare.Keys)
            {
                if (source.ContainsKey(x) && Equals(source[x], compare[x]))
                    continue;
                result.Add(new Tuple<string, object, object>(x, source[x], compare[x]));
            }

            return result;
        }

        public static void ThrowIfInequivalent<T>(this T source, T compare)
            where T : class
        {
            var ineq = source.ToObjectDictionary().ToListOfInequivalentValues(compare.ToObjectDictionary());

            if (ineq.Count > 0)
            {
                throw new ArgumentException(
                    $"The following property values are inequivalent: {ineq.Select(x => x.Item1).Join(", ")}");
            }
        }

        public static void ThrowIfInequivalentWithAttribute<T, TA>(this T source, T compare)
            where T : class
            where TA : Attribute
        {
            var a = source.ToDictionaryOfObjectsFromPropertiesWithAttribute<T, TA>();
            var b = compare.ToDictionaryOfObjectsFromPropertiesWithAttribute<T, TA>();
            var ineq = a.ToListOfInequivalentValues(b);

            if (ineq.Count > 0)
            {
                throw new ArgumentException(
                    $"The following property values are inequivalent: {ineq.Select(x => x.Item1).Join(", ")}");
            }
        }
    }
}