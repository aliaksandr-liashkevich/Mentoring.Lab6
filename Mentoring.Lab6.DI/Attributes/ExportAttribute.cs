using System;

namespace Mentoring.Lab6.DI.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited =  false)]
    public class ExportAttribute : Attribute
    {
        public ExportAttribute()
        {
        }

        public ExportAttribute(Type contract)
        {
            Contract = contract;
        }

        public Type Contract { get; }
    }
}
