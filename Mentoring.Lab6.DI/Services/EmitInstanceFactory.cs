using System;
using System.Linq;
using System.Reflection.Emit;

namespace Mentoring.Lab6.DI.Services
{
    public class EmitInstanceFactory : IInstanceFactory
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

            var parametersTypes = parameters.Select(x => x.GetType())
                .ToArray();

            var createInstanceMethod = new DynamicMethod(string.Empty, type, parametersTypes);
            var ilCreateInstanceGenerator = createInstanceMethod.GetILGenerator();
            for (var i = 0; i < parametersTypes.Length; i++)
            {
                ilCreateInstanceGenerator.Emit(OpCodes.Ldarg, i);
            }

            var constructor = type.GetConstructor(parametersTypes);
            ilCreateInstanceGenerator.Emit(OpCodes.Newobj, constructor);
            ilCreateInstanceGenerator.Emit(OpCodes.Ret);

            return createInstanceMethod.Invoke(null, parameters);
        }
    }
}
