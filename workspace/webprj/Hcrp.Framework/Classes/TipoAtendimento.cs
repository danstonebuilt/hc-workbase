using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class TipoAtendimento
    {

        public int Codigo {get; set;}
        public String Nome {get; set;}
        public String NomeAbreviado { get; set; }

        public TipoAtendimento BuscarTipoAtendimentoCodigo(int codigo)
        {
            return new Hcrp.Framework.Dal.TipoAtendimento().BuscarTipoAtendimentoCodigo(codigo);
        }
    }
}
