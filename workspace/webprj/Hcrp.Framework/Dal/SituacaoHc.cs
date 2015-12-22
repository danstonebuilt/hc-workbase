using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace Hcrp.Framework.Dal
{
    public class SituacaoHc : Hcrp.Framework.Classes.SituacaoHc
    {
        public SituacaoHc BuscarSituacaoCodigo(Int32 codSituacao)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT COD_SITUACAO, NOM_SITUACAO " + Environment.NewLine);
                    sb.Append(" FROM TIPO_SITUACAO_HC " + Environment.NewLine);
                    sb.Append(" WHERE COD_SITUACAO = :COD_SITUACAO " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["COD_SITUACAO"] = codSituacao;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        this.CodSituacao = Convert.ToInt32(dr["COD_SITUACAO"]);
                        this.DescricaoSituacao = Convert.ToString(dr["NOM_SITUACAO"]);
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
