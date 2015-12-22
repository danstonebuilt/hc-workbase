using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Hcrp.Framework.Classes
{
    public class MotivoEncaminhamento
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }

        public MotivoEncaminhamento() { }

        public Hcrp.Framework.Classes.MotivoEncaminhamento BuscarMotivoEncaminhamentoCodigo(int codMotivoEncaminhamento)
        {
            return new Hcrp.Framework.Dal.MotivoEncaminhamento().BuscarMotivoEncaminhamentoCodigo(codMotivoEncaminhamento);
        }
    }
}
