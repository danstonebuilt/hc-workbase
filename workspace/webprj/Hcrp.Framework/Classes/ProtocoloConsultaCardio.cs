using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class ProtocoloConsultaCardio
    {
        public Int64 Codigo { get; set; }
        public ItemPedidoAtendimento ItemPedidoAtendimento { get; set; }
        public string IdfNecessidadeAvalEspec { get; set; }
        public string IdfNecessidadeSegClinico { get; set; }
        public string IdfNecessidadeAtendUrgente { get; set; }
        public string IdfEspecialidadeMedico { get; set; }
        public string IdfNivelAtendimento { get; set; }
        public string IdfAdequacaoEncaminhamento { get; set; }
        public string IdfDestinacaoPaciente { get; set; }
        public string IdfMotivoFalta { get; set; }
    }
}
