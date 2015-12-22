using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;

namespace Hcrp.CarroUrgenciaPsicoativo.BLL.FrameworkAplicacao
{
    public class EnumUtil
    {
        public string Key { get; set; }

        public string Name { get; set; }

        public static List<EnumUtil> ListFrom<T>()
        {
            var array = (T[])(Enum.GetValues(typeof(T)).Cast<T>());

            return array
              .Select(a => new EnumUtil
              {
                  Key = enumValueOf(a.ToString(), typeof(T)).ToString(),
                  Name = DescricaoEnum((Enum)Enum.Parse(typeof(T), a.ToString()))
              })
                .OrderBy(kvp => kvp.Name)
               .ToList();
        }


        public static string DescricaoEnum(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }

        public static string stringValueOf(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return value.ToString();
        }

        public static object enumValueOf(string value, Type enumType)
        {
            string[] names = Enum.GetNames(enumType);

            foreach (string name in names)
            {
                if (stringValueOf((Enum)Enum.Parse(enumType, name)).Equals(value))
                {
                    try
                    {
                        return (int)Enum.Parse(enumType, name);
                    }
                    catch (Exception)
                    {
                        return (char)Enum.Parse(enumType, name);
                    }
                }
            }

            return "";
        }
    }
}
