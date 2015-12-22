using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace Hcrp.Framework.Dal
{
    public class Atendimento
    {
        public /*List<Hcrp.Framework.Classes.Atendimento>*/ Hcrp.Framework.Classes.Atendimento BuscaAtendimentoEmAberto(Hcrp.Framework.Classes.Paciente paciente)
        {
            /*List<Hcrp.Framework.Classes.Atendimento> l = new List<Hcrp.Framework.Classes.Atendimento>();*/
            Hcrp.Framework.Classes.Atendimento l = new Hcrp.Framework.Classes.Atendimento();
            
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT * " + Environment.NewLine);
                    sb.Append(" FROM PACIENTE_EM_ATENDIMENTO AP " + Environment.NewLine);
                    sb.Append(" WHERE AP.COD_PACIENTE = :COD_PACIENTE" + Environment.NewLine);
                    
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());
                    query.Params["COD_PACIENTE"] = paciente.RegistroPaciente;

                    ctx.ExecuteQuery(query);

                    // Cria objeto 
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        //Hcrp.Framework.Classes.Atendimento A = new Hcrp.Framework.Classes.Atendimento();
                        l.DataAberturaAtendimento = Convert.ToDateTime(dr["DTA_HOR_ABERTURA"]);
                        l.DataFechamentoAtendimento = Convert.ToDateTime(dr["DTA_HOR_FECHAMENTO"]);
                        l.SeqAtendimento = Convert.ToInt32(dr["SEQ_ATENDIMENTO"]);
                        l._codTipoAtendimento = Convert.ToInt32(dr["COD_TIPO_ATENDIMENTO"]);

                        if (dr["COD_ESPECIALIDADE_HC"] != DBNull.Value)                        
                            l._codEspecialidade = Convert.ToInt32(dr["COD_ESPECIALIDADE_HC"]);

                        //l.Add(A);
                    }
                }
                return l;
            }
            catch (Exception)
            {
                throw;
            }             
        }


        public Hcrp.Framework.Classes.Atendimento BuscaAtendimentoPorCodigo(int seqAtendimento)
        {
            Hcrp.Framework.Classes.Atendimento l = new Hcrp.Framework.Classes.Atendimento();

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT * " + Environment.NewLine);
                    sb.Append(" FROM ATENDIMENTO_PACIENTE PA " + Environment.NewLine);
                    sb.Append(" WHERE PA.SEQ_ATENDIMENTO = :SEQ_ATENDIMENTO" + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());
                    query.Params["SEQ_ATENDIMENTO"] = seqAtendimento;

                    ctx.ExecuteQuery(query);

                    // Cria objeto 
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        l.DataAberturaAtendimento = Convert.ToDateTime(dr["DTA_HOR_ABERTURA"]);
                        l.DataFechamentoAtendimento = Convert.ToDateTime(dr["DTA_HOR_FECHAMENTO"]);
                        l.SeqAtendimento = Convert.ToInt32(dr["SEQ_ATENDIMENTO"]);
                        l._codTipoAtendimento = Convert.ToInt32(dr["COD_TIPO_ATENDIMENTO"]);

                        if (dr["COD_ESPECIALIDADE_HC"] != DBNull.Value)
                            l._codEspecialidade = Convert.ToInt32(dr["COD_ESPECIALIDADE_HC"]);

                        if (dr["SEQ_MOVIMENTACAO_PACIENTE"] != DBNull.Value)
                            l.SeqMovimentacaoPaciente = Convert.ToInt32(dr["SEQ_MOVIMENTACAO_PACIENTE"]);
                    }
                }
                return l;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
