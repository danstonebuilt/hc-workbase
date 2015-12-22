using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Hcrp.Framework.Infra.Util
{
    public static class DataReader
    {
        /// <summary>
        /// Método utilizado para recuperar o valor passado em um determinado DataReader
        /// </summary>
        /// <typeparam name="T">Tipo do dado a ser retornado</typeparam>
        /// <param name="dr">DataReader contendo as informações</param>
        /// <param name="columnName">Nome da coluna a ser recuperada</param>
        /// <returns>Valor encontrado</returns>
        public static T GetDataValue<T>(IDataReader dtr, string columnName, object pValorPadraoParaNulo = null)
        {
            object result = null;
            object value = null;

            try
            {
                value = dtr[columnName];
            }
            catch
            {
                value = null;
            }

            if ((value == null) || (value == DBNull.Value))
            {
                if (typeof(T) == typeof(DateTime))
                {
                    result = DateTime.MinValue;
                }
                else if ((typeof(T) == typeof(int)) ||
                        (typeof(T) == typeof(Int16)) ||
                        (typeof(T) == typeof(Int32)) ||
                        (typeof(T) == typeof(Int64)) ||
                        (typeof(T) == typeof(long)) ||
                        (typeof(T) == typeof(double)) ||
                        (typeof(T) == typeof(decimal)) ||
                        (typeof(T) == typeof(Single)))
                {
                    result = 0;
                }
                else if (typeof(T) == typeof(bool))
                {
                    result = false;
                }
                else if (typeof(T) == typeof(Guid))
                {
                    result = Guid.NewGuid();
                }
                else if (pValorPadraoParaNulo != null)
                {
                    result = pValorPadraoParaNulo;
                }
                else
                {

                    result = 0;
                }

            }
            else
            {
                if (typeof(T) == typeof(DateTime))
                {
                    result = Convert.ToDateTime(value);
                }
                else if ((typeof(T) == typeof(int)) ||
                        (typeof(T) == typeof(Int16)) ||
                        (typeof(T) == typeof(Single)))
                {
                    result = Convert.ToInt16(value);

                }
                else if ((typeof(T) == typeof(Int32)) ||
                        (typeof(T) == typeof(Int64)) ||
                        (typeof(T) == typeof(long)) ||
                        (typeof(T) == typeof(double)) ||
                        (typeof(T) == typeof(decimal)))
                {
                    result = Convert.ToDouble(value);
                }
                else
                {
                    result = value;
                }
            }

            return (T)DataReader.ChangeType(result, typeof(T));
        }

        /// <summary>
        /// Método utilizado para converter um valor para um tipo definido
        /// </summary>
        /// <param name="value">Valor a ser convertivo</param>
        /// <param name="conversionType">Tipo que o valor será convertido</param>
        /// <returns>Tipo convertido</returns>
        public static object ChangeType(object value, Type conversionType)
        {
            if (conversionType.IsGenericType &&
                conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {

                if (value == null)
                    return null;

                System.ComponentModel.NullableConverter nullableConverter
                    = new System.ComponentModel.NullableConverter(conversionType);

                conversionType = nullableConverter.UnderlyingType;
            }

            return Convert.ChangeType(value, conversionType);
        }


    }
}
