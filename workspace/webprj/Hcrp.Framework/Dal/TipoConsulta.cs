using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace Hcrp.Framework.Dal
{
    class TipoConsulta : Hcrp.Framework.Classes.TipoConsulta
    {
        public TipoConsulta BuscaTipoConsultaCodigo(int codTipoConsulta)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT COD_TIPO_CONSULTA, DSC_TIPO_CONSULTA " + Environment.NewLine);
                    sb.Append(" FROM TIPO_CONSULTA_SUS " + Environment.NewLine);
                    sb.Append(" WHERE COD_TIPO_CONSULTA = :COD_TIPO_CONSULTA " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["COD_TIPO_CONSULTA"] = codTipoConsulta;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        this.Codigo = Convert.ToInt32(dr["COD_TIPO_CONSULTA"]);
                        this.Descricao = Convert.ToString(dr["DSC_TIPO_CONSULTA"]);
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
