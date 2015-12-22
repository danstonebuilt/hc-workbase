using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class ItemPedidoAtendimentoObs
    {
        public Int64 Seq { get; set; }
        public Int64 SeqItemPedidoAtendimento { get; set; }
        public Usuario Usuario { get; set; }
        public int IdfTipoObs { get; set; }
        public DateTime DataHoraCadastro { get; set; }
        public string DescObservacao { get; set; }
    }
}
