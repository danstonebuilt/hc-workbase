using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class SubGrupo
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string CodGrupo { get; set; }
        

        public SubGrupo() { }

        public List<Hcrp.Framework.Classes.SubGrupo> ObterListaDeSubGrupo(int paginaAtual, out int totalRegistro, string filtroCodSubGrupo, string filtroNomeSubGrupo, string filtroCodGrupo)
        {
            return new Hcrp.Framework.Dal.SubGrupo().ObterListaDeSubGrupo(paginaAtual, out totalRegistro, filtroCodSubGrupo, filtroNomeSubGrupo, filtroCodGrupo);
        }

    }
}
