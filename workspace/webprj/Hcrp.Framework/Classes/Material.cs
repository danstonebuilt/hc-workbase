using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Hcrp.Framework.Classes
{
    public class Material
    {
        private Grupo _Grupo;

        [DefaultValue("")]
        public string Codigo { get; set; }

        [DefaultValue("")]
        public string Nome { get; set; }

        public string CodigoGrupo { get; set; }
        public string CodigoSubGrupo { get; set; }
        public double ApropSeq { get; set; }
        public string ApropUsuarioCad { get; set; }
        public DateTime ApropDataCadastro { get; set; }
        public string ApropCodCenCusto { get; set; }
        public string ApropUsuarioExc { get; set; }
        public DateTime ApropDataExclusao { get; set; }
        public string TipoDeItem { get; set; }

        public Grupo Grupo
        {
            get
            {
                if (_Grupo == null)
                    _Grupo = new Hcrp.Framework.Classes.Grupo().BuscaGrupoCodigo(CodigoGrupo);
                return _Grupo;
            }
        }

        public Material() { }

        public List<Hcrp.Framework.Classes.Material> ObterListaDeMaterial(int paginaAtual, out int totalRegistro, string filtroCodMaterial, string filtroNomeMaterial, string filtroCodGrupo, string filtroCodSubGrupo)
        {
            return new Hcrp.Framework.Dal.Material().ObterListaDeMaterial(paginaAtual, out totalRegistro, filtroCodMaterial, filtroNomeMaterial, filtroCodGrupo, filtroCodSubGrupo);
        }
        public Hcrp.Framework.Classes.Material BuscaMaterialCodigo(string codMat)
        {
            return new Hcrp.Framework.Dal.Material().BuscaMaterialCodigo(codMat);
        }
        public List<Hcrp.Framework.Classes.Material> BuscaMaterialNaoApropriado(string CodMaterial, string CodCenCusto)
        {
            return new Hcrp.Framework.Dal.Material().BuscaMaterialNaoApropriado(CodMaterial, CodCenCusto);
        }
        public long InserirMatNaoAprop(Framework.Classes.Material Material)
        {
            return new Hcrp.Framework.Dal.Material().InserirMatNaoAprop(Material);
        }
        public double AlterarMatNaoAprop(Framework.Classes.Material Material)
        {
            return new Hcrp.Framework.Dal.Material().AlterarMatNaoAprop(Material);
        }
    }
}
