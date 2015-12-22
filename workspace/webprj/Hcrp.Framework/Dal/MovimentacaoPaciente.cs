using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hcrp.Infra.AcessoDado;
using Oracle.DataAccess.Client;

namespace Hcrp.Framework.Dal
{
    public class MovimentacaoPaciente
    {
        public Hcrp.Framework.Classes.MovimentacaoPaciente BuscaUltimaMovimentacaoPorAtendimento(Hcrp.Framework.Classes.Atendimento Atendimento)
        {
            Hcrp.Framework.Classes.MovimentacaoPaciente M = new Hcrp.Framework.Classes.MovimentacaoPaciente();

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT * " + Environment.NewLine);
                    sb.Append(" FROM MOVIMENTACAO_PACIENTE MP " + Environment.NewLine);
                    sb.Append(" WHERE MP.SEQ_ATENDIMENTO = :SEQ_ATENDIMENTO" + Environment.NewLine);
                    sb.Append(" AND DTA_HOR_SAIDA IS NULL " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());
                    query.Params["SEQ_ATENDIMENTO"] = Atendimento.SeqAtendimento;

                    ctx.ExecuteQuery(query);

                    // Cria objeto 
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        M.Codigo = Convert.ToInt32(dr["SEQ_MOVIMENTACAO_PACIENTE"]);
                        M._numSeqLocal = Convert.ToInt32(dr["NUM_SEQ_LOCAL"]);
                    }
                }
                return M;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Hcrp.Framework.Classes.MovimentacaoPaciente BuscaMovimentacaoDoAtendimento(Hcrp.Framework.Classes.Atendimento Atendimento)
        {
            Hcrp.Framework.Classes.MovimentacaoPaciente M = new Hcrp.Framework.Classes.MovimentacaoPaciente();

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT * " + Environment.NewLine);
                    sb.Append(" FROM MOVIMENTACAO_PACIENTE MP " + Environment.NewLine);
                    sb.Append(" WHERE MP.SEQ_MOVIMENTACAO_PACIENTE = :SEQ_MOVIMENTACAO_PACIENTE" + Environment.NewLine);                    

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());
                    query.Params["SEQ_MOVIMENTACAO_PACIENTE"] = Atendimento.SeqMovimentacaoPaciente;

                    ctx.ExecuteQuery(query);

                    // Cria objeto 
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        M.Codigo = Convert.ToInt32(dr["SEQ_MOVIMENTACAO_PACIENTE"]);
                        M._numSeqLocal = Convert.ToInt32(dr["NUM_SEQ_LOCAL"]);
                    }
                }
                return M;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
