using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace Hcrp.Framework.Dal
{
    public class TipoAtendimento
    {
        public Hcrp.Framework.Classes.TipoAtendimento BuscarTipoAtendimentoCodigo(int codTipoAtendimento)
        {

            Hcrp.Framework.Classes.TipoAtendimento TA = new Hcrp.Framework.Classes.TipoAtendimento();

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT COD_TIPO_ATENDIMENTO, NOM_TIPO_ATENDIMENTO, NOM_TIPO_ABREVIADO " + Environment.NewLine);
                    sb.Append(" FROM TIPO_ATENDIMENTO_HC " + Environment.NewLine);
                    sb.Append(" WHERE COD_TIPO_ATENDIMENTO = :COD_TIPO_ATENDIMENTO " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["COD_TIPO_ATENDIMENTO"] = codTipoAtendimento;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        TA.Codigo = Convert.ToInt32(dr["COD_TIPO_ATENDIMENTO"]);
                        TA.Nome = Convert.ToString(dr["NOM_TIPO_ATENDIMENTO"]);
                        TA.NomeAbreviado = Convert.ToString(dr["NOM_TIPO_ABREVIADO"]);
                    }

                }
                return TA;
            }
            catch (Exception)
            {
                throw;
            }
        }

        
        /// <summary>
        /// Método para popular os objetos DropDownList
        /// </summary>
        /// <param name="pCodInstituto">Código do Instituto</param>        
        /// <returns></returns>
        public static List<Hcrp.Framework.Entity.TipoAtendimento> getTipoAtendimentoDDL(int pCodInstituto)
        {

            List<Hcrp.Framework.Entity.TipoAtendimento> result = new List<Hcrp.Framework.Entity.TipoAtendimento>();

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();
                    StringBuilder sb = new StringBuilder();                    
                    sb.Append(" SELECT TA.COD_TIPO_ATENDIMENTO, TA.NOM_TIPO_ATENDIMENTO , TA.NOM_TIPO_ABREVIADO " + Environment.NewLine);
                    sb.Append(" FROM TIPO_ATENDIMENTO_HC TA " + Environment.NewLine);
                    sb.Append(" WHERE (1 = 1) " + Environment.NewLine); 

                    if (!pCodInstituto.Equals(0))
                    {
                        sb.Append(" AND EXISTS (SELECT ITA.COD_TIPO_ATENDIMENTO FROM INSTITUTO_TIPO_ATENDIMENTO ITA WHERE ITA.COD_INSTITUTO = :COD_INSTITUTO AND ITA.COD_TIPO_ATENDIMENTO = TA.COD_TIPO_ATENDIMENTO ) ");
                    }

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    if (!pCodInstituto.Equals(0))
                    {
                        query.Params["COD_INSTITUTO"] = pCodInstituto;
                    }
                    
                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;


                    while (dr.Read())
                    {
                        result.Add(new Entity.TipoAtendimento(
                            Hcrp.Framework.Infra.Util.DataReader.GetDataValue<long>(dr, "COD_TIPO_ATENDIMENTO",0),
                            Hcrp.Framework.Infra.Util.DataReader.GetDataValue<string>(dr, "NOM_TIPO_ATENDIMENTO",""),
                            Hcrp.Framework.Infra.Util.DataReader.GetDataValue<string>(dr, "NOM_TIPO_ABREVIADO","")));                            
                    }

                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
