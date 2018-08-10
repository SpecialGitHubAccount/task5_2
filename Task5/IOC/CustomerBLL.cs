using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5.IOC
{
    //[ImportConstructor]
    class CustomerBLL
    {
        public CustomerBLL(ICustomerDAL ic, Logger logger)
        {
            CustomerDAL = ic;
            Logger = logger;
        }

        public CustomerBLL()
        {
        }

        [ImportAttribute]
        public ICustomerDAL CustomerDAL { get; set; }
        [ImportAttribute]
        public Logger Logger { get; set; }

    }
}
