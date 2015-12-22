using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class GrupoDeCentroDeCusto
    {
        List<Hcrp.Framework.Classes.ConsumoMensalMaterial> _ConsumoMensalGrupo;

        public Int32 Codigo { get; set; }
        public string Nome { get; set; }
        public string OrdenacaoImpressao { get; set; }
        public string IdfTipoGrupo { get; set; }
        public string DescricaoTipoGrupo { get; set; }
        public Int32 CodigoInstituicao { get; set; }

        public GrupoDeCentroDeCusto() { }

        public List<Hcrp.Framework.Classes.GrupoDeCentroDeCusto> ObterListaDeGrupoDeCentroDeCusto(int paginaAtual, out int totalRegistro, string filtroIdGrupo, string filtroNomeGrupo, string filtroIdTipoGrupo) 
        {
            return new Hcrp.Framework.Dal.GrupoDeCentroDeCusto().ObterListaDeGrupoDeCentroDeCusto(paginaAtual, out totalRegistro,filtroIdGrupo,filtroNomeGrupo,filtroIdTipoGrupo);
        }

        public Hcrp.Framework.Classes.GrupoDeCentroDeCusto BuscaGrupoCentroCustoCodigo(int codGrupoCC)
        {
            return new Hcrp.Framework.Dal.GrupoDeCentroDeCusto().BuscaGrupoCentroCustoCodigo(codGrupoCC);
        }

        public List<Hcrp.Framework.Classes.GrupoDeCentroDeCusto> ObterListaDeGrupoSICHDeCentroDeCusto(int paginaAtual, out int totalRegistro, string filtroIdGrupo, string filtroNomeGrupo, string filtroIdTipoGrupo)
        {
            return new Hcrp.Framework.Dal.GrupoDeCentroDeCusto().ObterListaDeGrupoSICHDeCentroDeCusto(paginaAtual, out totalRegistro, filtroIdGrupo, filtroNomeGrupo, filtroIdTipoGrupo);
        }

        public Hcrp.Framework.Classes.GrupoDeCentroDeCusto ObterGrupoComOId(int idGrupo)
        {
            return new Hcrp.Framework.Dal.GrupoDeCentroDeCusto().ObterGrupoComOId(idGrupo);
        }

        public void Adicionar(Hcrp.Framework.Classes.GrupoDeCentroDeCusto _grupo)
        {
            new Hcrp.Framework.Dal.GrupoDeCentroDeCusto().Adicionar(_grupo);
        }

        public void Atualizar(Hcrp.Framework.Classes.GrupoDeCentroDeCusto _grupo)
        {
            new Hcrp.Framework.Dal.GrupoDeCentroDeCusto().Atualizar(_grupo);
        }

        public void Remover(Hcrp.Framework.Classes.GrupoDeCentroDeCusto _grupo)
        {
            new Hcrp.Framework.Dal.GrupoDeCentroDeCusto().Remover(_grupo);
        }
    }
}
