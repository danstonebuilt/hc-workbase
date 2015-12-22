using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Dal
{
    public class MamaLocalizacao : Hcrp.Framework.Classes.MamaLocalizacao
    {
        #region variáveis / construtor

        internal Hcrp.Infra.AcessoDado.TransacaoDinamica transacao;

        public MamaLocalizacao()
        {

        }

        public MamaLocalizacao(Hcrp.Infra.AcessoDado.TransacaoDinamica _trans)
        {
            transacao = _trans;
        }

        #endregion

        public void Gravar(Framework.Classes.MamaLocalizacao Localizacao, long SeqPedidoAtendimento)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abrir conexão
                    ctx.Open();

                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("PED_EXAME_MAMA_LOCALIZACAO");
                    comando.Params["SEQ_PEDIDO_ATENDIMENTO"] = SeqPedidoAtendimento;
                    comando.Params["IDF_LADO"] = Convert.ToString(Localizacao.Lado);
                    comando.Params["IDF_TIPO_LOCALIZACAO"] = Convert.ToString(Localizacao.TipoLocalizacao);
                    comando.Params["COD_REGIAO_CORPO"] = Convert.ToString(Localizacao.RegiaoCorpo);

                    // Executar o insert
                    ctx.ExecuteInsert(comando);

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void GravarTrans(Framework.Classes.MamaLocalizacao Localizacao, long SeqPedidoAtendimento)
        {
            try
            {
                // Obter contexto transacionado
                Hcrp.Infra.AcessoDado.Contexto ctx = this.transacao.ctx;

                // Preparar o comando
                Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("PED_EXAME_MAMA_LOCALIZACAO");

                comando.Params["SEQ_PED_EXAME_MAMA_LOCALIZACAO"] = ctx.GetSequenceValue("GENERICO.SEQ_PED_EXAME_MAMA_LOCALIZACAO", true);

                //comando.Params["NUM_PEDIDO"] = Localizacao.PedidoAtendimento._num;

                comando.Params["SEQ_PEDIDO_ATENDIMENTO"] = SeqPedidoAtendimento;
                comando.Params["IDF_LADO"] = Convert.ToString(Localizacao.Lado);
                comando.Params["IDF_TIPO_LOCALIZACAO"] = Convert.ToString(Localizacao.TipoLocalizacao);
                comando.Params["COD_REGIAO_CORPO"] = Convert.ToString(Localizacao.RegiaoCorpo);

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
