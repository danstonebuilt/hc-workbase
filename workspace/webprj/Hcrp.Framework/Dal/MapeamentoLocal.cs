using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace Hcrp.Framework.Dal
{
    class MapeamentoLocal : Hcrp.Framework.Classes.MapeamentoLocal
    {
        public Hcrp.Framework.Classes.MapeamentoLocal BuscarLocalCodigo(int codLocal)
        {
            Hcrp.Framework.Classes.MapeamentoLocal ml = new Hcrp.Framework.Classes.MapeamentoLocal();
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT A.COD_INSTITUTO, A.NUM_SEQ_LOCAL, A.IDF_ATIVO, A.NOM_LOCAL, A.SGL_LOCAL " + Environment.NewLine);
                    sb.Append(" FROM MAPEAMENTO_LOCAL A " + Environment.NewLine);
                    sb.Append(" WHERE A.NUM_SEQ_LOCAL = :NUM_SEQ_LOCAL " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["NUM_SEQ_LOCAL"] = codLocal;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        ml._CodInstituto = Convert.ToInt32(dr["COD_INSTITUTO"]);
                        ml.Seq = Convert.ToInt32(dr["NUM_SEQ_LOCAL"]);
                        ml.Ativo = Convert.ToString(dr["IDF_ATIVO"])=="S";
                        ml.Nome = Convert.ToString(dr["NOM_LOCAL"]);
                        ml.Sigla = Convert.ToString(dr["SGL_LOCAL"]);
                    }
                }
                return ml;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
