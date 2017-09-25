using System;

namespace PublicLib
{
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumAttribute: Attribute
    {
        public string Name { get; set; }
    }
}
