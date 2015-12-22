using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace Hcrp.Framework.Dal
{
    public class TipoProtocoloAtendimento
    {
        public Classes.TipoProtocoloAtendimento BuscarTipoProtocoloAtendimentoCodigo(int codigo)
        {
            Hcrp.Framework.Classes.TipoProtocoloAtendimento tipoProtocoloAtendimento = null;

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT COD_PROTOCOLO_ATENDIMENTO, NOM_PROTOCOLO_ATENDIMENTO, NOM_FORM_SOLICITACAO, NOM_FORM_VISUALIZACAO, IDF_ATIVO " + Environment.NewLine);
                    sb.Append(" FROM TIPO_PROTOCOLO_ATENDIMENTO " + Environment.NewLine);
                    sb.Append(" WHERE COD_PROTOCOLO_ATENDIMENTO = :COD_PROTOCOLO_ATENDIMENTO " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["COD_PROTOCOLO_ATENDIMENTO"] = codigo;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        tipoProtocoloAtendimento = new Classes.TipoProtocoloAtendimento();

                        if (dr["COD_PROTOCOLO_ATENDIMENTO"] != DBNull.Value)
                            tipoProtocoloAtendimento.Seq = Convert.ToInt32(dr["COD_PROTOCOLO_ATENDIMENTO"]);

                        if (dr["NOM_PROTOCOLO_ATENDIMENTO"] != DBNull.Value)
                            tipoProtocoloAtendimento.Nome = Convert.ToString(dr["NOM_PROTOCOLO_ATENDIMENTO"]);

                        if (dr["NOM_FORM_SOLICITACAO"] != DBNull.Value)
                            tipoProtocoloAtendimento.FormularioSolicitacao = Convert.ToString(dr["NOM_FORM_SOLICITACAO"]);

                        if (dr["NOM_FORM_VISUALIZACAO"] != DBNull.Value)
                            tipoProtocoloAtendimento.FormularioVisualizacao = Convert.ToString(dr["NOM_FORM_VISUALIZACAO"]);

                        if (dr["IDF_ATIVO"] != DBNull.Value)
                            tipoProtocoloAtendimento.Ativo = Convert.ToString(dr["IDF_ATIVO"]) == "S";

                        break;
                    }
                }

                return tipoProtocoloAtendimento;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
