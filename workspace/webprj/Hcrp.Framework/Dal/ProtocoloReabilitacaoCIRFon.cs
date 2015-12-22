using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Dal
{
    public class ProtocoloReabilitacaoCIRFon
    {
        #region variáveis / construtor

        internal Hcrp.Infra.AcessoDado.TransacaoDinamica transacao;

        public ProtocoloReabilitacaoCIRFon()
        {

        }

        public ProtocoloReabilitacaoCIRFon(Hcrp.Infra.AcessoDado.TransacaoDinamica _trans)
        {
            transacao = _trans;
        }

        #endregion

        /// <summary>
        /// Inserir protocolo reabilitação CIR Fonoaudiologia.
        /// </summary>
        /// <param name="protoc"></param>
        public void Inserir(Framework.Classes.ProtocoloReabilitacaoCIRFon _protocReabCIRFon)
        {
            try
            {

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abrir conexão
                    ctx.Open();

                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("PROTOCOLO_REABILITACAO_CIR_FON");

                    comando.Params["SEQ_ITEM_PEDIDO_ATENDIMENTO"] = _protocReabCIRFon.SeqItemPedidoAtendimento;

                    if (_protocReabCIRFon.IdfTipoCaso > 0)
                        comando.Params["IDF_TIPO_CASO"] = _protocReabCIRFon.IdfTipoCaso;

                    if (!string.IsNullOrWhiteSpace(_protocReabCIRFon.IdfOccAltFala))
                        comando.Params["IDF_OCC_ALT_FALA"] = _protocReabCIRFon.IdfOccAltFala;

                    if (!string.IsNullOrWhiteSpace(_protocReabCIRFon.IdfOccAltEscrita))
                        comando.Params["IDF_OCC_ALT_ESCRITA"] = _protocReabCIRFon.IdfOccAltEscrita;

                    if (!string.IsNullOrWhiteSpace(_protocReabCIRFon.IdfOccDisfagiaLeve))
                        comando.Params["IDF_OCC_DISFAGIA_LEVE"] = _protocReabCIRFon.IdfOccDisfagiaLeve;

                    if (!string.IsNullOrWhiteSpace(_protocReabCIRFon.IdfMoRespiradorOral))
                        comando.Params["IDF_MO_RESPIRADOR_ORAL"] = _protocReabCIRFon.IdfMoRespiradorOral;

                    if (!string.IsNullOrWhiteSpace(_protocReabCIRFon.IdfMoAltFala))
                        comando.Params["IDF_MO_ALT_FALA"] = _protocReabCIRFon.IdfMoAltFala;

                    if (!string.IsNullOrWhiteSpace(_protocReabCIRFon.IdfMoAltMastDegl))
                        comando.Params["IDF_MO_ALT_MAST_DEGL"] = _protocReabCIRFon.IdfMoAltMastDegl;

                    if (!string.IsNullOrWhiteSpace(_protocReabCIRFon.IdfMoCasosOrtodonticos))
                        comando.Params["IDF_MO_CASOS_ORTODONTICOS"] = _protocReabCIRFon.IdfMoCasosOrtodonticos;

                    if (!string.IsNullOrWhiteSpace(_protocReabCIRFon.IdfMoDisfagiaLeve))
                        comando.Params["IDF_MO_DISFAGIA_LEVE"] = _protocReabCIRFon.IdfMoDisfagiaLeve;

                    if (!string.IsNullOrWhiteSpace(_protocReabCIRFon.IdfGagueira))
                        comando.Params["IDF_GAGUEIRA"] = _protocReabCIRFon.IdfGagueira;

                    if (!string.IsNullOrWhiteSpace(_protocReabCIRFon.IdfComunicacaoAlternativa))
                        comando.Params["IDF_COMUNICACAO_ALTERNATIVA"] = _protocReabCIRFon.IdfComunicacaoAlternativa;

                    if (!string.IsNullOrWhiteSpace(_protocReabCIRFon.IdfAltercaoVozDisfonia))
                        comando.Params["IDF_ALT_VOZ"] = _protocReabCIRFon.IdfAltercaoVozDisfonia;

                    if (!string.IsNullOrWhiteSpace(_protocReabCIRFon.IdfDefAuditiva))
                        comando.Params["IDF_RDA_DEF_AUDITIVA"] = _protocReabCIRFon.IdfDefAuditiva;

                    if (!string.IsNullOrWhiteSpace(_protocReabCIRFon.IdfDefProcAuditivo))
                        comando.Params["IDF_RDA_DEF_PROC_AUDITIVO"] = _protocReabCIRFon.IdfDefProcAuditivo;

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
        /// Inserir protocolo reabilitação CIR Fonoaudiologia transacionado.
        /// </summary>        
        public void InserirTrans(Framework.Classes.ProtocoloReabilitacaoCIRFon _protocReabCIRFon)
        {
            try
            {
                // Obter contexto transacionado
                Hcrp.Infra.AcessoDado.Contexto ctx = this.transacao.ctx;
                
                // Preparar o comando
                Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("PROTOCOLO_REABILITACAO_CIR_FON");

                comando.Params["SEQ_ITEM_PEDIDO_ATENDIMENTO"] = _protocReabCIRFon.SeqItemPedidoAtendimento;

                if (_protocReabCIRFon.IdfTipoCaso > 0)
                    comando.Params["IDF_TIPO_CASO"] = _protocReabCIRFon.IdfTipoCaso;

                if (!string.IsNullOrWhiteSpace(_protocReabCIRFon.IdfOccAltFala))
                    comando.Params["IDF_OCC_ALT_FALA"] = _protocReabCIRFon.IdfOccAltFala;

                if (!string.IsNullOrWhiteSpace(_protocReabCIRFon.IdfOccAltEscrita))
                    comando.Params["IDF_OCC_ALT_ESCRITA"] = _protocReabCIRFon.IdfOccAltEscrita;

                if (!string.IsNullOrWhiteSpace(_protocReabCIRFon.IdfOccDisfagiaLeve))
                    comando.Params["IDF_OCC_DISFAGIA_LEVE"] = _protocReabCIRFon.IdfOccDisfagiaLeve;

                if (!string.IsNullOrWhiteSpace(_protocReabCIRFon.IdfMoRespiradorOral))
                    comando.Params["IDF_MO_RESPIRADOR_ORAL"] = _protocReabCIRFon.IdfMoRespiradorOral;

                if (!string.IsNullOrWhiteSpace(_protocReabCIRFon.IdfMoAltFala))
                    comando.Params["IDF_MO_ALT_FALA"] = _protocReabCIRFon.IdfMoAltFala;

                if (!string.IsNullOrWhiteSpace(_protocReabCIRFon.IdfMoAltMastDegl))
                    comando.Params["IDF_MO_ALT_MAST_DEGL"] = _protocReabCIRFon.IdfMoAltMastDegl;

                if (!string.IsNullOrWhiteSpace(_protocReabCIRFon.IdfMoCasosOrtodonticos))
                    comando.Params["IDF_MO_CASOS_ORTODONTICOS"] = _protocReabCIRFon.IdfMoCasosOrtodonticos;

                if (!string.IsNullOrWhiteSpace(_protocReabCIRFon.IdfMoDisfagiaLeve))
                    comando.Params["IDF_MO_DISFAGIA_LEVE"] = _protocReabCIRFon.IdfMoDisfagiaLeve;

                if (!string.IsNullOrWhiteSpace(_protocReabCIRFon.IdfGagueira))
                    comando.Params["IDF_GAGUEIRA"] = _protocReabCIRFon.IdfGagueira;

                if (!string.IsNullOrWhiteSpace(_protocReabCIRFon.IdfComunicacaoAlternativa))
                    comando.Params["IDF_COMUNICACAO_ALTERNATIVA"] = _protocReabCIRFon.IdfComunicacaoAlternativa;

                if (!string.IsNullOrWhiteSpace(_protocReabCIRFon.IdfAltercaoVozDisfonia))
                    comando.Params["IDF_ALT_VOZ"] = _protocReabCIRFon.IdfAltercaoVozDisfonia;

                if (!string.IsNullOrWhiteSpace(_protocReabCIRFon.IdfDefAuditiva))
                    comando.Params["IDF_RDA_DEF_AUDITIVA"] = _protocReabCIRFon.IdfDefAuditiva;

                if (!string.IsNullOrWhiteSpace(_protocReabCIRFon.IdfDefProcAuditivo))
                    comando.Params["IDF_RDA_DEF_PROC_AUDITIVO"] = _protocReabCIRFon.IdfDefProcAuditivo;

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
