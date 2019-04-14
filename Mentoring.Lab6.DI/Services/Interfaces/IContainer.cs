using System;
using System.Reflection;

namespace Mentoring.Lab6.DI.Services
{
    public interface IContainer
    {
        void AddAssembly(Assembly assembly);
        void AddType(Type type);
        void AddType(Type type, Type baseType);
        object CreateInstance(Type type);
        T CreateInstance<T>()
            where T : class;
    }
}
