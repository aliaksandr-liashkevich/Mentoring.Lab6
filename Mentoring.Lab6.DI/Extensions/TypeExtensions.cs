using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Mentoring.Lab6.DI.Attributes;

namespace Mentoring.Lab6.DI.Extensions
{
    public static class TypeExtensions
    {
        public static IReadOnlyList<PropertyInfo> GetPropertiesToImport(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.GetProperties().Where(p => p.GetCustomAttribute<ImportAttribute>() != null)
                .ToList();
        }

        public static ConstructorInfo GetConstructor(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            var constructors = type.GetConstructors();

            if (constructors.Length == 0)
            {
                throw new ArgumentException($"Type full name: {type.FullName}. Type constructor not found.");
            }

            return constructors.First();
        }
    }
}
