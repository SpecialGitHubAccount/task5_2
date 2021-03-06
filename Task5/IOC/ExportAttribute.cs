﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5.IOC
{
    [AttributeUsage(AttributeTargets.Class)]
    class ExportAttribute : Attribute
    {
        public ExportAttribute() { }

        public ExportAttribute(Type contractType)
        {
            ContractType = contractType;
        }

        public Type ContractType { get; set; }
    }
}
