using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Dal
{
    public class ProtocoloReabilitacaoQuest : Hcrp.Framework.Classes.ProtocoloReabilitacaoQuest
    {
        #region variáveis / construtor

        internal Hcrp.Infra.AcessoDado.TransacaoDinamica transacao;

        public ProtocoloReabilitacaoQuest()
        {

        }

        public ProtocoloReabilitacaoQuest(Hcrp.Infra.AcessoDado.TransacaoDinamica _trans)
        {
            transacao = _trans;
        }

        #endregion

        /// <summary>
        /// Inserir protocolo reabilitação questionário.
        /// </summary>
        /// <param name="protoc"></param>
        public void Inserir(Framework.Classes.ProtocoloReabilitacaoQuest _protocReabQuest)
        {
            try
            {

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abrir conexão
                    ctx.Open();

                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("PROTOCOLO_REABILITACAO_QUEST");

                    comando.Params["SEQ_ITEM_PEDIDO_ATENDIMENTO"] = _protocReabQuest.SeqItemPedidoAtendimento;

                    if (_protocReabQuest.IdfPressaoArterial > 0)
                        comando.Params["IDF_PRESSAO_ARTERIAL"] = _protocReabQuest.IdfPressaoArterial;

                    if (_protocReabQuest.IdfDiabete > 0)
                        comando.Params["IDF_DIABETE"] = _protocReabQuest.IdfDiabete;

                    if (_protocReabQuest.IdfCriseConvulsiva > 0)
                        comando.Params["IDF_CRISE_CONVULSIVA"] = _protocReabQuest.IdfCriseConvulsiva;

                    if (_protocReabQuest.IdfCondicaoRespiratoria > 0)
                        comando.Params["IDF_CONDICAO_RESPIRATORIA"] = _protocReabQuest.IdfCondicaoRespiratoria;

                    if (_protocReabQuest.IdfViaAlimentacao > 0)
                        comando.Params["IDF_VIA_ALIMENTACAO"] = _protocReabQuest.IdfViaAlimentacao;

                    if (_protocReabQuest.IdfFuncaoAuditiva > 0)
                        comando.Params["IDF_FUNCAO_AUDITIVA"] = _protocReabQuest.IdfFuncaoAuditiva;

                    // Esta coluna foi excluida do questionario, mantemos 0 no valor jah q nao existe.
                    comando.Params["IDF_FUNCAO_COMUNICATIVA"] = 0;

                    //if (_protocReabQuest.IdfFuncaoComunicativa > 0)
                    //    comando.Params["IDF_FUNCAO_COMUNICATIVA"] = _protocReabQuest.IdfFuncaoComunicativa;

                    if (_protocReabQuest.IdfFuncaoVisual > 0)
                        comando.Params["IDF_FUNCAO_VISUAL"] = _protocReabQuest.IdfFuncaoVisual;

                    if (_protocReabQuest.IdfCondicaoPele > 0)
                        comando.Params["IDF_CONDICAO_PELE"] = _protocReabQuest.IdfCondicaoPele;

                    if (_protocReabQuest.IdfPosCirurgico > 0)
                        comando.Params["IDF_POS_CIRURGICO"] = _protocReabQuest.IdfPosCirurgico;

                    if (_protocReabQuest.IdfLocomocao > 0)
                        comando.Params["IDF_LOCOMOCAO"] = _protocReabQuest.IdfLocomocao;

                    if (_protocReabQuest.IdfEquilibrio > 0)
                        comando.Params["IDF_EQUILIBRIO"] = _protocReabQuest.IdfEquilibrio;

                    if (_protocReabQuest.IdfAlteracaoPostural > 0)
                        comando.Params["IDF_ALTERACAO_POSTURAL"] = _protocReabQuest.IdfAlteracaoPostural;

                    if (_protocReabQuest.IdfFuncaoManual > 0)
                        comando.Params["IDF_FUNCAO_MANUAL"] = _protocReabQuest.IdfFuncaoManual;

                    if (_protocReabQuest.IdfFuncaoCognitiva > 0)
                        comando.Params["IDF_FUNCAO_COGNITIVA"] = _protocReabQuest.IdfFuncaoCognitiva;

                    if (_protocReabQuest.IdfDemencia > 0)
                        comando.Params["IDF_DEMENCIA"] = _protocReabQuest.IdfDemencia;

                    if (_protocReabQuest.IdfDificuldadeEscolar > 0)
                        comando.Params["IDF_DIFICULDADE_ESCOLAR"] = _protocReabQuest.IdfDificuldadeEscolar;

                    // Executar o insert
                    ctx.ExecuteInsert(comando);

                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Inserir protocolo reabilitação questionário transacionado.
        /// </summary>        
        public void InserirTrans(Framework.Classes.ProtocoloReabilitacaoQuest _protocReabQuest)
        {
            try
            {

                // Obter contexto transacionado
                Hcrp.Infra.AcessoDado.Contexto ctx = this.transacao.ctx;
                
                // Preparar o comando
                Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("PROTOCOLO_REABILITACAO_QUEST");

                comando.Params["SEQ_ITEM_PEDIDO_ATENDIMENTO"] = _protocReabQuest.SeqItemPedidoAtendimento;

                if (_protocReabQuest.IdfPressaoArterial > 0)
                    comando.Params["IDF_PRESSAO_ARTERIAL"] = _protocReabQuest.IdfPressaoArterial;

                if (_protocReabQuest.IdfDiabete > 0)
                    comando.Params["IDF_DIABETE"] = _protocReabQuest.IdfDiabete;

                if (_protocReabQuest.IdfCriseConvulsiva > 0)
                    comando.Params["IDF_CRISE_CONVULSIVA"] = _protocReabQuest.IdfCriseConvulsiva;

                if (_protocReabQuest.IdfCondicaoRespiratoria > 0)
                    comando.Params["IDF_CONDICAO_RESPIRATORIA"] = _protocReabQuest.IdfCondicaoRespiratoria;

                if (_protocReabQuest.IdfViaAlimentacao > 0)
                    comando.Params["IDF_VIA_ALIMENTACAO"] = _protocReabQuest.IdfViaAlimentacao;

                if (_protocReabQuest.IdfFuncaoAuditiva > 0)
                    comando.Params["IDF_FUNCAO_AUDITIVA"] = _protocReabQuest.IdfFuncaoAuditiva;

                // Esta coluna foi excluida do questionario, mantemos 0 no valor jah q nao existe.
                comando.Params["IDF_FUNCAO_COMUNICATIVA"] = 0;

                //if (_protocReabQuest.IdfFuncaoComunicativa > 0)
                //    comando.Params["IDF_FUNCAO_COMUNICATIVA"] = _protocReabQuest.IdfFuncaoComunicativa;

                if (_protocReabQuest.IdfFuncaoVisual > 0)
                    comando.Params["IDF_FUNCAO_VISUAL"] = _protocReabQuest.IdfFuncaoVisual;

                if (_protocReabQuest.IdfCondicaoPele > 0)
                    comando.Params["IDF_CONDICAO_PELE"] = _protocReabQuest.IdfCondicaoPele;

                if (_protocReabQuest.IdfPosCirurgico > 0)
                    comando.Params["IDF_POS_CIRURGICO"] = _protocReabQuest.IdfPosCirurgico;

                if (_protocReabQuest.IdfLocomocao > 0)
                    comando.Params["IDF_LOCOMOCAO"] = _protocReabQuest.IdfLocomocao;

                if (_protocReabQuest.IdfEquilibrio > 0)
                    comando.Params["IDF_EQUILIBRIO"] = _protocReabQuest.IdfEquilibrio;

                if (_protocReabQuest.IdfAlteracaoPostural > 0)
                    comando.Params["IDF_ALTERACAO_POSTURAL"] = _protocReabQuest.IdfAlteracaoPostural;

                if (_protocReabQuest.IdfFuncaoManual > 0)
                    comando.Params["IDF_FUNCAO_MANUAL"] = _protocReabQuest.IdfFuncaoManual;

                if (_protocReabQuest.IdfFuncaoCognitiva > 0)
                    comando.Params["IDF_FUNCAO_COGNITIVA"] = _protocReabQuest.IdfFuncaoCognitiva;

                if (_protocReabQuest.IdfDemencia > 0)
                    comando.Params["IDF_DEMENCIA"] = _protocReabQuest.IdfDemencia;

                if (_protocReabQuest.IdfDificuldadeEscolar > 0)
                    comando.Params["IDF_DIFICULDADE_ESCOLAR"] = _protocReabQuest.IdfDificuldadeEscolar;

                // Executar o insert
                ctx.ExecuteInsert(comando);


            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
