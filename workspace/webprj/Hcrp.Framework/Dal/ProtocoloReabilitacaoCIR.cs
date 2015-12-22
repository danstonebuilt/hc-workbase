using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Dal
{
    public class ProtocoloReabilitacaoCIR
    {
        #region variáveis / construtor

        internal Hcrp.Infra.AcessoDado.TransacaoDinamica transacao;

        public ProtocoloReabilitacaoCIR()
        {

        }

        public ProtocoloReabilitacaoCIR(Hcrp.Infra.AcessoDado.TransacaoDinamica _trans)
        {
            transacao = _trans;
        }

        #endregion

        /// <summary>
        /// Inserir protocolo reabilitação CIR.
        /// </summary>
        /// <param name="protoc"></param>
        public void Inserir(Framework.Classes.ProtocoloReabilitacaoCIR _protocReabCIR)
        {
            try
            {

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abrir conexão
                    ctx.Open();

                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("PROTOCOLO_REABILITACAO_CIR");

                    comando.Params["SEQ_ITEM_PEDIDO_ATENDIMENTO"] = _protocReabCIR.SeqItemPedidoAtendimento;

                    if (_protocReabCIR.IdfDestino > 0)
                        comando.Params["IDF_DESTINO"] = _protocReabCIR.IdfDestino;

                    if (_protocReabCIR.IdfTipoFonoaudiologia > 0)
                        comando.Params["IDF_TIPO_FONOAUDIOLOGIA"] = _protocReabCIR.IdfTipoFonoaudiologia;

                    // Executar o insert
                    ctx.ExecuteInsert(comando);

                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void InserirTrans(Framework.Classes.ProtocoloReabilitacaoCIR _protocReabCIR)
        {
            try
            {

                // Obter contexto transacionado
                Hcrp.Infra.AcessoDado.Contexto ctx = this.transacao.ctx;

                // Preparar o comando
                Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("PROTOCOLO_REABILITACAO_CIR");

                comando.Params["SEQ_ITEM_PEDIDO_ATENDIMENTO"] = _protocReabCIR.SeqItemPedidoAtendimento;

                if (_protocReabCIR.IdfDestino > 0)
                    comando.Params["IDF_DESTINO"] = _protocReabCIR.IdfDestino;

                if (_protocReabCIR.IdfTipoFonoaudiologia > 0)
                    comando.Params["IDF_TIPO_FONOAUDIOLOGIA"] = _protocReabCIR.IdfTipoFonoaudiologia;

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
