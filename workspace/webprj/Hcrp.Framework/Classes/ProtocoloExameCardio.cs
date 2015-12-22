using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class ProtocoloExameCardio
    {
        public Int64 Codigo { get; set; }
        public ItemPedidoAtendimento ItemPedidoAtendimento { get; set; }
        public Int64 NumeroPedidoConsulta { get; set; }
        public Int64 CodProcedimentoHC { get; set; }
        public string IdfEspecialidadeMedico { get; set; }
        public string IdfNivelAtendimento { get; set; }
        public string IdfAdequacaoEncaminhamento { get; set; }
        public string IdfDestinacaoPaciente { get; set; }
        public string IdfMotivoFalta { get; set; }
    }
}
