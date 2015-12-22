using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;


namespace Hcrp.Framework.Dal
{
    public class LogSadt : Hcrp.Framework.Classes.LogSadt
    {
        public List<Hcrp.Framework.Classes.LogSadt> BuscaLogs(int seqItemAtendimento)
        {
            List<Hcrp.Framework.Classes.LogSadt> l = new List<Hcrp.Framework.Classes.LogSadt>();
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT A.IDF_OPERACAO, A.DTA_HOR_LOG, A.NUM_USER_LOG, " + Environment.NewLine);
                    sb.Append(" A.DSC_CHAVE, A.NOM_TABELA, A.NOM_COLUNA, A.CTU_ANTERIOR " + Environment.NewLine);
                    sb.Append(" FROM LOG_SADT A " + Environment.NewLine);
                    sb.Append(" WHERE A.NOM_TABELA = 'ITEM_PEDIDO_ATENDIMENTO' " + Environment.NewLine);
                    sb.Append("   AND A.NOM_COLUNA = 'COD_SITUACAO' " + Environment.NewLine);
                    sb.Append("   AND A.DSC_CHAVE = '" + seqItemAtendimento + "' " + Environment.NewLine);
                    sb.Append(" ORDER BY A.DTA_HOR_LOG " + Environment.NewLine);
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["SEQ_ITEM_PEDIDO_ATENDIMENTO"] = seqItemAtendimento;                    

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.LogSadt L = new Hcrp.Framework.Classes.LogSadt();
                        
                        if (dr["CTU_ANTERIOR"] != DBNull.Value)
                            L._CodSituacao = Convert.ToInt32(dr["CTU_ANTERIOR"]);

                        if (dr["NUM_USER_LOG"] != DBNull.Value)
                            L._NumUsuario = Convert.ToInt32(dr["NUM_USER_LOG"]);

                        if (dr["DSC_CHAVE"] != DBNull.Value)
                            L.Chave = Convert.ToString(dr["DSC_CHAVE"]);

                        if (dr["NOM_COLUNA"] != DBNull.Value)
                            L.Coluna = Convert.ToString(dr["NOM_COLUNA"]);

                        if (dr["DTA_HOR_LOG"] != DBNull.Value)
                            L.DataLog = Convert.ToDateTime(dr["DTA_HOR_LOG"]);

                        if (dr["IDF_OPERACAO"] != DBNull.Value)
                            L.Operacao = Convert.ToString(dr["IDF_OPERACAO"]);

                        if (dr["NOM_TABELA"] != DBNull.Value)
                            L.Tabela = Convert.ToString(dr["NOM_TABELA"]);

                        l.Add(L);
                    }
                }
                return l;
            }
            catch (Exception)
            {
                throw;
            }                    
        }
    }
}
