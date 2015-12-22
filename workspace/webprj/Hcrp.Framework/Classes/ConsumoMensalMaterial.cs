using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Hcrp.Framework.Classes
{
    public class ConsumoMensalMaterial
    {        
        public Material _Material;
        public CentroDeCusto _CentrodeCusto;
        public GrupoDeCentroDeCusto _GrupoDeCentrodeCusto;
        public Unidade _Unidade;

        public string _CodMaterial;
        public string _CodCenCusto;
        public int _CodGrupoCentroCusto;
        public Int32 _CodUnidade;
        

        public Material Material
        {
            get
            {
                if (_Material == null)
                    _Material = new Hcrp.Framework.Classes.Material().BuscaMaterialCodigo(_CodMaterial);
                return _Material;
            }
        }

        public GrupoDeCentroDeCusto GrupoDeCentrodeCusto{ 
            get {
                if (_GrupoDeCentrodeCusto == null)
                    _GrupoDeCentrodeCusto = new Hcrp.Framework.Classes.GrupoDeCentroDeCusto().ObterGrupoComOId(_CodGrupoCentroCusto);
                return _GrupoDeCentrodeCusto;
            }        
        }

        public Unidade Unidade
        {
            get
            {
                if (_Unidade == null)
                    _Unidade = new Hcrp.Framework.Classes.Unidade().BuscaUnidadeNomeAbrev(_CodUnidade);
                return _Unidade;
            }
        }

        public CentroDeCusto CentrodeCusto
        {
            get
            {
                if (_CentrodeCusto == null)
                    _CentrodeCusto = new Hcrp.Framework.Classes.CentroDeCusto().BuscaCentroCustoCodigo(_CodCenCusto);
                return _CentrodeCusto;
            }
        }

        public string Ano { get; set; }
        public Int32 Mes { get; set; }
        public string PlanoConta { get; set; }
        public string Apropriar { get; set; }
        public double QtdDispensada { get; set; }
        public double QtdConsumida { get; set; }
        public double QtdEstoque { get; set; }
        public double VlrCustoMedio { get; set; }
        public double QtdConsuProgramacao { get; set; }
        public double QtdEntrada { get; set; }
        public double VlrTotalEntrada { get; set; }
        public double VlrTotalConsumo { get; set; }
        public double QtdEstoqueContabil { get; set; }
        public double QtdConsumidaContabil { get; set; }
        public double QtdProgramadaAno { get; set; }
        public double QtdSaldoRestante { get; set; }
        public double NumPeriodicidade { get; set; }
        public double IdfCurvaVen { get; set; }
        public DateTime DtaReferencia { get; set; }

        public ConsumoMensalMaterial() { }

        public List<Hcrp.Framework.Classes.ConsumoMensalMaterial> BuscarConsumoMaterialPorAlinea(int classe, string ano, int ordem, int tipo, string codcencusto)
        {
            return new Hcrp.Framework.Dal.ConsumoMensalMaterial().BuscarConsumoMaterialPorAlinea(classe, ano, ordem, tipo, codcencusto);
        }

        public List<Hcrp.Framework.Classes.ConsumoMensalMaterial> BuscarConsumoMaterialPorItemPlanoContaGrupo(string itemPlanoConta, string ano, int ordem)
        {
            return new Hcrp.Framework.Dal.ConsumoMensalMaterial().BuscarConsumoMaterialPorItemPlanoContaGrupo(itemPlanoConta, ano, ordem);
        }

        public List<Hcrp.Framework.Classes.ConsumoMensalMaterial> BuscarConsumoMaterialPorAlineaCC(int classe, string ano, int ordem, string codgrupo)
        {
            return new Hcrp.Framework.Dal.ConsumoMensalMaterial().BuscarConsumoMaterialPorAlineaCC(classe, ano, ordem, codgrupo);
        }

        public List<Hcrp.Framework.Classes.ConsumoMensalMaterial> BuscarConsumoMaterialPorItemPlanoContaCC(string itemPlanoConta, string ano, int ordem, string codgrupo)
        {
            return new Hcrp.Framework.Dal.ConsumoMensalMaterial().BuscarConsumoMaterialPorItemPlanoContaCC(itemPlanoConta, ano, ordem, codgrupo);
        }

        public List<Hcrp.Framework.Classes.ConsumoMensalMaterial> BuscarConsumoMaterialPorAlineaMaterial(int classe, string ano, int ordem, string codcencusto, string material)
        {
            return new Hcrp.Framework.Dal.ConsumoMensalMaterial().BuscarConsumoMaterialPorAlineaMaterial(classe, ano, ordem, codcencusto, material);
        }
    }
}
