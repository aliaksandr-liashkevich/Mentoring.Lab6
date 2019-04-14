using Mentoring.Lab6.DI.Attributes;

namespace Mentoring.Lab6.App.TestClasses.Export
{
    [Export(typeof(ICustomExport))]
    public class CustomExport : ICustomExport
    {
    }
}
