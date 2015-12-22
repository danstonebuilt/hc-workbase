using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class GrupoPlanoContaItem
    {
        public string Alinea { get; set; }
        public string CodGrupo { get; set; }
        public string NomeGrupo { get; set; }
        public string PlanoConta { get; set; }
        public string ItemPlanoConta { get; set; }

        public GrupoPlanoContaItem() { }

        public List<Hcrp.Framework.Classes.GrupoPlanoContaItem> ObterListaDeGrupoPlanoContaItem(long idPlanoConta)
        {
            return new Hcrp.Framework.Dal.GrupoPlanoContaItem().ObterListaDeGrupoPlanoContaItem(idPlanoConta);
        }

        public List<Hcrp.Framework.Classes.GrupoPlanoContaItem> ObterListaDeGrupoPlanoContaItem(int paginaAtual, out int totalRegistro, string filtroCodAlinea)
        {
            return new Hcrp.Framework.Dal.GrupoPlanoContaItem().ObterListaDeGrupoPlanoContaItem(paginaAtual, out totalRegistro, filtroCodAlinea);
        }

        public void Gravar(List<Hcrp.Framework.Classes.GrupoPlanoContaItem> _grupoPlanoContaItem)
        {
            new Hcrp.Framework.Dal.GrupoPlanoContaItem().Gravar(_grupoPlanoContaItem);
        }
    }
}
