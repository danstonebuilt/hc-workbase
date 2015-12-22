using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class Motivo
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }

        public Motivo() { }

        public Hcrp.Framework.Classes.Motivo BuscarMotivoCodigo(int codMotivo)
        {
            return new Hcrp.Framework.Dal.Motivo().BuscarMotivoCodigo(codMotivo);
        }
    }
}
