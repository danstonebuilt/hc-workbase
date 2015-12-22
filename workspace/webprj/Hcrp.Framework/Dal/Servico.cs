using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace Hcrp.Framework.Dal
{
    class Servico : Hcrp.Framework.Classes.Servico
    {
        public Servico BuscaServicoCodigo(int codServico)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT COD_SERVICO, NOM_SERVICO " + Environment.NewLine);
                    sb.Append(" FROM SERVICO_HC " + Environment.NewLine);
                    sb.Append(" WHERE COD_SERVICO = :COD_SERVICO " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["COD_SERVICO"] = codServico;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        this.Codigo = Convert.ToInt32(dr["COD_SERVICO"]);
                        this.Descricao = Convert.ToString(dr["NOM_SERVICO"]);
                    }

                }
                return this;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
    }
}
