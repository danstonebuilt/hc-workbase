using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class Unidade
    {
        public double Codigo { get; set; }

        [DefaultValue("")]
        public string Nome { get; set; }

        public string NomeAbrev { get; set; }        

        public Unidade() { }

        public List<Hcrp.Framework.Classes.Unidade> BuscarListaDeUnidade(double codigo, string nome, string nomeabrev)
        {
            return new Hcrp.Framework.Dal.Unidade().BuscarListaDeUnidade(codigo, nome, nomeabrev);
        }

        public Hcrp.Framework.Classes.Unidade BuscaUnidadeNomeAbrev(Int32 codUnid)
        {
            return new Hcrp.Framework.Dal.Unidade().BuscaUnidadeNomeAbrev(codUnid);
        }

        public List<Framework.Classes.Unidade> BuscarListaDeUnidade()
        {
            return new Hcrp.Framework.Dal.Unidade().BuscarListaDeUnidade();
        }

    }
}
