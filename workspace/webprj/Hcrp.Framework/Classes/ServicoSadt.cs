using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class ServicoSadt
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public ESistemaAgendamento SistemaAgendamento { get; set; }

        public enum ESistemaAgendamento
        {
            Radiologia2 = 1,
            Endoscopia = 2,
            Cardiologia = 3,
            Cardiologia2 = 4,
            Broncofibroscopia = 5
        }

        public ServicoSadt()
        { }

        public Hcrp.Framework.Classes.ServicoSadt BuscaServicoSadtCodigo(int codServico)
        {
            return new Hcrp.Framework.Dal.ServicoSadt().BuscaServicoSadtCodigo(codServico);
        }

        public Hcrp.Framework.Classes.ServicoSadt BuscaServicoSadtPedAtendimento(int seqPedidoAtendimento)
        {
            return new Hcrp.Framework.Dal.ServicoSadt().BuscaServicoSadtPedAtendimento(seqPedidoAtendimento);
        }

        public List<Hcrp.Framework.Classes.ServicoSadt> BuscarServicoSadt()
        {
            return new Hcrp.Framework.Dal.ServicoSadt().BuscarServicoSadt();       
        }
    }
}
