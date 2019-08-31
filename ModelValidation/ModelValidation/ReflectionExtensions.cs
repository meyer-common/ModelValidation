using System;
using System.Collections;
using System.Collections.Generic;

namespace Meyer.Common.ModelValidation
{
    internal static class ReflectionExtensions
    {
        public static bool IsComplexType(this Type type)
        {
            return type.IsClass && !type.IsValueType && !type.IsPrimitive && type.FullName != "System.String";
        }

        public static bool IsCollection(this Type type)
        {
            return type.GetInterface(typeof(ICollection).Name) != null || type.GetInterface(typeof(ICollection<>).Name) != null;
        }
    }
}