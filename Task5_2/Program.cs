using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Task5_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person()
            {
                Address = new Address
                {
                    City = new City
                    {
                        Region = "Karagandy"
                    }
                }
            };

            object value = ReflectionHelper.GetPropertyValue(person, "Address.City.Region");
            Console.WriteLine(value.ToString());
            
            if(ReflectionHelper.HasProperty(person, "Address.City.Region"))
            {
                Console.WriteLine("has");
            }

            PropertyInfo p = ReflectionHelper.GetProperty(person, "Address.City.Region");
            Console.WriteLine(p);

            Console.ReadLine();
        }
    }
}
