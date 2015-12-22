using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Dal
{
    public class MamaControleLesao : Hcrp.Framework.Classes.MamaControleLesao
    {
        #region variáveis / construtor

        internal Hcrp.Infra.AcessoDado.TransacaoDinamica transacao;

        public MamaControleLesao()
        {

        }

        public MamaControleLesao(Hcrp.Infra.AcessoDado.TransacaoDinamica _trans)
        {
            transacao = _trans;
        }

        #endregion

        public void Gravar(Framework.Classes.MamaControleLesao ControleLesao, long SeqMamografia)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abrir conexão
                    ctx.Open();

                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("PED_EXAME_MAMA_CONTROLE_LESAO");
                    comando.Params["SEQ_PEDIDO_EXAME_MAMOGRAFIA"] = SeqMamografia;
                    comando.Params["IDF_LADO"] = Convert.ToString(ControleLesao.Lado);
                    comando.Params["IDF_NODULO"] = Convert.ToString(ControleLesao.Nodulo);
                    comando.Params["IDF_MICROCALCIFICACAO"] = Convert.ToString(ControleLesao.Microcalcificacao);
                    comando.Params["IDF_ASSIMETRIA_FOCAL"] = Convert.ToString(ControleLesao.AssimetriaFocal);
                    comando.Params["IDF_ASSIMETRIA_DIFUSA"] = Convert.ToString(ControleLesao.AssimetriaDifusa);
                    comando.Params["IDF_AREA_DENSA"] = Convert.ToString(ControleLesao.AreaDensa);
                    comando.Params["IDF_DISTORCAO_FOCAL"] = Convert.ToString(ControleLesao.DistorcaoFocal);

                    // Executar o insert
                    ctx.ExecuteInsert(comando);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void GravarTrans(Framework.Classes.MamaControleLesao ControleLesao, long SeqMamografia)
        {
            try
            {
                // Obter contexto transacionado
                Hcrp.Infra.AcessoDado.Contexto ctx = this.transacao.ctx;

                // Preparar o comando
                Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("PED_EXAME_MAMA_CONTROLE_LESAO");

                comando.Params["SEQ_PED_EXAME_MAMA_CONTROLE_LE"] = ctx.GetSequenceValue("GENERICO.SEQ_PED_EXAME_MAMA_CONTROLE_LE", true);

                comando.Params["SEQ_PEDIDO_EXAME_MAMOGRAFIA"] = SeqMamografia;
                comando.Params["IDF_LADO"] = Convert.ToString(ControleLesao.Lado);
                comando.Params["IDF_NODULO"] = Convert.ToString(ControleLesao.Nodulo);
                comando.Params["IDF_MICROCALCIFICACAO"] = Convert.ToString(ControleLesao.Microcalcificacao);
                comando.Params["IDF_ASSIMETRIA_FOCAL"] = Convert.ToString(ControleLesao.AssimetriaFocal);
                comando.Params["IDF_ASSIMETRIA_DIFUSA"] = Convert.ToString(ControleLesao.AssimetriaDifusa);
                comando.Params["IDF_AREA_DENSA"] = Convert.ToString(ControleLesao.AreaDensa);
                comando.Params["IDF_DISTORCAO_FOCAL"] = Convert.ToString(ControleLesao.DistorcaoFocal);

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
