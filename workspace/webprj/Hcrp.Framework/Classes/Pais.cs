using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class Pais
    {
        public string Sigla { get; set; }
        public string Nome { get; set; }

        public Pais()
        { }

        public Pais BuscaPaisSigla(string sigla)
        {
            return new Hcrp.Framework.Dal.Pais().BuscaPaisSigla(sigla);
        }
        public List<Pais> BuscaPais()
        {
            return new Hcrp.Framework.Dal.Pais().BuscaPais();
        }
        
    }
}
