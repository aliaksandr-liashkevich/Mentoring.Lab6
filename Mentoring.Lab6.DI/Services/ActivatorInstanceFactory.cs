using System;

namespace Mentoring.Lab6.DI.Services
{
    public class ActivatorInstanceFactory : IInstanceFactory
    {
        public object CreateInstance(Type type, params object[] parameters)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            return Activator.CreateInstance(type, parameters);
        }
    }
}
