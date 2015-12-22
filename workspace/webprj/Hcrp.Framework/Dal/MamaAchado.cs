using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Dal
{
    public class MamaAchado : Hcrp.Framework.Classes.MamaAchado
    {
        #region variáveis / construtor

        internal Hcrp.Infra.AcessoDado.TransacaoDinamica transacao;

        public MamaAchado()
        {

        }

        public MamaAchado(Hcrp.Infra.AcessoDado.TransacaoDinamica _trans)
        {
            transacao = _trans;
        }

        #endregion

        public void Gravar(Framework.Classes.MamaAchado Achado, long SeqMamografia)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abrir conexão
                    ctx.Open();

                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("PED_EXAME_MAMA_ACHADO");
                    comando.Params["SEQ_PEDIDO_EXAME_MAMOGRAFIA"] = SeqMamografia;
                    comando.Params["IDF_LADO"] = Convert.ToString(Achado.Lado);
                    comando.Params["IDF_LESAO_PAPILAR"] = Convert.ToString(Achado.LesaoPapilar);
                    comando.Params["IDF_DESCARGA_PAPILAR"] = Convert.ToString(Achado.DescargaPapilar);
                    comando.Params["IDF_LINFONODO_PALPAVEL_AUXILIA"] = Convert.ToString(Achado.LinfonodoPalpavelAxilar);
                    comando.Params["IDF_LINFONODO_PALPAVEL_SUPRACL"] = Convert.ToString(Achado.LinfonodoPalpavelSupraclavicular);

                    // Executar o insert
                    ctx.ExecuteInsert(comando);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void GravarTrans(Framework.Classes.MamaAchado Achado, long SeqMamografia)
        {
            try
            {
                // Obter contexto transacionado
                Hcrp.Infra.AcessoDado.Contexto ctx = this.transacao.ctx;

                // Preparar o comando
                Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("PED_EXAME_MAMA_ACHADO");

                comando.Params["SEQ_PED_EXAME_MAMA_ACHADO"] = ctx.GetSequenceValue("GENERICO.SEQ_PED_EXAME_MAMA_ACHADO", true);

                comando.Params["SEQ_PEDIDO_EXAME_MAMOGRAFIA"] = SeqMamografia;
                comando.Params["IDF_LADO"] = Convert.ToString(Achado.Lado);
                comando.Params["IDF_LESAO_PAPILAR"] = Convert.ToString(Achado.LesaoPapilar);
                comando.Params["IDF_DESCARGA_PAPILAR"] = Convert.ToString(Achado.DescargaPapilar);
                comando.Params["IDF_LINFONODO_PALPAVEL_AUXILIA"] = Convert.ToString(Achado.LinfonodoPalpavelAxilar);
                comando.Params["IDF_LINFONODO_PALPAVEL_SUPRACL"] = Convert.ToString(Achado.LinfonodoPalpavelSupraclavicular);

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
