using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace Hcrp.Framework.Dal
{
    public class ParametroMonitorizacao
    {
        public Classes.ParametroMonitorizacao BuscarParametroMonitorizacaoCodigo(int codParametroMonitorizacao)
        {
            try
            {
                Classes.ParametroMonitorizacao PM = new Classes.ParametroMonitorizacao();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT " + Environment.NewLine);
                    sb.Append("      SEQ_PARAMETRO_MONITORIZACAO, " + Environment.NewLine);
                    sb.Append("      VLRINI_PARAMETRO_MONITORIZACAO,  " + Environment.NewLine);
                    sb.Append("      VLRFIM_PARAMETRO_MONITORIZACAO, " + Environment.NewLine);
                    sb.Append("      DSC_PARAMETRO_MONITORIZACAO, " + Environment.NewLine);
                    sb.Append("      IDF_ATIVO " + Environment.NewLine);
                    sb.Append("FROM PARAMETRO_MONITORIZACAO " + Environment.NewLine);
                    sb.Append(" WHERE SEQ_PARAMETRO_MONITORIZACAO = :SEQ_PARAMETRO_MONITORIZACAO " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["SEQ_PARAMETRO_MONITORIZACAO"] = codParametroMonitorizacao;

                    ctx.ExecuteQuery(query);

                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        PM.Codigo = Convert.ToInt32(dr["SEQ_PARAMETRO_MONITORIZACAO"]);
                        PM.Descricao = Convert.ToString(dr["DSC_PARAMETRO_MONITORIZACAO"]);
                        PM.ValorMinimo = Convert.ToDouble(dr["VLRINI_PARAMETRO_MONITORIZACAO"]);
                        PM.ValorMaximo = Convert.ToDouble(dr["VLRFIM_PARAMETRO_MONITORIZACAO"]);
                        PM.Ativo = Convert.ToString(dr["IDF_ATIVO"]) == "S";
                    }

                }
                return PM;
            }
            catch (Exception)
            {
                throw;
            }            
        }
    }
}
