using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Mentoring.Lab6.DI.Attributes;
using Mentoring.Lab6.DI.Extensions;

namespace Mentoring.Lab6.DI.Services
{
    public class Container : IContainer
    {
        private readonly IDictionary<Type, Type> _types;
        private readonly IInstanceFactory _instanceFactory;

        public Container(IInstanceFactory instanceFactory)
        {
            _types = new Dictionary<Type, Type>();
            _instanceFactory = instanceFactory ?? throw new ArgumentNullException(nameof(instanceFactory));
        }

        public void AddAssembly(Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            var exportedTypes = assembly.ExportedTypes;

            foreach (var exportedType in exportedTypes)
            {
                if (HasImportConstructor(exportedType)
                    || HaveImportProperties(exportedType))
                {
                    _types.Add(exportedType, exportedType);
                }

                var exportAttributes = exportedType.GetCustomAttributes<ExportAttribute>();

                foreach (var exportAttribute in exportAttributes)
                {
                    var exportContract = exportAttribute.Contract;
                    _types.Add(exportContract ?? exportedType, exportedType);
                }
            }
        }

        public void AddType(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            _types.Add(type, type);
        }

        public void AddType(Type type, Type baseType)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (baseType == null)
            {
                throw new ArgumentNullException(nameof(baseType));
            }

            _types.Add(baseType, type);
        }

        public T CreateInstance<T>()
            where T : class
        {
            var type = typeof(T);
            var instance = (T) CreateInstance(type);

            return instance;
        }

        public object CreateInstance(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (!_types.ContainsKey(type))
            {
                throw new ArgumentException($"Type full name: {type.FullName}. Type not found.");
            }

            var typeToCreate = _types[type];
            var constructor = typeToCreate.GetConstructor();
            var instance = CreateInstanceFromConstructor(typeToCreate, constructor);

            if (HasImportConstructor(typeToCreate))
            {
                return instance;
            }

            ResolvePropertiesToImport(typeToCreate, instance);

            return instance;
        }

        private void ResolvePropertiesToImport(Type typeToCreate, object instance)
        {
            var propertiesToImport = typeToCreate.GetPropertiesToImport();
            foreach (var propertyToImport in propertiesToImport)
            {
                var resolvedInstanceProperty = CreateInstance(propertyToImport.PropertyType);
                propertyToImport.SetValue(instance, resolvedInstanceProperty);
            }
        }

        private object CreateInstanceFromConstructor(Type typeToCreate, ConstructorInfo constructor)
        {
            var parameters = constructor.GetParameters();

            if (parameters.Length == 0)
            {
                return _instanceFactory.CreateInstance(typeToCreate);
            }

            var instanceParameters = new List<object>(parameters.Length);

            foreach (var constructorParameter in parameters)
            {
                var instanceParameter = CreateInstance(constructorParameter.ParameterType);
                instanceParameters.Add(instanceParameter);
            }

            return _instanceFactory.CreateInstance(typeToCreate, instanceParameters.ToArray());
        }

        private bool HaveImportProperties(Type exportedType)
        {
            return exportedType.GetPropertiesToImport().Any();
        }

        private bool HasImportConstructor(Type exportedType)
        {
            return exportedType.GetCustomAttribute<ImportConstructorAttribute>() != null;
        }
    }
}
