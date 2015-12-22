using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class Grupo
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public Int32 CodigoAlinea { get; set; }

        public Grupo() { }

        public Hcrp.Framework.Classes.Grupo BuscaGrupoCodigo(string codGrupo)
        {
            return new Hcrp.Framework.Dal.Grupo().BuscaGrupoCodigo(codGrupo);
        }

        public List<Hcrp.Framework.Classes.Grupo> ObterListaDeGrupo(int paginaAtual, out int totalRegistro, string filtroCodGrupo, string filtroNomeGrupo, string filtroCodAlinea)
        {
            return new Hcrp.Framework.Dal.Grupo().ObterListaDeGrupo(paginaAtual, out totalRegistro, filtroCodGrupo, filtroNomeGrupo, filtroCodAlinea);
        }

    }
}
