using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class CentroDeCusto
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public Int32? CodigoGrupo { get; set; }

        public CentroDeCusto() { }

        public List<Hcrp.Framework.Classes.CentroDeCusto> ObterListaDeCentroRelacionadosAoGrupo(Int32 idGrupo, int paginaAtual, out int totalRegistro, string filtroId, string filtroNome)
        {
            return new Hcrp.Framework.Dal.CentroDeCusto().ObterListaDeCentroRelacionadosAoGrupo(idGrupo, paginaAtual, out totalRegistro, filtroId, filtroNome);
        }

        public Hcrp.Framework.Classes.CentroDeCusto BuscaCentroCustoCodigo(string codCC)
        {
            return new Hcrp.Framework.Dal.CentroDeCusto().BuscaCentroCustoCodigo(codCC);
        }

        public List<Hcrp.Framework.Classes.CentroDeCusto> ObterListaDeCentroRelacionadosAoGrupoSICH(Int32 idGrupo, int paginaAtual, out int totalRegistro, string filtroId, string filtroNome)
        {
            return new Hcrp.Framework.Dal.CentroDeCusto().ObterListaDeCentroRelacionadosAoGrupoSICH(idGrupo, paginaAtual, out totalRegistro, filtroId, filtroNome);
        }

        public List<Hcrp.Framework.Classes.CentroDeCusto> ObterListaDeCentroSemRelacionamento(int paginaAtual, string ordenacao, out int totalRegistro, string filtroId, string filtroNome)
        {
            return new Hcrp.Framework.Dal.CentroDeCusto().ObterListaDeCentroSemRelacionamento(paginaAtual, ordenacao, out totalRegistro, filtroId, filtroNome);
        }

        public Hcrp.Framework.Classes.CentroDeCusto ObterCentroDeCustoComOId(string idCentroDeCusto)
        {
            return new Hcrp.Framework.Dal.CentroDeCusto().ObterCentroDeCustoComOId(idCentroDeCusto);
        }

        public void Adicionar(Hcrp.Framework.Classes.CentroDeCusto _centro)
        {
            new Hcrp.Framework.Dal.CentroDeCusto().Adicionar(_centro);
        }

        public void Remover(Hcrp.Framework.Classes.CentroDeCusto _centro)
        {
            new Hcrp.Framework.Dal.CentroDeCusto().Remover(_centro);
        }
    }
}
