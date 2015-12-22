using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class PesquisaGenerica
    {
        public string CODIGO { get; set; }
        public string DESCRICAO { get; set; }

        public PesquisaGenerica() { }

        public List<Hcrp.Framework.Classes.PesquisaGenerica> ListaPesquisaGenerica(string sql)
        {
            return new Hcrp.Framework.Dal.PesquisaGenerica().ListaPesquisaGenerica(sql);
        }
    }
}
