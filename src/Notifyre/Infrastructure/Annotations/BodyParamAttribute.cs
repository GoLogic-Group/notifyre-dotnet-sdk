using System;

namespace Notifyre.Infrastructure.Annotations
{
    public class BodyParamAttribute : Attribute
    {
        public Type Type { get; set; }

        public BodyParamAttribute() { }
        public BodyParamAttribute(Type type) { Type = type; }
    }
}
