using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class UnidadeFederacao
    {
        public Pais Pais { get; set; }
        public string Sigla { get; set; }
        public string Nome { get; set; }
        public int CodigoIbge { get; set; }

        public UnidadeFederacao()
        { }

        public UnidadeFederacao BuscaUFSigla(string sigla)
        {
            return new Hcrp.Framework.Dal.UnidadeFederacao().BuscaUFSigla(sigla);
        }

        public List<UnidadeFederacao> BuscaUF()
        {
            return new Hcrp.Framework.Dal.UnidadeFederacao().BuscaUF();
        }

        public List<UnidadeFederacao> BuscaUFPais(string sigla_pais)
        {
            return new Hcrp.Framework.Dal.UnidadeFederacao().BuscaUFPais(sigla_pais);
        }
    }
}
