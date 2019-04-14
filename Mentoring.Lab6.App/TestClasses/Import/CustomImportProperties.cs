using System;
using Mentoring.Lab6.App.TestClasses.Export;
using Mentoring.Lab6.DI.Attributes;

namespace Mentoring.Lab6.App.TestClasses.Import
{
    public class CustomImportProperties
    {
        [Import]
        public ICustomExport CustomExport { get; set; }
        [Import]
        public CustomExportWithoutInterface CustomExportWithoutInterface { get; set; }

        public void EnsureThatClassHasProperties()
        {
            if (CustomExport == null)
            {
                throw new InvalidOperationException($"{nameof(CustomExport)} must be set.");
            }

            if (CustomExportWithoutInterface == null)
            {
                throw new InvalidOperationException($"{nameof(CustomExportWithoutInterface)} must be set.");
            }
        }
    }
}
