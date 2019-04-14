using System;
using Mentoring.Lab6.App.TestClasses.Export;
using Mentoring.Lab6.DI.Attributes;

namespace Mentoring.Lab6.App.TestClasses.Import
{
    [ImportConstructor]
    public class CustomImportConstructor
    {
        public CustomImportConstructor(ICustomExport customExport,
            CustomExportWithoutInterface customExportWithoutInterface)
        {
            CustomExport = customExport ?? throw new ArgumentNullException(nameof(customExport));
            CustomExportWithoutInterface = customExportWithoutInterface ?? throw new ArgumentNullException(nameof(customExportWithoutInterface));
        }

        public ICustomExport CustomExport { get; }
        public CustomExportWithoutInterface CustomExportWithoutInterface { get; }
    }
}
