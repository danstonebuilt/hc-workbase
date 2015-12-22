using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class ProtocoloReabilitacaoCIR
    {
        public ItemPedidoAtendimento ItemPedidoAtendimento { get; set; }
        public Int64 SeqItemPedidoAtendimento { get; set; }
        public Int16 IdfDestino { get; set; }
        public Int16 IdfTipoFonoaudiologia { get; set; }

        /// <summary>
        /// Inserir protocolo reabilitação CIR.
        /// </summary>
        /// <param name="protoc"></param>
        public void Inserir(Framework.Classes.ProtocoloReabilitacaoCIR _protocReabCIR)
        {
            new Hcrp.Framework.Dal.ProtocoloReabilitacaoCIR().Inserir(_protocReabCIR);
        }

        /// <summary>
        /// Inserir protocolo reabilitação CIR Transacionado.
        /// </summary>        
        public void InserirTrans(Hcrp.Infra.AcessoDado.TransacaoDinamica transacao, Framework.Classes.ProtocoloReabilitacaoCIR _protocReabCIR)
        {
            new Hcrp.Framework.Dal.ProtocoloReabilitacaoCIR(transacao).InserirTrans(_protocReabCIR);
        }
    }
}
