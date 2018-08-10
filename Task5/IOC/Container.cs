using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Task5.IOC
{
    class Container
    {
        public void AddType(Type type)
        {
            if (TypeCatalog != null && TypeCatalog.Contains(type))
            {
                throw new ArgumentException("Container already contain this type");
            }
            TypeCatalog.Add(type);
        }

        public void AddType(Type type, Type baseType)
        {
            bool isValid = false;

            if (type.GetInterfaces().Contains(baseType) || (type.BaseType == baseType && baseType.IsAbstract))
            {
                isValid = true;
            }
            if (isValid)
            {
                TypeCatalog.Add(baseType);
                derivedTypesCatalog.Add(type);
            }
        }
        
        public object CreateInstance(Type type)
        {
            if (type.GetCustomAttribute<ImportConstructor>() != null)
            {
                // Возвращает конструктор, который содержит список параметоров,
                // из всех зависимых типо, либо выдает исключение исключение.
                ConstructorInfo constructor = type.GetConstructors()
                    .Where(ci =>
                    {
                        int dependentTypecQuantity = ci.GetParameters().Aggregate(0, (counter, param) =>
                        {
                            if (param.ParameterType != type && 
                            (TypeCatalog.Contains(param.ParameterType) || derivedTypesCatalog.Contains(param.ParameterType)))
                            {
                                counter++;
                            }
                            return counter;
                        });

                        if (dependentTypecQuantity == TypeCatalog.Count - 1)
                        {
                            return true;
                        }
                        return false;
                    }).First();

                List<object> parameters = new List<object>();

                foreach (var param in constructor.GetParameters())
                {
                    Type parameterType = null;
                    // Если тип параметра конструктора экспортирует базовый интерфейс.
                    parameterType = derivedTypesCatalog.FirstOrDefault(t => t.GetCustomAttribute<ExportAttribute>().ContractType == param.ParameterType);
                    // Подходящий параметр найден - создаем его экземпляр, 
                    // добавляем в список параметров конструктора, и переходим к следующему параметру.
                    if (parameterType != null)
                    {
                        parameters.Add(parameterType.GetConstructor(Type.EmptyTypes).Invoke(null));
                        continue;
                    }
                    // Если тип параметра конструктора экспортирует собственный класс.
                    parameterType = TypeCatalog.FirstOrDefault(t =>
                    {
                        // Один из типов в TypeCatalog - всегда будет класс, экземляр которого требуется создать.
                        if (t == type)
                        {
                            return false;
                        }
                        Type contractType = t.GetCustomAttribute<ExportAttribute>().ContractType;
                        // contractType == null - для тех случаев, когда класс помечен атрибутом Export
                        // конструктором по умолчанию.
                        return ((contractType == param.ParameterType) || contractType == null);
                    });
                    if (parameterType != null)
                    {
                        parameters.Add(parameterType.GetConstructor(Type.EmptyTypes).Invoke(null));
                    }
                }

                return constructor.Invoke(parameters.ToArray());
            }

            ConstructorInfo defaultConstructor = type.GetConstructor(Type.EmptyTypes);
            if(defaultConstructor == null)
            {
                throw new NullReferenceException();
            }
            object obj = defaultConstructor.Invoke(null);

            var properties = type.GetProperties().Where(x => x.GetCustomAttribute<ImportAttribute>() != null);

            foreach (var prop in properties)
            {
                Type propertyType = null;

                // Если тип свойства экспортирует базовый интерфейс.
                propertyType = derivedTypesCatalog.FirstOrDefault(t => t.GetCustomAttribute<ExportAttribute>().ContractType == prop.PropertyType);
                if (propertyType != null)
                {
                    prop.SetValue(obj, propertyType.GetConstructor(Type.EmptyTypes).Invoke(Type.EmptyTypes));
                    continue;
                }
                // Если тип свойства экспортирует свой класс.
                propertyType = TypeCatalog.FirstOrDefault(t =>
                {
                    if(t == type) { return false; }
                    var contractType = t.GetCustomAttribute<ExportAttribute>().ContractType;
                    return ((contractType == prop.PropertyType) || contractType == null);
                });
                if (propertyType != null)
                {
                    prop.SetValue(obj, propertyType.GetConstructor(Type.EmptyTypes).Invoke(null));
                }
            }



            
            // для тех типов, котороые сами предоставляют свой тип.
            



            return obj;

        }
        public T CreateInstance<T>()
        {
            return default(T);
        }        

        public List<Type> TypeCatalog
        {
            get
            {
                if (typesCatalog == null)
                {
                    typesCatalog = new List<Type>();
                }
                return typesCatalog;
            }
            private set
            {
                typesCatalog = value;
            }
        }

        private List<Type> derivedTypesCatalog = new List<Type>();

        private List<Type> typesCatalog = new List<Type>();        
    }
}
