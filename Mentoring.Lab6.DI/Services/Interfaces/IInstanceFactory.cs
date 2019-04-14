using System;

namespace Mentoring.Lab6.DI.Services
{
    public interface IInstanceFactory
    {
        object CreateInstance(Type type, params object[] parameters);
    }
}
