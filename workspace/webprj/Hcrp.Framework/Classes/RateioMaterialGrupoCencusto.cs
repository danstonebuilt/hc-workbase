using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class RateioMaterialGrupoCencusto
    {
        private GrupoDeCentroDeCusto _GrupoDeCentrodeCusto;
        public string CodMaterial { get; set; }
        public string CodCenCusto { get; set; }
        public int CodGrupoCentroCusto { get; set; }
        public int PctRateio { get; set; }

        public GrupoDeCentroDeCusto GrupoDeCentrodeCusto
        {
            get
            {
                if (_GrupoDeCentrodeCusto == null)
                    _GrupoDeCentrodeCusto = new Hcrp.Framework.Classes.GrupoDeCentroDeCusto().ObterGrupoComOId(CodGrupoCentroCusto);
                return _GrupoDeCentrodeCusto;
            }
        }

        public RateioMaterialGrupoCencusto() { }

        public List<Hcrp.Framework.Classes.RateioMaterialGrupoCencusto> BuscaRateioMaterialGrupoCencusto(string CodMaterial, string CodCenCusto)
        {
            return new Hcrp.Framework.Dal.RateioMaterialGrupoCencusto().BuscaRateioMaterialGrupoCencusto(CodMaterial, CodCenCusto);
        }

        public void Adicionar(Hcrp.Framework.Classes.RateioMaterialGrupoCencusto _rateio)
        {
            new Hcrp.Framework.Dal.RateioMaterialGrupoCencusto().Adicionar(_rateio);
        }

        public void Remover(Hcrp.Framework.Classes.RateioMaterialGrupoCencusto _rateio)
        {
            new Hcrp.Framework.Dal.RateioMaterialGrupoCencusto().Remover(_rateio);
        }
    }
}
