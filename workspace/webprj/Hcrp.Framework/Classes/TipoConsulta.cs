using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class TipoConsulta
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }

        public TipoConsulta()
        { }
        
        public TipoConsulta BuscaTipoConsultaCodigo(int codTipoConsulta)
        {
            return new Hcrp.Framework.Dal.TipoConsulta().BuscaTipoConsultaCodigo(codTipoConsulta);
        }
    }
}
