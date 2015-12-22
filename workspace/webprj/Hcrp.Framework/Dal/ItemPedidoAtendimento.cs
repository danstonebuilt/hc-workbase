using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace Hcrp.Framework.Dal
{
    public class ItemPedidoAtendimento : Hcrp.Framework.Classes.ItemPedidoAtendimento
    {
        public List<Hcrp.Framework.Classes.ItemPedidoAtendimento> BuscarItemsAtendimento(int seqPedidoAtendimento)
        {
            List<Hcrp.Framework.Classes.ItemPedidoAtendimento> l = new List<Hcrp.Framework.Classes.ItemPedidoAtendimento>();
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT A.SEQ_ITEM_PEDIDO_ATENDIMENTO, A.COD_SITUACAO, A.COD_PROCEDIMENTO_HC, " + Environment.NewLine); 
                    sb.Append("   A.NUM_SEQ_LOCAL_ATEND, A.DTA_HOR_ATENDIMENTO, A.NUM_USER_DEVOLUCAO, " + Environment.NewLine);
                    sb.Append("   A.COD_MOTIVO_CANCELAMENTO, A.IDF_ORIGEM_AGENDAMENTO, A.NUM_ORIGEM_AGENDAMENTO, A.COD_INSTITUTO, A.COD_ESPECIALIDADE_HC, A.SEQ_CONF_PROC_AGENDA " + Environment.NewLine);
                    sb.Append(" FROM ITEM_PEDIDO_ATENDIMENTO A " + Environment.NewLine);
                    sb.Append(" WHERE A.SEQ_PEDIDO_ATENDIMENTO = :SEQ_PEDIDO_ATENDIMENTO " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["SEQ_PEDIDO_ATENDIMENTO"] = seqPedidoAtendimento;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.ItemPedidoAtendimento i = new Hcrp.Framework.Classes.ItemPedidoAtendimento();
                        i.Seq = Convert.ToInt32(dr["SEQ_ITEM_PEDIDO_ATENDIMENTO"]);
                        if (dr["COD_SITUACAO"] != DBNull.Value)
                            i._CodSituacao = Convert.ToInt32(dr["COD_SITUACAO"]);
                        if (dr["COD_PROCEDIMENTO_HC"] != DBNull.Value)
                            i._CodProcedimentoHc = Convert.ToInt32(dr["COD_PROCEDIMENTO_HC"]);
                        if (dr["NUM_SEQ_LOCAL_ATEND"] != DBNull.Value)
                            i._NumSeqLocalAtendimento = Convert.ToInt32(dr["NUM_SEQ_LOCAL_ATEND"]);
                        if (dr["DTA_HOR_ATENDIMENTO"] != DBNull.Value)
                            i.DataAtendimento = Convert.ToDateTime(dr["DTA_HOR_ATENDIMENTO"]);
                        if (dr["NUM_USER_DEVOLUCAO"] != DBNull.Value)
                            i._NumUserDevolucao = Convert.ToInt32(dr["NUM_USER_DEVOLUCAO"]);
                        if (dr["COD_MOTIVO_CANCELAMENTO"] != DBNull.Value)
                            i._CodMotivoCancelamento = Convert.ToInt32(dr["COD_MOTIVO_CANCELAMENTO"]);
                        if (dr["IDF_ORIGEM_AGENDAMENTO"] != DBNull.Value)
                            i.OrigemAgendamento = (EOrigemAgendamento)Convert.ToInt32(dr["IDF_ORIGEM_AGENDAMENTO"]);
                        if (dr["NUM_ORIGEM_AGENDAMENTO"] != DBNull.Value)
                            i.NumeroOrigem = Convert.ToInt32(dr["NUM_ORIGEM_AGENDAMENTO"]);
                        if (dr["COD_INSTITUTO"] != DBNull.Value)
                            i._CodInstituto = Convert.ToInt32(dr["COD_INSTITUTO"]);
                        if (dr["COD_ESPECIALIDADE_HC"] != DBNull.Value)
                            i._CodEspecialidadeHc = Convert.ToInt32(dr["COD_ESPECIALIDADE_HC"]);
                        if (dr["SEQ_CONF_PROC_AGENDA"] != DBNull.Value)
                            i._SeqConfProcAgenda = Convert.ToInt32(dr["SEQ_CONF_PROC_AGENDA"]);
                        l.Add(i);
                    }
                }
                return l;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Hcrp.Framework.Classes.ItemPedidoAtendimento BuscarItemAtendimento(int seqItemPedidoAtendimento)
        {
            try
            {
                Hcrp.Framework.Classes.ItemPedidoAtendimento i = new Hcrp.Framework.Classes.ItemPedidoAtendimento();
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT A.SEQ_ITEM_PEDIDO_ATENDIMENTO, A.SEQ_PEDIDO_ATENDIMENTO, A.COD_SITUACAO, A.COD_PROCEDIMENTO_HC, " + Environment.NewLine); 
                    sb.Append("   A.NUM_SEQ_LOCAL_ATEND, A.DTA_HOR_ATENDIMENTO, A.NUM_USER_DEVOLUCAO, " + Environment.NewLine);
                    sb.Append("   A.COD_MOTIVO_CANCELAMENTO, A.IDF_ORIGEM_AGENDAMENTO, A.NUM_ORIGEM_AGENDAMENTO, A.COD_INSTITUTO, A.COD_ESPECIALIDADE_HC, A.SEQ_CONF_PROC_AGENDA " + Environment.NewLine);
                    sb.Append(" FROM ITEM_PEDIDO_ATENDIMENTO A " + Environment.NewLine);
                    sb.Append(" WHERE A.SEQ_ITEM_PEDIDO_ATENDIMENTO = :SEQ_ITEM_PEDIDO_ATENDIMENTO " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["SEQ_ITEM_PEDIDO_ATENDIMENTO"] = seqItemPedidoAtendimento;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        i.Seq = Convert.ToInt32(dr["SEQ_ITEM_PEDIDO_ATENDIMENTO"]);
                        if (dr["SEQ_PEDIDO_ATENDIMENTO"] != DBNull.Value)
                            i._NumSeqPedidoAtendimento = Convert.ToInt64(dr["SEQ_PEDIDO_ATENDIMENTO"]);
                        if (dr["COD_SITUACAO"] != DBNull.Value)
                            i._CodSituacao = Convert.ToInt32(dr["COD_SITUACAO"]);
                        if (dr["COD_PROCEDIMENTO_HC"] != DBNull.Value)
                            i._CodProcedimentoHc = Convert.ToInt32(dr["COD_PROCEDIMENTO_HC"]);
                        if (dr["NUM_SEQ_LOCAL_ATEND"] != DBNull.Value)
                            i._NumSeqLocalAtendimento = Convert.ToInt32(dr["NUM_SEQ_LOCAL_ATEND"]);
                        if (dr["DTA_HOR_ATENDIMENTO"] != DBNull.Value)
                            i.DataAtendimento = Convert.ToDateTime(dr["DTA_HOR_ATENDIMENTO"]);
                        if (dr["NUM_USER_DEVOLUCAO"] != DBNull.Value)
                            i._NumUserDevolucao = Convert.ToInt32(dr["NUM_USER_DEVOLUCAO"]);
                        if (dr["COD_MOTIVO_CANCELAMENTO"] != DBNull.Value)
                            i._CodMotivoCancelamento = Convert.ToInt32(dr["COD_MOTIVO_CANCELAMENTO"]);
                        if (dr["IDF_ORIGEM_AGENDAMENTO"] != DBNull.Value)
                            i.OrigemAgendamento = (EOrigemAgendamento)Convert.ToInt32(dr["IDF_ORIGEM_AGENDAMENTO"]);
                        if (dr["NUM_ORIGEM_AGENDAMENTO"] != DBNull.Value)
                            i.NumeroOrigem = Convert.ToInt32(dr["NUM_ORIGEM_AGENDAMENTO"]);
                        if (dr["COD_INSTITUTO"] != DBNull.Value)
                            i._CodInstituto = Convert.ToInt32(dr["COD_INSTITUTO"]);
                        if (dr["COD_ESPECIALIDADE_HC"] != DBNull.Value)
                            i._CodEspecialidadeHc = Convert.ToInt32(dr["COD_ESPECIALIDADE_HC"]);
                        if (dr["SEQ_CONF_PROC_AGENDA"] != DBNull.Value)
                            i._SeqConfProcAgenda = Convert.ToInt32(dr["SEQ_CONF_PROC_AGENDA"]);
                    }
                }
                return i;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Hcrp.Framework.Classes.ItemPedidoAtendimento> BuscarPedidosControle(List<Hcrp.Framework.Classes.ParametrosOracle> filtros, Hcrp.Framework.Classes.ItemPedidoAtendimento.ETipoSituacaoItemPedidoAtendimento situacao, int codServicoSadt, int codDrs = -1)
        {
            List<Hcrp.Framework.Classes.ItemPedidoAtendimento> l = new List<Hcrp.Framework.Classes.ItemPedidoAtendimento>();
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT B.SEQ_ITEM_PEDIDO_ATENDIMENTO, B.COD_SITUACAO, B.COD_PROCEDIMENTO_HC, " + Environment.NewLine);
                    sb.Append(" B.SEQ_PEDIDO_ATENDIMENTO, B.NUM_SEQ_LOCAL_ATEND, B.DTA_HOR_ATENDIMENTO, " + Environment.NewLine);
                    sb.Append(" B.NUM_USER_DEVOLUCAO, B.COD_MOTIVO_CANCELAMENTO, B.IDF_ORIGEM_AGENDAMENTO, " + Environment.NewLine);
                    sb.Append(" B.NUM_ORIGEM_AGENDAMENTO, B.COD_INSTITUTO, B.COD_ESPECIALIDADE_HC, B.SEQ_CONF_PROC_AGENDA, " + Environment.NewLine);
                    sb.Append(" NVL(EC.NOM_ESPECIALIDADE_HC, P.NOM_PROCEDIMENTO_HC) ESPEC_PROCEDIMENTO " + Environment.NewLine);
                    sb.Append(" FROM ITEM_PEDIDO_ATENDIMENTO B, PEDIDO_ATENDIMENTO A, " + Environment.NewLine);
                    sb.Append("      ESPECIALIDADE_HC EC, PROCEDIMENTO_HC P " + Environment.NewLine);
                    if (codDrs != -1)
                    {
                        sb.Append(" , LOCALIDADE C " + Environment.NewLine);
                    }
                    sb.Append(" WHERE B.SEQ_PEDIDO_ATENDIMENTO = A.SEQ_PEDIDO_ATENDIMENTO " + Environment.NewLine);
                    sb.Append("   AND B.COD_ESPECIALIDADE_HC = EC.COD_ESPECIALIDADE_HC(+)  " + Environment.NewLine);
                    sb.Append("   AND B.COD_PROCEDIMENTO_HC = P.COD_PROCEDIMENTO_HC(+) " + Environment.NewLine);

                    if (codDrs != -1)
                    {
                        sb.Append(" AND A.SGL_PAIS = C.SGL_PAIS " + Environment.NewLine);
                        sb.Append(" AND A.SGL_UF = C.SGL_UF " + Environment.NewLine);
                        sb.Append(" AND A.COD_LOCALIDADE = C.COD_LOCALIDADE " + Environment.NewLine);
                        sb.Append(" AND C.COD_DIR = :COD_DIR " + Environment.NewLine);
                    }

                    if ((int)situacao > -1)
                        sb.Append(" AND B.COD_SITUACAO = :COD_SITUACAO " + Environment.NewLine);
                    if (codServicoSadt > -1)
                        sb.Append(" AND A.COD_SERVICO_SADT = :COD_SERVICO_SADT " + Environment.NewLine);

                    foreach (var item in filtros)
                    {
                        if (item.TipoAssociacao != Hcrp.Framework.Classes.ParametrosOracle.eTipoAssociacao.IsNull)
                        {
                            // Quando informando o filtro "DTA_NASCIMENTO", deve ser convertido para TO_DATE para não ocorrer erro
                            // de cultura de data configurada no servidor.
                            if (item.TipoAssociacao == Hcrp.Framework.Classes.ParametrosOracle.eTipoAssociacao.Igual && item.CampoOracle.ToUpper() == "DTA_NASCIMENTO")
                            {
                                //sb.Append(string.Format(" AND A.DTA_NASCIMENTO = TO_DATE('{0}', 'DD/MM/YYYY') ", Convert.ToDateTime(item.ValorOracle).ToString("dd/MM/yyyy")));
                                sb.Append(" AND A.DTA_NASCIMENTO = TO_DATE(:DTA_NASCIMENTO, 'DD/MM/YYYY') ");
                                sb.Append(" " + Environment.NewLine);
                            }
                            else if (item.TipoAssociacao == Hcrp.Framework.Classes.ParametrosOracle.eTipoAssociacao.Igual)
                            {
                                sb.Append(" AND A." + item.CampoOracle + " = :" + item.CampoOracle + Environment.NewLine);
                            }
                            else if (item.TipoAssociacao == Hcrp.Framework.Classes.ParametrosOracle.eTipoAssociacao.Like)
                            {
                                sb.Append(" AND A." + item.CampoOracle + " LIKE :" + item.CampoOracle + Environment.NewLine);
                            }
                            else if (item.TipoAssociacao == Hcrp.Framework.Classes.ParametrosOracle.eTipoAssociacao.Between)
                            {
                                string campo1 = item.CampoOracle + "_1";
                                string campo2 = item.CampoOracle + "_2";
                                string[] valores = Convert.ToString(item.ValorOracle).Split(new char[] { '|' });
                                sb.Append(" AND A." + item.CampoOracle + " BETWEEN TO_DATE(:" + campo1 + ",'DD/MM/YYYY HH24:MI:SS') AND TO_DATE(:" + campo2 + ",'DD/MM/YYYY HH24:MI:SS')" + Environment.NewLine);
                            }
                        }
                    }
                    sb.Append(" ORDER BY 1 " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    if (codDrs != -1)
                        query.Params["COD_DIR"] = codDrs;

                    if ((int)situacao > -1)
                        query.Params["COD_SITUACAO"] = (int)situacao;

                    if (codServicoSadt > -1)
                        query.Params["COD_SERVICO_SADT"] = codServicoSadt;
                    
                    foreach (var item in filtros)
                    {
                        if (item.TipoAssociacao != Hcrp.Framework.Classes.ParametrosOracle.eTipoAssociacao.IsNull)
                        {
                            // Quando for "DTA_NASCIMENTO" a entrada da data tem que ser formatada para dd/mm/yyyy
                            if (item.TipoAssociacao == Hcrp.Framework.Classes.ParametrosOracle.eTipoAssociacao.Igual && item.CampoOracle.ToUpper() == "DTA_NASCIMENTO")
                            {
                                query.Params[item.CampoOracle] = item.ValorOracle.ToString();
                            }
                            else if (item.TipoAssociacao == Hcrp.Framework.Classes.ParametrosOracle.eTipoAssociacao.Igual)
                            {
                                query.Params[item.CampoOracle] = item.ValorOracle;
                            }
                            else if (item.TipoAssociacao == Hcrp.Framework.Classes.ParametrosOracle.eTipoAssociacao.Like)
                            {
                                query.Params[item.CampoOracle] = item.ValorOracle + "%";
                            }
                            else if (item.TipoAssociacao == Hcrp.Framework.Classes.ParametrosOracle.eTipoAssociacao.Between)
                            {
                                string campo1 = item.CampoOracle + "_1";
                                string campo2 = item.CampoOracle + "_2";
                                string[] valores = Convert.ToString(item.ValorOracle).Split(new char[] { '|' });
                                query.Params[campo1] = valores[0];
                                query.Params[campo2] = valores[1];
                            }
                        }
                    }

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.ItemPedidoAtendimento ip = new Hcrp.Framework.Classes.ItemPedidoAtendimento();                        
                        
                        if (dr["SEQ_ITEM_PEDIDO_ATENDIMENTO"] != DBNull.Value)
                            ip.Seq = Convert.ToInt32(dr["SEQ_ITEM_PEDIDO_ATENDIMENTO"]);

                        if (dr["COD_SITUACAO"] != DBNull.Value)
                            ip.TipoSituacao = (ETipoSituacaoItemPedidoAtendimento)Convert.ToInt32(dr["COD_SITUACAO"]);

                        if (dr["COD_SITUACAO"] != DBNull.Value)
                            ip._CodSituacao = Convert.ToInt32(dr["COD_SITUACAO"]);

                        if (dr["COD_PROCEDIMENTO_HC"] != DBNull.Value)
                            ip._CodProcedimentoHc = Convert.ToInt32(dr["COD_PROCEDIMENTO_HC"]);

                        if (dr["SEQ_PEDIDO_ATENDIMENTO"] != DBNull.Value)
                            ip._NumSeqPedidoAtendimento = Convert.ToInt32(dr["SEQ_PEDIDO_ATENDIMENTO"]);

                        if (dr["NUM_SEQ_LOCAL_ATEND"] != DBNull.Value) 
                            ip._NumSeqLocalAtendimento = Convert.ToInt32(dr["NUM_SEQ_LOCAL_ATEND"]);

                        if (dr["DTA_HOR_ATENDIMENTO"] != DBNull.Value) 
                            ip.DataAtendimento = Convert.ToDateTime(dr["DTA_HOR_ATENDIMENTO"]);

                        if (dr["NUM_USER_DEVOLUCAO"] != DBNull.Value) 
                            ip._NumUserDevolucao = Convert.ToInt32(dr["NUM_USER_DEVOLUCAO"]);

                        if (dr["COD_MOTIVO_CANCELAMENTO"] != DBNull.Value) 
                            ip._CodMotivoCancelamento = Convert.ToInt32(dr["COD_MOTIVO_CANCELAMENTO"]);

                        if (dr["IDF_ORIGEM_AGENDAMENTO"] != DBNull.Value) 
                            ip.OrigemAgendamento = (EOrigemAgendamento)Convert.ToInt32(dr["IDF_ORIGEM_AGENDAMENTO"]);

                        if (dr["NUM_ORIGEM_AGENDAMENTO"] != DBNull.Value) 
                            ip.NumeroOrigem = Convert.ToInt32(dr["NUM_ORIGEM_AGENDAMENTO"]);

                        if (dr["COD_INSTITUTO"] != DBNull.Value)
                            ip._CodInstituto = Convert.ToInt32(dr["COD_INSTITUTO"]);

                        if (dr["COD_ESPECIALIDADE_HC"] != DBNull.Value)
                            ip._CodEspecialidadeHc = Convert.ToInt32(dr["COD_ESPECIALIDADE_HC"]);

                        if (dr["SEQ_CONF_PROC_AGENDA"] != DBNull.Value)
                            ip._SeqConfProcAgenda = Convert.ToInt32(dr["SEQ_CONF_PROC_AGENDA"]);

                        if (dr["ESPEC_PROCEDIMENTO"] != DBNull.Value)
                            ip._EspecialidadeHc = dr["ESPEC_PROCEDIMENTO"].ToString();

                        l.Add(ip);
                    }
                }
                return l;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public long Gravar(Hcrp.Framework.Classes.ItemPedidoAtendimento ip,long seqPedidoAtendimento)
        {
            long seqItemPedidoAtendimento = 0;

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abrir conexão
                    ctx.Open();

                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("ITEM_PEDIDO_ATENDIMENTO");
                    comando.Params["SEQ_PEDIDO_ATENDIMENTO"] = seqPedidoAtendimento;

                    if (ip._CodProcedimentoHc > 0)
                        comando.Params["COD_PROCEDIMENTO_HC"] = ip._CodProcedimentoHc;

                    if (ip._CodInstituto > 0)
                        comando.Params["COD_INSTITUTO"] = ip._CodInstituto;

                    if (ip._CodEspecialidadeHc > 0)
                        comando.Params["COD_ESPECIALIDADE_HC"] = ip._CodEspecialidadeHc;

                    if (ip._SeqConfProcAgenda > 0)
                        comando.Params["SEQ_CONF_PROC_AGENDA"] = ip._SeqConfProcAgenda;

                    comando.Params["COD_SITUACAO"] = ip._CodSituacao;

                    // Executar o insert
                    ctx.ExecuteInsert(comando);

                    // Obter a sequence
                    seqItemPedidoAtendimento = ctx.GetSequenceValue("GENERICO.SEQ_ITEM_PEDIDO_ATENDIMENTO", false);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return seqItemPedidoAtendimento;

        }
        public void AlterarStatus(Hcrp.Framework.Classes.ItemPedidoAtendimento ip, ETipoSituacaoItemPedidoAtendimento NovoStatus)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abrir conexão
                    ctx.Open();

                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.UpdateCommandConfig comando = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("ITEM_PEDIDO_ATENDIMENTO");                    
                    comando.Params["COD_SITUACAO"] = (int)NovoStatus;
                    comando.FilterParams["SEQ_ITEM_PEDIDO_ATENDIMENTO"] = ip.Seq;                    
                    // Executar o insert
                    ctx.ExecuteUpdate(comando);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
