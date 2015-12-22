using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class GrupoLocal
    {
        public long IdCentroCustoLocal { get; set; }
        public Local Local { get; set; }
        public string IdCentroLocal { get; set; }

        public GrupoLocal()
        {
            this.Local = new Local();
        }

        public List<Hcrp.Framework.Classes.GrupoLocal> ObterListaDeLocalPorCentroDeCusto(string idCentroDeCusto, int paginaAtual, out int totalRegistro)
        {
            return new Hcrp.Framework.Dal.GrupoLocal().ObterListaDeLocalPorCentroDeCusto(idCentroDeCusto, paginaAtual, out totalRegistro);
        }

        public bool JahExisteOGrupoLocal(Hcrp.Framework.Classes.GrupoLocal _grupoLocal)
        {
            return new Hcrp.Framework.Dal.GrupoLocal().JahExisteOGrupoLocal(_grupoLocal);
        }

        public void Adicionar(Hcrp.Framework.Classes.GrupoLocal _grupoLocal)
        {
            new Hcrp.Framework.Dal.GrupoLocal().Adicionar(_grupoLocal);
        }

        public void Remover(long idRelacionamento)
        {
            new Hcrp.Framework.Dal.GrupoLocal().Remover(idRelacionamento);
        }
    }
}
