using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace Hcrp.Framework.Dal
{
    public class MotivoEncaminhamento
    {
        public Hcrp.Framework.Classes.MotivoEncaminhamento BuscarMotivoEncaminhamentoCodigo(int codMotivoEncaminhamento)
        {
            Hcrp.Framework.Classes.MotivoEncaminhamento m = null;
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT A.COD_MOTIVO_ENCAMINHAMENTO, A.DSC_MOTIVO_ENCAMINHAMENTO, A.IDF_ATIVO " + Environment.NewLine);
                    sb.Append(" FROM MOTIVO_ENCAMINHAMENTO A " + Environment.NewLine);
                    sb.Append(" WHERE A.COD_MOTIVO_ENCAMINHAMENTO = :COD_MOTIVO_ENCAMINHAMENTO " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["COD_MOTIVO_ENCAMINHAMENTO"] = codMotivoEncaminhamento;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        m = new Hcrp.Framework.Classes.MotivoEncaminhamento(); ;

                        if (dr["COD_MOTIVO_ENCAMINHAMENTO"] != DBNull.Value)
                            m.Codigo = Convert.ToInt32(dr["COD_MOTIVO_ENCAMINHAMENTO"]);

                        if (dr["IDF_ATIVO"] != DBNull.Value)
                            m.Ativo = Convert.ToString(dr["IDF_ATIVO"]) == "S";

                        if (dr["DSC_MOTIVO_ENCAMINHAMENTO"] != DBNull.Value)
                            m.Descricao = Convert.ToString(dr["DSC_MOTIVO_ENCAMINHAMENTO"]);

                        break;
                    }
                }

                return m;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
