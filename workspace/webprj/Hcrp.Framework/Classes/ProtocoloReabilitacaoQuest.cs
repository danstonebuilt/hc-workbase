using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class ProtocoloReabilitacaoQuest
    {
        public ItemPedidoAtendimento ItemPedidoAtendimento { get; set; }
        public Int64 SeqItemPedidoAtendimento { get; set; }
        public Int16 IdfPressaoArterial { get; set; }
        public Int16 IdfDiabete { get; set; }
        public Int16 IdfCriseConvulsiva { get; set; }
        public Int16 IdfCondicaoRespiratoria { get; set; }
        public Int16 IdfViaAlimentacao { get; set; }
        public Int16 IdfFuncaoAuditiva { get; set; }
        public Int16 IdfFuncaoComunicativa { get; set; }
        public Int16 IdfFuncaoVisual { get; set; }
        public Int16 IdfCondicaoPele { get; set; }
        public Int16 IdfPosCirurgico { get; set; }
        public Int16 IdfLocomocao { get; set; }
        public Int16 IdfEquilibrio { get; set; }
        public Int16 IdfAlteracaoPostural { get; set; }
        public Int16 IdfAmputacao { get; set; }
        public Int16 IdfLesaoMedular { get; set; }
        public Int16 IdfFuncaoManual { get; set; }
        public Int16 IdfFuncaoCognitiva { get; set; }
        public Int16 IdfDemencia { get; set; }
        public Int16 IdfDificuldadeEscolar { get; set; }
        public Int16 IdfBebeRisco { get; set; }

        /// <summary>
        /// Inserir protocolo reabilitação questionário.
        /// </summary>
        /// <param name="protoc"></param>
        public void Inserir(Framework.Classes.ProtocoloReabilitacaoQuest _protocReabQuest)
        {
            new Hcrp.Framework.Dal.ProtocoloReabilitacaoQuest().Inserir(_protocReabQuest);
        }

        /// <summary>
        /// Inserir protocolo reabilitação questionário transacionado.
        /// </summary>        
        public void InserirTrans(Hcrp.Infra.AcessoDado.TransacaoDinamica transacao, Framework.Classes.ProtocoloReabilitacaoQuest _protocReabQuest)
        {
            new Hcrp.Framework.Dal.ProtocoloReabilitacaoQuest(transacao).InserirTrans(_protocReabQuest);
        }
    }
}
