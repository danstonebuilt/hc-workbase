using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace Hcrp.Framework.Dal
{
    public class AgendaPaciente : Hcrp.Framework.Classes.AgendaPaciente
    {
        public List<Hcrp.Framework.Classes.AgendaPaciente> BuscaAgendaPaciente(String codPaciente)
        {
            
            List<Hcrp.Framework.Classes.AgendaPaciente> l = new List<Hcrp.Framework.Classes.AgendaPaciente>();

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT APS.COD_PACIENTE, " + Environment.NewLine);
                    sb.Append("        APS.DTA_HOR_CONSULTA DTA_HOR_AGENDAMENTO, " + Environment.NewLine);
                    sb.Append("        'CONSULTA' TIPO, " + Environment.NewLine);
                    sb.Append("        APS.COD_ESPECIALIDADE_HC, " + Environment.NewLine);
                    sb.Append("        -1 COD_PROCEDIMENTO_HC, " + Environment.NewLine);
                    sb.Append("        -1 COD_SERVICO,  " + Environment.NewLine);
                    sb.Append("        APS.COD_TIPO_CONSULTA, " + Environment.NewLine);
                    sb.Append("        APS.COD_PROF_SOLICITANTE, " + Environment.NewLine);
                    sb.Append("        ML.COD_INSTITUTO " + Environment.NewLine);
                    sb.Append("   FROM AGENDA_PROCEDIMENTO_SUS APS, " + Environment.NewLine);
                    sb.Append("        MAPEAMENTO_LOCAL ML " + Environment.NewLine);
                    sb.Append(" WHERE APS.DTA_HOR_CONSULTA >= ADD_MONTHS(SYSDATE,-24) " + Environment.NewLine);
                    sb.Append("   AND APS.COD_PACIENTE = :COD_PACIENTE " + Environment.NewLine);
                    sb.Append("   AND APS.NUM_SEQ_LOCAL = ML.NUM_SEQ_LOCAL " + Environment.NewLine);
                    sb.Append(" UNION ALL " + Environment.NewLine);
                    sb.Append(" SELECT PHC.COD_PACIENTE, " + Environment.NewLine);
                    sb.Append("        PEA.DTA_HOR_AGENDAMENTO, " + Environment.NewLine);
                    sb.Append("        'EXAME' TIPO, " + Environment.NewLine);
                    sb.Append("        -1 COD_ESPECIALIDADE_HC, " + Environment.NewLine);
                    sb.Append("        PEI.COD_PROCEDIMENTO_HC, " + Environment.NewLine);
                    sb.Append("        PEI.COD_SERVICO, " + Environment.NewLine);
                    sb.Append("        -1 COD_TIPO_CONSULTA, " + Environment.NewLine);
                    sb.Append("        PHC.COD_PROFISSIONAL_SOLICITANTE COD_PROF_SOLICITANTE, " + Environment.NewLine);
                    sb.Append("        PHC.COD_INSTITUTO " + Environment.NewLine);
                    sb.Append("   FROM PEDIDO_EXAME_HC PHC, " + Environment.NewLine);
                    sb.Append("        PEDIDO_EXAME_ITEM_HC PEI, " + Environment.NewLine);
                    sb.Append("        PEDIDO_EXAME_ITEM_AGENDA_HC PEA " + Environment.NewLine);
                    sb.Append("  WHERE PEI.IDF_SITUACAO IN (0,1) " + Environment.NewLine);
                    sb.Append("    AND PEA.DTA_HOR_AGENDAMENTO >= ADD_MONTHS(SYSDATE,-24) " + Environment.NewLine);
                    sb.Append("    AND PHC.COD_PACIENTE = :COD_PACIENTE " + Environment.NewLine);
                    sb.Append("    AND PHC.NUM_PEDIDO = PEI.NUM_PEDIDO " + Environment.NewLine);
                    sb.Append("    AND PEI.NUM_PEDIDO_EXAME_ITEM = PEA.NUM_PEDIDO_EXAME_ITEM(+) " + Environment.NewLine);
                    sb.Append(" ORDER BY 2 DESC " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["COD_PACIENTE"] = codPaciente;
                    query.Params["COD_PACIENTE"] = codPaciente;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.AgendaPaciente a = new Hcrp.Framework.Classes.AgendaPaciente();
                        a._CodPaciente = Convert.ToString(dr["COD_PACIENTE"]);
                        if (dr["DTA_HOR_AGENDAMENTO"] != DBNull.Value)
                            a.DataAgendamento = Convert.ToDateTime(dr["DTA_HOR_AGENDAMENTO"]);
                        if (dr["COD_ESPECIALIDADE_HC"] != DBNull.Value)
                            a._CodEspecialidade = Convert.ToInt32(dr["COD_ESPECIALIDADE_HC"]);
                        if (dr["COD_PROCEDIMENTO_HC"] != DBNull.Value)
                            a._CodProcedimentoHc = Convert.ToInt32(dr["COD_PROCEDIMENTO_HC"]);
                        if (dr["COD_SERVICO"] != DBNull.Value)
                            a._CodServico = Convert.ToInt32(dr["COD_SERVICO"]);
                        if (dr["COD_TIPO_CONSULTA"] != DBNull.Value)
                            a._CodTipoConsulta = Convert.ToInt32(dr["COD_TIPO_CONSULTA"]);
                        if (dr["COD_PROF_SOLICITANTE"] != DBNull.Value)
                            a._CodProfissional = Convert.ToInt32(dr["COD_PROF_SOLICITANTE"]);
                        if (dr["COD_INSTITUTO"] != DBNull.Value)
                            a._CodInstituto = Convert.ToInt32(dr["COD_INSTITUTO"]);
                        a.TipoAgendamento = Convert.ToString(dr["TIPO"]);
                        l.Add(a);
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
