using Notifyre.Infrastructure.Annotations;
using System;
using System.Reflection;

namespace Notifyre.Infrastructure.Utils
{
    public static class UrlUtil
    {
        public static string SerializeQuery(object parameters)
        {
            string query = "";
            foreach (var prop in parameters.GetType().GetProperties())
            {
                if (Attribute.IsDefined(prop, typeof(QueryParamAttribute)))
                {
                    query += $"{prop.Name}={FormatValue(prop, parameters)}&";
                }
            }
            return query.Substring(0, query.Length - 1).ToLower();
        }

        private static string FormatValue(PropertyInfo prop, object obj)
        {
            if (prop.PropertyType == typeof(DateTime?))
            {
                return ((DateTime?)prop.GetValue(obj))?.ToString("yyyy-MM-dd") ?? "";
            }
            else if (prop.PropertyType == typeof(DateTime))
            {
                return ((DateTime)prop.GetValue(obj)).ToString("yyyy-MM-dd");
            }
            else if (prop.PropertyType.IsEnum)
            {
                var queryParamAttribute = (QueryParamAttribute)Attribute.GetCustomAttribute(prop, typeof(QueryParamAttribute));
                if (queryParamAttribute.Type != null)
                {
                    return EnumUtil.ToString(queryParamAttribute.Type, prop.GetValue(obj));
                }
            }
            return prop.GetValue(obj)?.ToString() ?? "";
        }

        public static Uri CreateUrlWithQuery(object query, params string[] routes)
        {
            string url = string.Join("/", routes);
            UriBuilder builder = new UriBuilder(url);
            builder.Query = UrlUtil.SerializeQuery(query);
            return builder.Uri;
        }

        public static Uri CreateUrl(params string[] routes)
        {
            string url = string.Join("/", routes);
            UriBuilder builder = new UriBuilder(url);
            return builder.Uri;
        }
    }
}
