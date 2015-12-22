using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class LocalLeitoUnidadeInternacao
    {
        public string Numero { get; set; }
        public string Nome { get; set; }

        public LocalLeitoUnidadeInternacao()
        { }

        public List<LocalLeitoUnidadeInternacao> BuscaLocalLeitoUnidadeInternacao(string NumSeqLocalEnf)
        {
            return new Hcrp.Framework.Dal.LocalLeitoUnidadeInternacao().BuscaLocalLeitoUnidadeInternacao(NumSeqLocalEnf);
        }
    }
}
