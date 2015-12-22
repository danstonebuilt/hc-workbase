using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Dal
{
    public class ServicoSadt : Hcrp.Framework.Classes.ServicoSadt
    {
        public Hcrp.Framework.Classes.ServicoSadt BuscaServicoSadtPedAtendimento(int seqPedidoAtendimento)
        {
            using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
            {
                ctx.Open();

                StringBuilder sb = new StringBuilder();

                sb.Append(" SELECT A.COD_SERVICO_SADT, A.DSC_SERVICO_SADT, A.IDF_SISTEMA_AGENDAMENTO, A.IDF_ATIVO " + Environment.NewLine);
                sb.Append(" FROM SADT_SERVICO A, PEDIDO_ATENDIMENTO B " + Environment.NewLine);
                sb.Append(" WHERE A.COD_SERVICO_SADT = B.COD_SERVICO_SADT " + Environment.NewLine);
                sb.Append("   AND B.SEQ_PEDIDO_ATENDIMENTO = :SEQ_PEDIDO_ATENDIMENTO " + Environment.NewLine);

                Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                query.Params["SEQ_PEDIDO_ATENDIMENTO"] = seqPedidoAtendimento;
                ctx.ExecuteQuery(query);

                while (ctx.Reader.Read())
                {
                    if (ctx.Reader["COD_SERVICO_SADT"] != DBNull.Value)
                    {
                        if (ctx.Reader["COD_SERVICO_SADT"] != DBNull.Value)
                            this.Codigo = Convert.ToInt32(ctx.Reader["COD_SERVICO_SADT"]);

                        if (ctx.Reader["DSC_SERVICO_SADT"] != DBNull.Value)
                            this.Descricao = Convert.ToString(ctx.Reader["DSC_SERVICO_SADT"]);

                        if (ctx.Reader["IDF_ATIVO"] != DBNull.Value)
                            this.Ativo = Convert.ToString(ctx.Reader["IDF_ATIVO"]) == "S";

                        if (ctx.Reader["IDF_SISTEMA_AGENDAMENTO"] != DBNull.Value)
                            this.SistemaAgendamento = (ESistemaAgendamento)Convert.ToInt32(ctx.Reader["IDF_SISTEMA_AGENDAMENTO"]);
                    }

                    break;
                }
            }
            return this;
        }

        public Hcrp.Framework.Classes.ServicoSadt BuscaServicoSadtCodigo(int codServico)
        {
            using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
            {
                ctx.Open();

                StringBuilder sb = new StringBuilder();

                sb.Append(" SELECT A.COD_SERVICO_SADT, A.DSC_SERVICO_SADT, A.IDF_SISTEMA_AGENDAMENTO, A.IDF_ATIVO " + Environment.NewLine);
                sb.Append(" FROM SADT_SERVICO A " + Environment.NewLine);
                sb.Append(" WHERE A.COD_SERVICO_SADT = :COD_SERVICO_SADT " + Environment.NewLine);

                Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                query.Params["COD_SERVICO_SADT"] = codServico;
                ctx.ExecuteQuery(query);

                while (ctx.Reader.Read())
                {
                    if (ctx.Reader["COD_SERVICO_SADT"] != DBNull.Value)
                    {
                        this.Codigo = Convert.ToInt32(ctx.Reader["COD_SERVICO_SADT"]);
                        this.Descricao = Convert.ToString(ctx.Reader["DSC_SERVICO_SADT"]);
                        this.Ativo = Convert.ToString(ctx.Reader["IDF_ATIVO"])=="S";
                        this.SistemaAgendamento = (ESistemaAgendamento)Convert.ToInt32(ctx.Reader["IDF_SISTEMA_AGENDAMENTO"]);
                    }
                }
            }
            return this;        
        }

        public List<Hcrp.Framework.Classes.ServicoSadt> BuscarServicoSadt()
        {
            List<Hcrp.Framework.Classes.ServicoSadt> l = new List<Hcrp.Framework.Classes.ServicoSadt>();

            using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
            {
                ctx.Open();

                StringBuilder sb = new StringBuilder();

                sb.Append(" SELECT A.COD_SERVICO_SADT, A.DSC_SERVICO_SADT, A.IDF_SISTEMA_AGENDAMENTO, A.IDF_ATIVO " + Environment.NewLine);
                sb.Append(" FROM SADT_SERVICO A " + Environment.NewLine);
                sb.Append(" ORDER BY A.DSC_SERVICO_SADT " + Environment.NewLine);

                Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                ctx.ExecuteQuery(query);

                while (ctx.Reader.Read())
                {
                    if (ctx.Reader["COD_SERVICO_SADT"] != DBNull.Value)
                    {
                        Hcrp.Framework.Classes.ServicoSadt s = new Hcrp.Framework.Classes.ServicoSadt();
                        s.Codigo = Convert.ToInt32(ctx.Reader["COD_SERVICO_SADT"]);
                        s.Descricao = Convert.ToString(ctx.Reader["DSC_SERVICO_SADT"]);
                        s.Ativo = Convert.ToString(ctx.Reader["IDF_ATIVO"]) == "S";
                        s.SistemaAgendamento = (ESistemaAgendamento)Convert.ToInt32(ctx.Reader["IDF_SISTEMA_AGENDAMENTO"]);
                        l.Add(s);
                    }
                }
            }
            return l;
        }
    
    }
}
