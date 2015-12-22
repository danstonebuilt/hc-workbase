using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace Hcrp.Framework.Dal
{
    class Motivo : Hcrp.Framework.Classes.Motivo
    {
        public Hcrp.Framework.Classes.Motivo BuscarMotivoCodigo(int codMotivo)
        {
            Hcrp.Framework.Classes.Motivo m = new Hcrp.Framework.Classes.Motivo();
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT A.COD_MOTIVO, A.DSC_MOTIVO, A.IDF_ATIVO " + Environment.NewLine);
                    sb.Append(" FROM MOTIVO A " + Environment.NewLine);
                    sb.Append(" WHERE A.COD_MOTIVO = :COD_MOTIVO " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["COD_MOTIVO"] = codMotivo;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        m.Codigo = Convert.ToInt32(dr["COD_MOTIVO"]);
                        m.Ativo = Convert.ToString(dr["IDF_ATIVO"]) == "S";
                        m.Descricao = Convert.ToString(dr["DSC_MOTIVO"]);
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
