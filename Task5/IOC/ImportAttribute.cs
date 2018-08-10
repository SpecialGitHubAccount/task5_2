using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5.IOC
{
    [AttributeUsage(AttributeTargets.Property)]
    class ImportAttribute : Attribute
    {
        public ImportAttribute() { }

        public ImportAttribute(Type contractType)
        {
            ContractType = contractType;
        }

        public Type ContractType { get; set; }
    }
}