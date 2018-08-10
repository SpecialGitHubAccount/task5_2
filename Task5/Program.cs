using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task5.IOC;

namespace Task5
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container();
            container.AddType(typeof(CustomerBLL));
            container.AddType(typeof(Logger));
            container.AddType(typeof(CustomerDAL), typeof(ICustomerDAL));

            CustomerBLL customerBLL = (CustomerBLL)container.CreateInstance(
                typeof(CustomerBLL));

            Console.WriteLine(customerBLL.ToString());

            Console.ReadLine();
        }
    }
}
