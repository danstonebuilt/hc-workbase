using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace Hcrp.Framework.Dal
{
    public class Mamografia : Framework.Classes.Mamografia
    {
        #region variáveis / construtor

        internal Hcrp.Infra.AcessoDado.TransacaoDinamica transacao;

        public Mamografia()
        {

        }

        public Mamografia(Hcrp.Infra.AcessoDado.TransacaoDinamica _trans)
        {
            transacao = _trans;
        }

        #endregion

        public long Gravar(Framework.Classes.Mamografia PedidoMamografia)
        {
            try
            {
                long retorno = 0;
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abrir conexão
                    ctx.Open();

                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("PEDIDO_EXAME_HC_MAMOGRAFIA");

                    comando.Params["SEQ_PEDIDO_EXAME_MAMOGRAFIA"] = ctx.GetSequenceValue("GENERICO.SEQ_PEDIDO_EXAME_MAMOGRAFIA", true);

                    comando.Params["SEQ_PEDIDO_ATENDIMENTO"] = PedidoMamografia.seqPedidoAtendimento;
                    comando.Params["IDF_NODULO_CAROCO_MD"] = Convert.ToString(PedidoMamografia.NoduloCarocoMamaDireita);
                    comando.Params["IDF_NODULO_CAROCO_ME"] = Convert.ToString(PedidoMamografia.NoduloCarocoMamaEsquerda);
                    comando.Params["IDF_RISCO_ELEVADO_CANCER"] = Convert.ToString(PedidoMamografia.RiscoElevadoCancer);
                    comando.Params["IDF_EXAME_PREVIO"] = Convert.ToString(PedidoMamografia.ExamePrevio);
                    comando.Params["IDF_MAMOGRAFIA_PREVIA"] = Convert.ToString(PedidoMamografia.MamografiaPrevia);
                    comando.Params["NUM_ANO_MAMOGRAFIA_PREVIA"] = PedidoMamografia.AnoMamografiaPrevia;
                    comando.Params["DTA_EXAME_CLINICO"] = PedidoMamografia.DataExameClinico;
                    comando.Params["IDF_TIPO_MAMOGRAFIA"] = Convert.ToString(PedidoMamografia.TipoMamografia);
                    comando.Params["IDF_MAMA_MAMOGRAFIA_DIAGN"] = Convert.ToString(PedidoMamografia.MamaMamografiaDiagnostica);
                    comando.Params["IDF_TIPO_MAMOGRAFIA_DIAGN"] = Convert.ToString(PedidoMamografia.TipoMamografiaDiagnostica);
                    comando.Params["IDF_AVAL_RESP_QT_NEO_ADJ"] = Convert.ToString(PedidoMamografia.AvaliacaoRespostaQT);

                    // Executar o insert
                    ctx.ExecuteInsert(comando);

                    // Pegar o último ID
                    retorno = ctx.GetSequenceValue("GENERICO.SEQ_PEDIDO_EXAME_MAMOGRAFIA", false);

                    if ((PedidoMamografia.MamaControleLesao != null) && (PedidoMamografia.MamaControleLesao.Count > 0))
                    {
                        foreach (var ItemControleLesao in PedidoMamografia.MamaControleLesao)
                        {
                            ItemControleLesao.Gravar(retorno);
                        }
                    }

                    if ((PedidoMamografia.MamaLocalizacao != null) && (PedidoMamografia.MamaLocalizacao.Count > 0))
                    {
                        foreach (var ItemMamaLocalizacao in PedidoMamografia.MamaLocalizacao)
                        {
                            ItemMamaLocalizacao.Gravar(PedidoMamografia.seqPedidoAtendimento);
                        }
                    }

                    if ((PedidoMamografia.MamaAchado != null) && (PedidoMamografia.MamaAchado.Count > 0))
                    {
                        foreach (var ItemMamaAchado in PedidoMamografia.MamaAchado)
                        {
                            ItemMamaAchado.Gravar(retorno);
                        }
                    }
                }
                return retorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public long GravarTrans(Framework.Classes.Mamografia PedidoMamografia)
        {
            try
            {
                long retorno = 0;
                // Obter contexto transacionado
                Hcrp.Infra.AcessoDado.Contexto ctx = this.transacao.ctx;

                // Preparar o comando
                Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("PEDIDO_EXAME_HC_MAMOGRAFIA");

                comando.Params["SEQ_PEDIDO_EXAME_MAMOGRAFIA"] = ctx.GetSequenceValue("GENERICO.SEQ_PEDIDO_EXAME_MAMOGRAFIA", true);

                comando.Params["SEQ_PEDIDO_ATENDIMENTO"] = PedidoMamografia.seqPedidoAtendimento;
                comando.Params["IDF_NODULO_CAROCO_MD"] = Convert.ToString(PedidoMamografia.NoduloCarocoMamaDireita);
                comando.Params["IDF_NODULO_CAROCO_ME"] = Convert.ToString(PedidoMamografia.NoduloCarocoMamaEsquerda);
                comando.Params["IDF_RISCO_ELEVADO_CANCER"] = Convert.ToString(PedidoMamografia.RiscoElevadoCancer);
                comando.Params["IDF_EXAME_PREVIO"] = Convert.ToString(PedidoMamografia.ExamePrevio);
                comando.Params["IDF_MAMOGRAFIA_PREVIA"] = Convert.ToString(PedidoMamografia.MamografiaPrevia);
                comando.Params["NUM_ANO_MAMOGRAFIA_PREVIA"] = PedidoMamografia.AnoMamografiaPrevia;
                comando.Params["DTA_EXAME_CLINICO"] = PedidoMamografia.DataExameClinico;
                comando.Params["IDF_TIPO_MAMOGRAFIA"] = Convert.ToString(PedidoMamografia.TipoMamografia);
                comando.Params["IDF_MAMA_MAMOGRAFIA_DIAGN"] = Convert.ToString(PedidoMamografia.MamaMamografiaDiagnostica);
                comando.Params["IDF_TIPO_MAMOGRAFIA_DIAGN"] = Convert.ToString(PedidoMamografia.TipoMamografiaDiagnostica);
                comando.Params["IDF_AVAL_RESP_QT_NEO_ADJ"] = Convert.ToString(PedidoMamografia.AvaliacaoRespostaQT);

                // Executar o insert
                ctx.ExecuteInsert(comando);

                // Pegar o último ID
                retorno = ctx.GetSequenceValue("GENERICO.SEQ_PEDIDO_EXAME_MAMOGRAFIA", false);

                if ((PedidoMamografia.MamaControleLesao != null) && (PedidoMamografia.MamaControleLesao.Count > 0))
                {
                    foreach (var ItemControleLesao in PedidoMamografia.MamaControleLesao)
                    {
                        ItemControleLesao.GravarTrans(this.transacao, retorno);
                    }
                }

                if ((PedidoMamografia.MamaLocalizacao != null) && (PedidoMamografia.MamaLocalizacao.Count > 0))
                {
                    foreach (var ItemMamaLocalizacao in PedidoMamografia.MamaLocalizacao)
                    {
                        ItemMamaLocalizacao.GravarTrans(this.transacao, PedidoMamografia.seqPedidoAtendimento);
                    }
                }

                if ((PedidoMamografia.MamaAchado != null) && (PedidoMamografia.MamaAchado.Count > 0))
                {
                    foreach (var ItemMamaAchado in PedidoMamografia.MamaAchado)
                    {
                        ItemMamaAchado.GravarTrans(this.transacao, retorno);
                    }
                }

                return retorno;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
