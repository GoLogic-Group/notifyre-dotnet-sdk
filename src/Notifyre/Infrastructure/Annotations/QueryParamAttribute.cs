using System;

namespace Notifyre.Infrastructure.Annotations
{
    public class QueryParamAttribute : Attribute
    {
        public Type Type { get; set; }

        public QueryParamAttribute() { }
        public QueryParamAttribute(Type type) { Type = type; }
    }
}
