using Notifyre.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Notifyre.Infrastructure.Utils
{
    public static class EnumUtil
    {
        public static string ToString(Type targetType, object input)
        {
            string name = input.ToString();
            var field = targetType.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(d => d.IsLiteral && !d.IsInitOnly && string.Equals(name, d.Name, StringComparison.OrdinalIgnoreCase))
                .Select(d => d.GetValue(null))
                .FirstOrDefault();
            return (string)field;
        }
    }
}
