using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Hcrp.Infra.AcessoDado;

namespace Hcrp.CarroUrgenciaPsicoativo.DAL
{
    public class AtendimentoPaciente
    {
        #region Métodos

        /// <summary>
        /// Obter o atendimento da paciente.
        /// </summary>        
        public Int64 ObterAtendimentoDoPacienteParaAData(string codigoPaciente, DateTime dataAtendimento)
        {
            Int64 seqAtendimento = 0;

            try
            {
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    // Query Principal
                    //str.AppendLine(" SELECT AP.SEQ_ATENDIMENTO ");
                    //str.AppendLine("   FROM ATENDIMENTO_PACIENTE AP,  ");
                    //str.AppendLine("        TIPO_ATENDIMENTO_HC TA ");
                    //str.AppendLine(string.Format(" WHERE AP.COD_PACIENTE = '{0}' ", codigoPaciente));
                    //str.AppendLine(string.Format(" AND TO_DATE('{0}', 'DD/MM/YYYY') BETWEEN TRUNC(AP.DTA_HOR_ABERTURA)AND TRUNC(AP.DTA_HOR_FECHAMENTO) ", dataAtendimento.ToString("dd/MM/yyyy")));
                    //str.AppendLine(" AND AP.COD_TIPO_ATENDIMENTO=TA.COD_TIPO_ATENDIMENTO ");
                    //str.AppendLine(" ORDER BY AP.SEQ_ATENDIMENTO DESC ");

                    // select FCN_ATENDIMENTO_PAC_DATA('0377826A',sysdate,'S') from dual

                    str.AppendLine(" SELECT FCN_ATENDIMENTO_PAC_DATA('" + codigoPaciente + "', SYSDATE,'S') SEQ_ATENDIMENTO FROM DUAL  ");

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    // Preparar o retorno
                    while (ctx.Reader.Read())
                    {
                        if (ctx.Reader["SEQ_ATENDIMENTO"] != DBNull.Value)
                            seqAtendimento = Convert.ToInt64(ctx.Reader["SEQ_ATENDIMENTO"]);

                        break;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return seqAtendimento;
        }

        /// <summary>
        /// Obter dados do atendimento por seq atendimento.
        /// </summary>        
        public DataView ObterDadosDoAtendimentoPorSeqAtendimento(Int64 seqAtendimento)
        {
            try
            {
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    // Query Principal
                    str.AppendLine(" SELECT A.SEQ_ATENDIMENTO, ");
                    str.AppendLine(" A.NUM_ATENDIMENTO ||' / '|| A.ANO_ATENDIMENTO AS NUM_ANO_ATENDIMENTO, ");

                    str.AppendLine(" CASE WHEN P.COD_PACIENTE IS NOT NULL THEN P.COD_PACIENTE || ' - ' || P.NOM_PACIENTE ||' '|| P.SBN_PACIENTE ");
                    str.AppendLine(" WHEN PS.COD_PACIENTE_SEM_REGISTRO_HC IS NOT NULL THEN PS.COD_PACIENTE_SEM_REGISTRO_HC || ' - ' || PS.NOM_PACIENTE ||' '|| PS.SBN_PACIENTE ");
                    str.AppendLine(" ELSE '' END AS NOME_PACIENTE, ");

                    str.AppendLine(" P.COD_PACIENTE, ");
                    str.AppendLine(" PS.COD_PACIENTE_SEM_REGISTRO_HC ");

                    str.AppendLine(" FROM ATENDIMENTO_PACIENTE A, ");
                    str.AppendLine(" PACIENTE P, ");
                    str.AppendLine(" PACIENTE_SEM_REGISTRO_HC PS ");
                    str.AppendLine(" WHERE A.COD_PACIENTE = P.COD_PACIENTE(+) AND ");
                    str.AppendLine(" A.COD_PACIENTE_SEM_REGISTRO_HC = PS.COD_PACIENTE_SEM_REGISTRO_HC(+) AND ");

                    str.AppendLine(string.Format(" A.SEQ_ATENDIMENTO = {0} ", seqAtendimento));

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    System.Data.DataTable dt = new System.Data.DataTable();

                    dt.Load(ctx.Reader);

                    return dt.DefaultView;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        #endregion
    }
}
