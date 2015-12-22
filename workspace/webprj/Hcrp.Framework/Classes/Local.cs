using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class Local
    {
        public string IdLocal { get; set; }
        public string NomeLocal { get; set; }

        public Local() { }

        public List<Hcrp.Framework.Classes.Local> ObterListaDeGrupoDeCentroDeCusto(int paginaAtual, out int totalRegistro, string filtroIdLocal, string filtroNomeLocal)
        {
            return new Hcrp.Framework.Dal.Local().ObterListaDeGrupoDeCentroDeCusto(paginaAtual, out totalRegistro, filtroIdLocal, filtroNomeLocal);
        }
    }
}
