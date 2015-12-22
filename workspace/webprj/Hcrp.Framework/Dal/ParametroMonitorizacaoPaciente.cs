using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;


namespace Hcrp.Framework.Dal
{
    public class ParametroMonitorizacaoPaciente
    {
        public List<Classes.ParametroMonitorizacaoPaciente> BuscaMonitorizacaoPaciente(Classes.Paciente paciente, Classes.Atendimento atendimento, Classes.ParametroMonitorizacao parametroMonitorizacao, DateTime dataInicial, DateTime dataFinal)
        {
            List<Hcrp.Framework.Classes.ParametroMonitorizacaoPaciente> l = new List<Hcrp.Framework.Classes.ParametroMonitorizacaoPaciente>();

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT " + Environment.NewLine);
                    sb.Append("      PMP.SEQ_PARAM_MONIT_PACIENTE, " + Environment.NewLine);
                    sb.Append("      PMP.SEQ_PARAMETRO_MONITORIZACAO, " + Environment.NewLine);
                    sb.Append("      PMP.VLR_PARAMETRO_MONITORIZACAO, " + Environment.NewLine);
                    sb.Append("      PMP.DAT_HOR_PAR_MON_PACIENTE " + Environment.NewLine);
                    sb.Append(" FROM PARAMETRO_MONIT_PACIENTE PMP, " + Environment.NewLine);
                    sb.Append("      PARAMETRO_MONITORIZACAO PM, " + Environment.NewLine);
                    sb.Append("      ATENDIMENTO_PACIENTE AP " + Environment.NewLine);
                    sb.Append(" WHERE  " + Environment.NewLine);
                    sb.Append("      PMP.SEQ_PARAMETRO_MONITORIZACAO = PM.SEQ_PARAMETRO_MONITORIZACAO " + Environment.NewLine);
                    sb.Append("      AND PMP.SEQ_ATENDIMENTO = AP.SEQ_ATENDIMENTO " + Environment.NewLine);
                    if (!paciente.Equals(default(Paciente)))
                    {
                        sb.Append("      AND AP.COD_PACIENTE = :COD_PACIENTE " + Environment.NewLine);
                    }
                    if (!atendimento.Equals(default(Atendimento)))
                    {
                        sb.Append("      AND AP.SEQ_ATENDIMENTO = :SEQ_ATENDIMENTO " + Environment.NewLine);
                    }
                    if (!parametroMonitorizacao.Equals(default(ParametroMonitorizacao)))
                    {
                        sb.Append("      AND PM.SEQ_PARAMETRO_MONITORIZACAO = :SEQ_PARAMETRO_MONITORIZACAO " + Environment.NewLine);
                    }
                    if (!dataInicial.Equals(default(DateTime)))
                    {
                        sb.Append("      AND PMP.DAT_HOR_PAR_MON_PACIENTE >= :DAT_HOR_PAR_MON_PACIENTE_INI " + Environment.NewLine);
                    }
                    if (!dataFinal.Equals(default(DateTime)))
                    {
                        sb.Append("      AND PMP.DAT_HOR_PAR_MON_PACIENTE <= :DAT_HOR_PAR_MON_PACIENTE_FIM " + Environment.NewLine);
                    }
                    sb.Append("      ORDER BY PMP.SEQ_PARAM_MONIT_PACIENTE ASC " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    if (!paciente.Equals(default(Paciente)))
                    {
                        query.Params["COD_PACIENTE"] = paciente.RegistroPaciente;
                    }
                    if (!atendimento.Equals(default(Atendimento)))
                    {
                        query.Params["SEQ_ATENDIMENTO"] = atendimento.SeqAtendimento;
                    }
                    if (!parametroMonitorizacao.Equals(default(ParametroMonitorizacao)))
                    {
                        query.Params["SEQ_PARAMETRO_MONITORIZACAO"] = parametroMonitorizacao.Codigo;
                    }
                    if (!dataInicial.Equals(default(DateTime)))
                    {
                        query.Params["DAT_HOR_PAR_MON_PACIENTE_INI"] = dataInicial;
                    }
                    if (!dataFinal.Equals(default(DateTime)))
                    {
                        query.Params["DAT_HOR_PAR_MON_PACIENTE_FIM"] = dataFinal;
                    }

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.ParametroMonitorizacaoPaciente pm = new Hcrp.Framework.Classes.ParametroMonitorizacaoPaciente();
                        pm.Codigo = Convert.ToInt32(dr["SEQ_PARAM_MONIT_PACIENTE"]);
                        pm._seqParametroMonitorizacao = Convert.ToInt32(dr["SEQ_PARAMETRO_MONITORIZACAO"]);
                        pm.valor = Convert.ToDouble(dr["VLR_PARAMETRO_MONITORIZACAO"]);
                        pm.DataHora = Convert.ToDateTime(dr["DAT_HOR_PAR_MON_PACIENTE"]);
                        l.Add(pm);
                    }
                }
                return l;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        
        public List<Classes.ParametroMonitorizacaoPaciente> BuscaMonitorizacaoPacienteRecente(Classes.Paciente paciente)
        {
            List<Hcrp.Framework.Classes.ParametroMonitorizacaoPaciente> l = new List<Hcrp.Framework.Classes.ParametroMonitorizacaoPaciente>();

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT " + Environment.NewLine);
                    sb.Append("       PMP.SEQ_PARAM_MONIT_PACIENTE, " + Environment.NewLine);
                    sb.Append("       PMP.SEQ_PARAMETRO_MONITORIZACAO, " + Environment.NewLine);
                    sb.Append("       PMP.VLR_PARAMETRO_MONITORIZACAO, " + Environment.NewLine);
                    sb.Append("       PMP.DAT_HOR_PAR_MON_PACIENTE " + Environment.NewLine);
                    sb.Append(" FROM PARAMETRO_MONIT_PACIENTE PMP, " + Environment.NewLine);
                    sb.Append("    ( " + Environment.NewLine);
                    sb.Append("    SELECT A.SEQ_PARAMETRO_MONITORIZACAO, MAX(A.DAT_HOR_PAR_MON_PACIENTE) AS DATA " + Environment.NewLine);
                    sb.Append("    FROM  PARAMETRO_MONIT_PACIENTE A, ATENDIMENTO_PACIENTE B " + Environment.NewLine);
                    sb.Append("    WHERE A.SEQ_ATENDIMENTO = B.SEQ_ATENDIMENTO " + Environment.NewLine);
                    sb.Append("          AND B.COD_PACIENTE = :COD_PACIENTE " + Environment.NewLine);
                    sb.Append("    GROUP BY A.SEQ_PARAMETRO_MONITORIZACAO " + Environment.NewLine);
                    sb.Append("    ) X, " + Environment.NewLine);
                    sb.Append("    ATENDIMENTO_PACIENTE B " + Environment.NewLine);
                    sb.Append(" WHERE  " + Environment.NewLine);
                    sb.Append("    X.SEQ_PARAMETRO_MONITORIZACAO = PMP.SEQ_PARAMETRO_MONITORIZACAO " + Environment.NewLine);
                    sb.Append("    AND PMP.SEQ_ATENDIMENTO = B.SEQ_ATENDIMENTO " + Environment.NewLine);
                    sb.Append("    AND B.COD_PACIENTE = :COD_PACIENTE " + Environment.NewLine);
                    sb.Append("    AND X.DATA = PMP.DAT_HOR_PAR_MON_PACIENTE " + Environment.NewLine);
                
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["COD_PACIENTE"] = paciente.RegistroPaciente;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.ParametroMonitorizacaoPaciente pm = new Hcrp.Framework.Classes.ParametroMonitorizacaoPaciente();
                        pm.Codigo = Convert.ToInt32(dr["SEQ_PARAM_MONIT_PACIENTE"]);
                        pm._seqParametroMonitorizacao = Convert.ToInt32(dr["SEQ_PARAMETRO_MONITORIZACAO"]);
                        pm.valor = Convert.ToDouble(dr["VLR_PARAMETRO_MONITORIZACAO"]);
                        pm.DataHora = Convert.ToDateTime(dr["DAT_HOR_PAR_MON_PACIENTE"]);
                        l.Add(pm);
                    }
                }
                return l;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public int IncluirParametroMonitorizacaoPaciente(Hcrp.Framework.Classes.ParametroMonitorizacaoPaciente pmp)
        {
            try
            {
                int retorno = 0;
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abrir conexão
                    ctx.Open();

                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.CommandConfig insert = new Hcrp.Infra.AcessoDado.CommandConfig("PARAMETRO_MONIT_PACIENTE");
                    insert.Params["NUM_SEQ_LOCAL"] = pmp._numSeqLocal;
                    insert.Params["SEQ_PARAMETRO_MONITORIZACAO"] = pmp._seqParametroMonitorizacao;
                    insert.Params["SEQ_ATENDIMENTO"] = pmp._seqAtendimento;
                    insert.Params["NUM_USER_BANCO"] = pmp._numUserBanco;
                    insert.Params["VLR_PARAMETRO_MONITORIZACAO"] = pmp.valor;
                    insert.Params["DAT_HOR_PAR_MON_PACIENTE"] = pmp.DataHora;                    
                    
                    // Executar o insert
                    ctx.ExecuteInsert(insert);

                    // Pegar o último ID
                    retorno = Convert.ToInt32(ctx.GetSequenceValue("GENERICO.SEQ_PARAM_MONIT_PACIENTE", false));

                }
                return retorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Realiza a busca dos valores padroes de monitorizacao, sendo eles: valor minimo, valor maximo e valor atual.
        /// </summary>
        /// <param name="codigoAtendimento">Codigo do atendimento para identificacao dos valores padroes</param>
        /// <returns>Lista com valores padroes de monitorizacao</returns>
        public List<Classes.ParametroMonitorizacaoPaciente> BuscarMonitorizacaoPadrao(long codigoAtendimento)
        {
            List<Classes.ParametroMonitorizacaoPaciente> result = new List<Classes.ParametroMonitorizacaoPaciente>();

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine("SELECT ");
                    sb.AppendLine("    X.SEQ_PARAMETRO_MONITORIZACAO, ");
                    sb.AppendLine("    X.DSC_PARAMETRO_MONITORIZACAO, ");
                    sb.AppendLine("    X.VLR_MIN, ");
                    sb.AppendLine("    X.VLR_MAX, ");
                    sb.AppendLine("    Y.VLR_PARAMETRO_MONITORIZACAO AS VLR_ATUAL ");
                    sb.AppendLine("FROM ");
                    sb.AppendLine("    ( ");
                    sb.AppendLine("        SELECT DISTINCT ");
                    sb.AppendLine("            PMP.SEQ_ATENDIMENTO, ");
                    sb.AppendLine("            PMP.SEQ_PARAMETRO_MONITORIZACAO, ");
                    sb.AppendLine("            SUBSTR(PMN.DSC_PARAMETRO_MONITORIZACAO, 1, 30) AS DSC_PARAMETRO_MONITORIZACAO, ");
                    sb.AppendLine("            MIN(PMP.VLR_PARAMETRO_MONITORIZACAO) AS VLR_MIN, ");
                    sb.AppendLine("            MAX(PMP.VLR_PARAMETRO_MONITORIZACAO) AS VLR_MAX, ");
                    sb.AppendLine("            MAX(PMP.DAT_HOR_PAR_MON_PACIENTE) AS DTA_ULT ");
                    sb.AppendLine("        FROM ");
                    sb.AppendLine("            PARAMETRO_MONIT_PACIENTE PMP, ");
                    sb.AppendLine("            PARAMETRO_MONITORIZACAO  PMN, ");
                    sb.AppendLine("            MOVIMENTACAO_PACIENTE    MVP, ");
                    sb.AppendLine("            ATENDIMENTO_PACIENTE     ATP, ");
                    sb.AppendLine("            MAPEAMENTO_LOCAL         LOC, ");
                    sb.AppendLine("            CONFIGURACAO_LEITO       CFL, ");
                    sb.AppendLine("            INSTITUTO                INS  ");
                    sb.AppendLine("        WHERE ");
                    sb.AppendLine("            PMP.SEQ_ATENDIMENTO = :SEQ_ATENDIMENTO ");
                    /// Hoje
                    //sb.AppendLine("            AND PMP.DAT_HOR_PAR_MON_PACIENTE BETWEEN TRUNC(SYSDATE) - 1060 AND TRUNC(SYSDATE) + 0.99999 "); ALTERADO PARA BUSCAR DO DIA ANTERIOR E NAO DO PRIMEIRO DIA DO ATENDIMENTO
                    sb.AppendLine("            AND PMP.DAT_HOR_PAR_MON_PACIENTE BETWEEN TRUNC(SYSDATE) - 1 AND TRUNC(SYSDATE) + 0.99999 ");
                    sb.AppendLine("            AND PMN.COD_TIPO_MONITORIZACAO = 1 ");
                    sb.AppendLine("            AND PMP.SEQ_PARAMETRO_MONITORIZACAO = PMN.SEQ_PARAMETRO_MONITORIZACAO ");
                    sb.AppendLine("            AND PMP.SEQ_ATENDIMENTO = ATP.SEQ_ATENDIMENTO ");
                    sb.AppendLine("            AND PMP.NUM_SEQ_LOCAL = MVP.NUM_SEQ_LOCAL ");
                    sb.AppendLine("            AND PMP.DAT_HOR_PAR_MON_PACIENTE > MVP.DTA_HOR_ENTRADA ");
                    sb.AppendLine("            AND MVP.NUM_SEQ_LOCAL = LOC.NUM_SEQ_LOCAL ");
                    sb.AppendLine("            AND LOC.NUM_SEQ_LOCAL = CFL.NUM_SEQ_LOCAL(+) ");
                    sb.AppendLine("            AND LOC.COD_INSTITUTO = INS.COD_INSTITUTO ");
                    sb.AppendLine("            AND NOT EXISTS ( ");
                    sb.AppendLine("                    SELECT PMD2.SEQ_PARAMETRO_MONITORIZACAO ");
                    sb.AppendLine("                      FROM PARAMETRO_MONIT_DOMINIO PMD2 ");
                    sb.AppendLine("                     WHERE PMD2.SEQ_PARAMETRO_MONITORIZACAO = PMP.SEQ_PARAMETRO_MONITORIZACAO ");
                    sb.AppendLine("               ) ");
                    sb.AppendLine("        GROUP BY ");
                    sb.AppendLine("            PMP.SEQ_ATENDIMENTO, ");
                    sb.AppendLine("            PMP.SEQ_PARAMETRO_MONITORIZACAO, ");
                    sb.AppendLine("            SUBSTR(PMN.DSC_PARAMETRO_MONITORIZACAO, 1, 30) ");
                    sb.AppendLine("    ) X, ");
                    sb.AppendLine("    PARAMETRO_MONIT_PACIENTE Y ");
                    sb.AppendLine("WHERE ");
                    sb.AppendLine("    Y.SEQ_ATENDIMENTO = X.SEQ_ATENDIMENTO ");
                    sb.AppendLine("    AND Y.SEQ_PARAMETRO_MONITORIZACAO = X.SEQ_PARAMETRO_MONITORIZACAO ");
                    sb.AppendLine("    AND Y.DAT_HOR_PAR_MON_PACIENTE = X.DTA_ULT ");

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["SEQ_ATENDIMENTO"] = codigoAtendimento;

                    ctx.ExecuteQuery(query);

                    while (ctx.Reader.Read())
                    {
                        Hcrp.Framework.Classes.ParametroMonitorizacaoPaciente pm = new Hcrp.Framework.Classes.ParametroMonitorizacaoPaciente();
                        pm.ParametroMonitorizacao = new Classes.ParametroMonitorizacao();
                        pm.ParametroMonitorizacao.Codigo = Convert.ToInt32(ctx.Reader["SEQ_PARAMETRO_MONITORIZACAO"]);
                        pm.ParametroMonitorizacao.Descricao = ctx.Reader["DSC_PARAMETRO_MONITORIZACAO"].ToString();
                        pm.ParametroMonitorizacao.ValorMinimo = Convert.ToDouble(ctx.Reader["VLR_MIN"]);
                        pm.ParametroMonitorizacao.ValorMaximo = Convert.ToDouble(ctx.Reader["VLR_MAX"]);
                        pm._seqParametroMonitorizacao = Convert.ToInt32(ctx.Reader["SEQ_PARAMETRO_MONITORIZACAO"]);
                        pm.valor = Convert.ToDouble(ctx.Reader["VLR_ATUAL"]);
                        result.Add(pm);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }


    }
}
