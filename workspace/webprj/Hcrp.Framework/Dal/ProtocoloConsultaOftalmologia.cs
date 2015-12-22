using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Dal
{
    public class ProtocoloConsultaOftalmologia
    {
        #region variáveis / construtor

        internal Hcrp.Infra.AcessoDado.TransacaoDinamica transacao;

        public ProtocoloConsultaOftalmologia()
        {

        }

        public ProtocoloConsultaOftalmologia(Hcrp.Infra.AcessoDado.TransacaoDinamica _trans)
        {
            transacao = _trans;
        }

        #endregion

        /// <summary>
        /// Obter protocolo consulta oftalmologia por sequencia de item pedido atendimento.
        /// </summary>
        /// <param name="seqitemPedidoAtendimento"></param>
        /// <returns></returns>
        public Framework.Classes.ProtocoloConsultaOftalmologia ObterProtocoloConsultaOftalmologiaPorSequenciaItemPedidoAtendimento(int numSeqItemPedidoAtendimento)
        {
            Framework.Classes.ProtocoloConsultaOftalmologia protcConsOftal = null;
            Framework.Classes.ProtocoloConsultaOftalmologiaExame protcConsOftalExame = null;
            Framework.Classes.ProtocoloConsultaOftalmoProc protcConsOftalProcedimento = null;

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Preparar a query
                    StringBuilder str = new StringBuilder();

                    str.AppendLine(" SELECT PO.SEQ_PROT_CONS_OFTALMOLOGIA, PO.SEQ_ITEM_PEDIDO_ATENDIMENTO, ");
                    str.AppendLine(" PO.IDF_ESPECIALIDADE_MEDICO, PO.DSC_OUTR_ESPECIALIDADE_MEDICO, PO.IDF_NEC_EXAME_COMPLEMENTAR, ");
                    str.AppendLine(" PO.IDF_NEC_AVALIACAO_DIAGNOSTICA, PO.IDF_NEC_SEG_CLINICO, PO.IDF_NEC_AVAL_ESPEC_CIRURGICA, PO.IDF_NEC_PROCEDIMENTO ");
                    str.AppendLine(" FROM PROTOCOLO_CONS_OFTALMOLOGIA PO ");
                    str.AppendLine(" WHERE PO.SEQ_ITEM_PEDIDO_ATENDIMENTO = :SEQ_ITEM_PEDIDO_ATENDIMENTO ");

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(str.ToString());

                    query.Params["SEQ_ITEM_PEDIDO_ATENDIMENTO"] = numSeqItemPedidoAtendimento;

                    // Abrir conexão
                    ctx.Open();

                    // Executar a query
                    ctx.ExecuteQuery(query);

                    // Preparar o retorno
                    while (ctx.Reader.Read())
                    {
                        #region carregar Protocolo Consulta Oftalmologia
                        protcConsOftal = new Classes.ProtocoloConsultaOftalmologia();

                        if (ctx.Reader["SEQ_PROT_CONS_OFTALMOLOGIA"] != DBNull.Value)
                            protcConsOftal.NumSeqProtocoloConsultaOftalmologia = Convert.ToInt64(ctx.Reader["SEQ_PROT_CONS_OFTALMOLOGIA"]);

                        if (ctx.Reader["SEQ_ITEM_PEDIDO_ATENDIMENTO"] != DBNull.Value)
                            protcConsOftal.NumSeqItemPedidoAtendimento = Convert.ToInt32(ctx.Reader["SEQ_ITEM_PEDIDO_ATENDIMENTO"]);

                        if (ctx.Reader["IDF_ESPECIALIDADE_MEDICO"] != DBNull.Value)
                            protcConsOftal.IdfEspecialidadeMedico = ctx.Reader["IDF_ESPECIALIDADE_MEDICO"].ToString();

                        if (ctx.Reader["DSC_OUTR_ESPECIALIDADE_MEDICO"] != DBNull.Value)
                            protcConsOftal.DscOutraEspecialidadeMedico = ctx.Reader["DSC_OUTR_ESPECIALIDADE_MEDICO"].ToString();

                        if (ctx.Reader["IDF_NEC_EXAME_COMPLEMENTAR"] != DBNull.Value)
                            protcConsOftal.IdfNecessidadeExameComplementar = ctx.Reader["IDF_NEC_EXAME_COMPLEMENTAR"].ToString();

                        if (ctx.Reader["IDF_NEC_AVALIACAO_DIAGNOSTICA"] != DBNull.Value)
                            protcConsOftal.IdfNecessidadeAvaliacaoDiagnostica = ctx.Reader["IDF_NEC_AVALIACAO_DIAGNOSTICA"].ToString();

                        if (ctx.Reader["IDF_NEC_SEG_CLINICO"] != DBNull.Value)
                            protcConsOftal.IdfNecessidadeSeguimentoClinico = ctx.Reader["IDF_NEC_SEG_CLINICO"].ToString();

                        if (ctx.Reader["IDF_NEC_AVAL_ESPEC_CIRURGICA"] != DBNull.Value)
                            protcConsOftal.IdfNecessidadeAvaliacaoEspecializadaCirurgica = ctx.Reader["IDF_NEC_AVAL_ESPEC_CIRURGICA"].ToString();

                        if (ctx.Reader["IDF_NEC_PROCEDIMENTO"] != DBNull.Value)
                            protcConsOftal.IdfNecessidadeProcedimento = ctx.Reader["IDF_NEC_PROCEDIMENTO"].ToString();

                        #endregion
                    }

                    #region carregar Protocolo Consulta Oftalmologia Exame

                    if (protcConsOftal != null)
                    {

                        str.Clear();

                        str.AppendLine(" SELECT PE.SEQ_PROT_CONS_OFTALMOLOGIA, PE.IDF_EXAME, PE.DSC_OUTRO_EXAME ");
                        str.AppendLine(" FROM PROT_CONS_OFTALMO_EXAME PE ");
                        str.AppendLine(" WHERE PE.SEQ_PROT_CONS_OFTALMOLOGIA = :SEQ_PROT_CONS_OFTALMOLOGIA ");

                        Hcrp.Infra.AcessoDado.QueryCommandConfig query2 = new Hcrp.Infra.AcessoDado.QueryCommandConfig(str.ToString());

                        query2.Params["SEQ_PROT_CONS_OFTALMOLOGIA"] = protcConsOftal.NumSeqProtocoloConsultaOftalmologia;

                        // Executar a query
                        ctx.ExecuteQuery(query2);

                        // Preparar o retorno
                        while (ctx.Reader.Read())
                        {
                            protcConsOftalExame = new Classes.ProtocoloConsultaOftalmologiaExame();

                            if (ctx.Reader["SEQ_PROT_CONS_OFTALMOLOGIA"] != DBNull.Value)
                                protcConsOftalExame.NumSeqProtocoloConsultaOftalmologia = Convert.ToInt64(ctx.Reader["SEQ_PROT_CONS_OFTALMOLOGIA"]);

                            if (ctx.Reader["IDF_EXAME"] != DBNull.Value)
                                protcConsOftalExame.IdfExame = Convert.ToInt16(ctx.Reader["IDF_EXAME"]);

                            if (ctx.Reader["DSC_OUTRO_EXAME"] != DBNull.Value)
                                protcConsOftalExame.DscOutroExame = ctx.Reader["DSC_OUTRO_EXAME"].ToString();

                            protcConsOftal.listProtocoloConsultaOftalmologiaExame.Add(protcConsOftalExame);
                        }

                    }

                    #endregion

                    #region carregar Protocolo Consulta Oftalmologia Procedimento

                    if (protcConsOftal != null)
                    {

                        str.Clear();
                        str = new StringBuilder();

                        str.AppendLine(" SELECT PP.SEQ_PROT_CONS_OFTALMOLOGIA, PP.IDF_PROCEDIMENTO, PP.DSC_OUTRO_PROCEDIMENTO ");
                        str.AppendLine(" FROM PROT_CONS_OFTALMO_PROCEDIMENTO PP ");
                        str.AppendLine(" WHERE PP.SEQ_PROT_CONS_OFTALMOLOGIA = :SEQ_PROT_CONS_OFTALMOLOGIA_ ");

                        Hcrp.Infra.AcessoDado.QueryCommandConfig query3 = new Hcrp.Infra.AcessoDado.QueryCommandConfig(str.ToString());

                        query3.Params["SEQ_PROT_CONS_OFTALMOLOGIA_"] = protcConsOftal.NumSeqProtocoloConsultaOftalmologia;

                        // Executar a query
                        ctx.ExecuteQuery(query3);

                        // Preparar o retorno
                        while (ctx.Reader.Read())
                        {
                            protcConsOftalProcedimento = new Classes.ProtocoloConsultaOftalmoProc();

                            if (ctx.Reader["SEQ_PROT_CONS_OFTALMOLOGIA"] != DBNull.Value)
                                protcConsOftalProcedimento.NumSeqProtocoloConsultaOftalmologia = Convert.ToInt64(ctx.Reader["SEQ_PROT_CONS_OFTALMOLOGIA"]);

                            if (ctx.Reader["IDF_PROCEDIMENTO"] != DBNull.Value)
                                protcConsOftalProcedimento.IdfProcedimento = Convert.ToInt16(ctx.Reader["IDF_PROCEDIMENTO"]);

                            if (ctx.Reader["DSC_OUTRO_PROCEDIMENTO"] != DBNull.Value)
                                protcConsOftalProcedimento.DescricaoOutroProc = ctx.Reader["DSC_OUTRO_PROCEDIMENTO"].ToString();

                            protcConsOftal.listProtocoloConsultaOftalmologiaProcedimento.Add(protcConsOftalProcedimento);
                        }

                    }

                    #endregion
                }
            }
            catch (Exception)
            {
                throw;
            }

            return protcConsOftal;
        }

        /// <summary>
        /// Inserir novo Protocolo de Consulta Oftalmologia.
        /// </summary>
        /// <param name="protoc"></param>
        public void Inserir(Framework.Classes.ProtocoloConsultaOftalmologia protoc)
        {
            try
            {

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abrir conexão
                    ctx.Open();

                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("PROTOCOLO_CONS_OFTALMOLOGIA");
                    comando.Params["SEQ_ITEM_PEDIDO_ATENDIMENTO"] = protoc.NumSeqItemPedidoAtendimento;

                    if (!string.IsNullOrWhiteSpace(protoc.IdfEspecialidadeMedico))
                        comando.Params["IDF_ESPECIALIDADE_MEDICO"] = protoc.IdfEspecialidadeMedico;

                    if (!string.IsNullOrWhiteSpace(protoc.DscOutraEspecialidadeMedico))
                        comando.Params["DSC_OUTR_ESPECIALIDADE_MEDICO"] = protoc.DscOutraEspecialidadeMedico;

                    if (!string.IsNullOrWhiteSpace(protoc.IdfNecessidadeExameComplementar))
                        comando.Params["IDF_NEC_EXAME_COMPLEMENTAR"] = protoc.IdfNecessidadeExameComplementar;

                    if (!string.IsNullOrWhiteSpace(protoc.IdfNecessidadeAvaliacaoDiagnostica))
                        comando.Params["IDF_NEC_AVALIACAO_DIAGNOSTICA"] = protoc.IdfNecessidadeAvaliacaoDiagnostica;

                    if (!string.IsNullOrWhiteSpace(protoc.IdfNecessidadeSeguimentoClinico))
                        comando.Params["IDF_NEC_SEG_CLINICO"] = protoc.IdfNecessidadeSeguimentoClinico;

                    if (!string.IsNullOrWhiteSpace(protoc.IdfNecessidadeAvaliacaoEspecializadaCirurgica))
                        comando.Params["IDF_NEC_AVAL_ESPEC_CIRURGICA"] = protoc.IdfNecessidadeAvaliacaoEspecializadaCirurgica;

                    if (!string.IsNullOrWhiteSpace(protoc.IdfNecessidadeProcedimento))
                        comando.Params["IDF_NEC_PROCEDIMENTO"] = protoc.IdfNecessidadeAvaliacaoEspecializadaCirurgica;

                    // Executar o insert
                    ctx.ExecuteInsert(comando);

                    // Pegar o último ID
                    protoc.NumSeqProtocoloConsultaOftalmologia = ctx.GetSequenceValue("GENERICO.SEQ_PROT_CONS_OFTALMOLOGIA", false);

                    if (protoc.NumSeqProtocoloConsultaOftalmologia > 0 &&
                        protoc.listProtocoloConsultaOftalmologiaExame != null &&
                        protoc.listProtocoloConsultaOftalmologiaExame.Count > 0)
                    {

                        Hcrp.Infra.AcessoDado.CommandConfig comando2 = new Hcrp.Infra.AcessoDado.CommandConfig("PROT_CONS_OFTALMO_EXAME");

                        foreach (Framework.Classes.ProtocoloConsultaOftalmologiaExame item in protoc.listProtocoloConsultaOftalmologiaExame)
                        {
                            // Preparar o comando

                            comando2.Params["SEQ_PROT_CONS_OFTALMOLOGIA"] = protoc.NumSeqProtocoloConsultaOftalmologia;
                            comando2.Params["IDF_EXAME"] = item.IdfExame;

                            if (!string.IsNullOrWhiteSpace(item.DscOutroExame))
                                comando2.Params["DSC_OUTRO_EXAME"] = item.DscOutroExame;

                            // Executar o insert
                            ctx.ExecuteInsert(comando2);
                        }
                    }

                    if (protoc.NumSeqProtocoloConsultaOftalmologia > 0 &&
                        protoc.listProtocoloConsultaOftalmologiaProcedimento != null &&
                        protoc.listProtocoloConsultaOftalmologiaProcedimento.Count > 0)
                    {

                        Hcrp.Infra.AcessoDado.CommandConfig comando3 = new Hcrp.Infra.AcessoDado.CommandConfig("PROT_CONS_OFTALMO_PROCEDIMENTO");

                        foreach (Framework.Classes.ProtocoloConsultaOftalmoProc item in protoc.listProtocoloConsultaOftalmologiaProcedimento)
                        {
                            // Preparar o comando
                            comando3.Params["SEQ_PROT_CONS_OFTALMOLOGIA"] = protoc.NumSeqProtocoloConsultaOftalmologia;
                            comando3.Params["IDF_PROCEDIMENTO"] = item.IdfProcedimento;

                            if (!string.IsNullOrWhiteSpace(item.DescricaoOutroProc))
                                comando3.Params["DSC_OUTRO_PROCEDIMENTO"] = item.DescricaoOutroProc;

                            // Executar o insert
                            ctx.ExecuteInsert(comando3);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Inserir novo Protocolo de Consulta Oftalmologia transacionado.
        /// </summary>        
        public void InserirTrans(Framework.Classes.ProtocoloConsultaOftalmologia protoc)
        {
            try
            {
                // Obter contexto transacionado
                Hcrp.Infra.AcessoDado.Contexto ctx = this.transacao.ctx;

                // Preparar o comando
                Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("PROTOCOLO_CONS_OFTALMOLOGIA");
                comando.Params["SEQ_ITEM_PEDIDO_ATENDIMENTO"] = protoc.NumSeqItemPedidoAtendimento;

                if (!string.IsNullOrWhiteSpace(protoc.IdfEspecialidadeMedico))
                    comando.Params["IDF_ESPECIALIDADE_MEDICO"] = protoc.IdfEspecialidadeMedico;

                if (!string.IsNullOrWhiteSpace(protoc.DscOutraEspecialidadeMedico))
                    comando.Params["DSC_OUTR_ESPECIALIDADE_MEDICO"] = protoc.DscOutraEspecialidadeMedico;

                if (!string.IsNullOrWhiteSpace(protoc.IdfNecessidadeExameComplementar))
                    comando.Params["IDF_NEC_EXAME_COMPLEMENTAR"] = protoc.IdfNecessidadeExameComplementar;

                if (!string.IsNullOrWhiteSpace(protoc.IdfNecessidadeAvaliacaoDiagnostica))
                    comando.Params["IDF_NEC_AVALIACAO_DIAGNOSTICA"] = protoc.IdfNecessidadeAvaliacaoDiagnostica;

                if (!string.IsNullOrWhiteSpace(protoc.IdfNecessidadeSeguimentoClinico))
                    comando.Params["IDF_NEC_SEG_CLINICO"] = protoc.IdfNecessidadeSeguimentoClinico;

                if (!string.IsNullOrWhiteSpace(protoc.IdfNecessidadeAvaliacaoEspecializadaCirurgica))
                    comando.Params["IDF_NEC_AVAL_ESPEC_CIRURGICA"] = protoc.IdfNecessidadeAvaliacaoEspecializadaCirurgica;

                if (!string.IsNullOrWhiteSpace(protoc.IdfNecessidadeProcedimento))
                    comando.Params["IDF_NEC_PROCEDIMENTO"] = protoc.IdfNecessidadeAvaliacaoEspecializadaCirurgica;

                // Executar o insert
                ctx.ExecuteInsert(comando);

                // Pegar o último ID
                protoc.NumSeqProtocoloConsultaOftalmologia = ctx.GetSequenceValue("GENERICO.SEQ_PROT_CONS_OFTALMOLOGIA", false);

                if (protoc.NumSeqProtocoloConsultaOftalmologia > 0 &&
                    protoc.listProtocoloConsultaOftalmologiaExame != null &&
                    protoc.listProtocoloConsultaOftalmologiaExame.Count > 0)
                {

                    Hcrp.Infra.AcessoDado.CommandConfig comando2 = new Hcrp.Infra.AcessoDado.CommandConfig("PROT_CONS_OFTALMO_EXAME");

                    foreach (Framework.Classes.ProtocoloConsultaOftalmologiaExame item in protoc.listProtocoloConsultaOftalmologiaExame)
                    {
                        // Preparar o comando

                        comando2.Params["SEQ_PROT_CONS_OFTALMOLOGIA"] = protoc.NumSeqProtocoloConsultaOftalmologia;
                        comando2.Params["IDF_EXAME"] = item.IdfExame;

                        if (!string.IsNullOrWhiteSpace(item.DscOutroExame))
                            comando2.Params["DSC_OUTRO_EXAME"] = item.DscOutroExame;

                        // Executar o insert
                        ctx.ExecuteInsert(comando2);
                    }
                }

                if (protoc.NumSeqProtocoloConsultaOftalmologia > 0 &&
                    protoc.listProtocoloConsultaOftalmologiaProcedimento != null &&
                    protoc.listProtocoloConsultaOftalmologiaProcedimento.Count > 0)
                {

                    Hcrp.Infra.AcessoDado.CommandConfig comando3 = new Hcrp.Infra.AcessoDado.CommandConfig("PROT_CONS_OFTALMO_PROCEDIMENTO");

                    foreach (Framework.Classes.ProtocoloConsultaOftalmoProc item in protoc.listProtocoloConsultaOftalmologiaProcedimento)
                    {
                        // Preparar o comando
                        comando3.Params["SEQ_PROT_CONS_OFTALMOLOGIA"] = protoc.NumSeqProtocoloConsultaOftalmologia;
                        comando3.Params["IDF_PROCEDIMENTO"] = item.IdfProcedimento;

                        if (!string.IsNullOrWhiteSpace(item.DescricaoOutroProc))
                            comando3.Params["DSC_OUTRO_PROCEDIMENTO"] = item.DescricaoOutroProc;

                        // Executar o insert
                        ctx.ExecuteInsert(comando3);
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
