using System;
using Mentoring.Lab6.App.TestClasses.Export;
using Mentoring.Lab6.App.TestClasses.Import;
using Mentoring.Lab6.DI.Services;

namespace Mentoring.Lab6.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var activatorInstanceFactory = new ActivatorInstanceFactory();
            var emitInstanceFactory = new EmitInstanceFactory();

            var container = new Container(activatorInstanceFactory);

            // Using assembly
            //container.AddAssembly(Assembly.GetExecutingAssembly());

            // Using all types
            container.AddType(typeof(CustomExportWithoutInterface));
            container.AddType(typeof(CustomExport), typeof(ICustomExport));
            container.AddType(typeof(CustomImportConstructor));
            container.AddType(typeof(CustomImportProperties));

            var customImportConstructor = container.CreateInstance<CustomImportConstructor>();
            var customImportProperties = container.CreateInstance<CustomImportProperties>();
            customImportProperties.EnsureThatClassHasProperties();

            Console.WriteLine("Instances was created.");
            Console.ReadKey();
        }
    }
}
