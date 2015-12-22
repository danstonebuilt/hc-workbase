using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class Alinea
    {
        List<Hcrp.Framework.Classes.ConsumoMensalMaterial> ConsumoMensalAlinea;
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public Int32 IdfClasse { get; set; }
        public Alinea() { }

        public List<Hcrp.Framework.Classes.Alinea> ObterListaDeAlinea()
        {
            return new Hcrp.Framework.Dal.Alinea().ObterListaDeAlinea();
        }

        public List<Hcrp.Framework.Classes.Alinea> ObterListaDeAlinea(int paginaAtual, out int totalRegistro, string filtroCodAlinea, string filtroNomeAlinea, string filtroSglAlinea, string filtroIdfClasse)
        {
            return new Hcrp.Framework.Dal.Alinea().ObterListaDeAlinea(paginaAtual, out totalRegistro, filtroCodAlinea, filtroNomeAlinea, filtroSglAlinea, filtroIdfClasse);
        }
    }
}
