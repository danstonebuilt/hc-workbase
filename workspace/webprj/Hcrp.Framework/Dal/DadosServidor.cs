using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace Hcrp.Framework.Dal
{
    public class DadosServidor
    {
        public static DateTime BuscaDataServidor()
        {
            DateTime data = new DateTime();
            try
            {                
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    string str = " SELECT SYSDATE AS DATA FROM DUAL ";
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(str);
                    ctx.ExecuteQuery(str);

                    // Cria objeto 
                    OracleDataReader dr = ctx.Reader as OracleDataReader;
                    while (dr.Read())
                    {
                        data = Convert.ToDateTime(dr["DATA"]);
                    }
                }
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
