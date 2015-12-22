using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class ProtocoloReabilitacaoCIRFon
    {
        public ItemPedidoAtendimento ItemPedidoAtendimento { get; set; }
        public Int64 SeqItemPedidoAtendimento { get; set; }
        public Int16 IdfTipoCaso { get; set; }
        
        public string IdfOccAltFala { get; set; }
        public string IdfOccDisfagiaLeve { get; set; }
        public string IdfOccAltEscrita { get; set; }
        public string IdfOccComunicacao { get; set; }

        public string IdfMoRespiradorOral { get; set; }
        public string IdfMoAltFala { get; set; }
        public string IdfMoAltMastDegl { get; set; }
        public string IdfMoCasosOrtodonticos { get; set; }
        public string IdfMoDisfagiaLeve { get; set; }

        public string IdfGagueira { get; set; }
        public string IdfComunicacaoAlternativa { get; set; }
        public string IdfAltercaoVozDisfonia { get; set; }

        //public string IdfAltVoz { get; set; }
        public string IdfDefAuditiva { get; set; }
        public string IdfDefProcAuditivo { get; set; }

        /// <summary>
        /// Inserir protocolo reabilitação CIR Fonoaudiologia.
        /// </summary>
        /// <param name="protoc"></param>
        public void Inserir(Framework.Classes.ProtocoloReabilitacaoCIRFon _protocReabCIRFon)
        {
            new Hcrp.Framework.Dal.ProtocoloReabilitacaoCIRFon().Inserir(_protocReabCIRFon);
        }

        /// <summary>
        /// Inserir protocolo reabilitação CIR Fonoaudiologia transacionado.
        /// </summary>        
        public void InserirTrans(Hcrp.Infra.AcessoDado.TransacaoDinamica transacao, Framework.Classes.ProtocoloReabilitacaoCIRFon _protocReabCIRFon)
        {
            new Hcrp.Framework.Dal.ProtocoloReabilitacaoCIRFon(transacao).InserirTrans(_protocReabCIRFon);
        }
    }
}
