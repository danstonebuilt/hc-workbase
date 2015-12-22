using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Dal
{
    public class ObservacaoItemAtendimento : Hcrp.Framework.Classes.ObservacaoItemAtendimento
    {
        public List<Hcrp.Framework.Classes.ObservacaoItemAtendimento> BuscarObservacoesItem(int ItemPedidoAtendimento)
        {
            return null;
        }

        public Boolean Gravar(Hcrp.Framework.Classes.ObservacaoItemAtendimento ip)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abrir conexão
                    ctx.Open();
                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("ITEM_PEDIDO_ATENDIMENTO_OBS");
                    comando.Params["SEQ_ITEM_PEDIDO_ATENDIMENTO"] = ip.ItemPedidoAtendimento.Seq;
                    comando.Params["NUM_USER_BANCO"] = ip._NumUserObservacao;
                    comando.Params["IDF_TIPO_OBSERVACAO"] = (int)ip.TipoObservacao;
                    comando.Params["DTA_HOR_CADASTRO"] = ip.DataCadastro;
                    comando.Params["DSC_OBSERVACAO"] = ip.Descricao;
                    // Executar o insert
                    ctx.ExecuteInsert(comando);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

    }
}
