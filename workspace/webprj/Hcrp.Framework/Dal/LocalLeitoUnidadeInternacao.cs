using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace Hcrp.Framework.Dal
{
    public class LocalLeitoUnidadeInternacao : Hcrp.Framework.Classes.LocalLeitoUnidadeInternacao
    {
        public List<Hcrp.Framework.Classes.LocalLeitoUnidadeInternacao> BuscaLocalLeitoUnidadeInternacao(string NumSeqLocalEnf)
        {

            List<Hcrp.Framework.Classes.LocalLeitoUnidadeInternacao> p = new List<Hcrp.Framework.Classes.LocalLeitoUnidadeInternacao>();
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT -1 NUM_SEQ_LOCAL " + Environment.NewLine);
                    sb.Append("       ,'TODOS' NOM_LOCAL " + Environment.NewLine);
                    sb.Append("       ,'AAAAA' ORDENACAO " + Environment.NewLine);
                    sb.Append("   FROM DUAL " + Environment.NewLine);
                    sb.Append(" UNION ALL " + Environment.NewLine);
                    sb.Append(" SELECT LOC.NUM_SEQ_LOCAL " + Environment.NewLine);
                    sb.Append("       ,RPAD(LOC.NOM_LOCAL,60 ,' ') || ' - ' || LOC.SGL_LOCAL NOM_LOCAL " + Environment.NewLine);
                    sb.Append("       ,LOC.NOM_LOCAL ORDENACAO " + Environment.NewLine);
                    sb.Append("   FROM MAPEAMENTO_LOCAL LOC " + Environment.NewLine);
                    sb.Append("  WHERE LOC.NUM_ID_LOCAL = :NUM_ID_LOCAL " + Environment.NewLine);
                    sb.Append("    AND LOC.NUM_SEQ_LOCAL_PAI = :NUM_SEQ_LOCAL_PAI " + Environment.NewLine);
                    sb.Append("  ORDER BY ORDENACAO " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["NUM_ID_LOCAL"] = 13;
                    query.Params["NUM_SEQ_LOCAL_PAI"] = Convert.ToInt32(NumSeqLocalEnf);

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.LocalLeitoUnidadeInternacao a = new Hcrp.Framework.Classes.LocalLeitoUnidadeInternacao();
                        a.Numero = Convert.ToString(dr["NUM_SEQ_LOCAL"]);
                        a.Nome = Convert.ToString(dr["NOM_LOCAL"]);
                        p.Add(a);
                    }
                }
                return p;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
