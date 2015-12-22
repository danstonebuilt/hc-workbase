using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class ConsumoMaterialCCMaterial
    {
        public string Codigo { get; set; }
        public string Material { get; set; }
        public string Unidade { get; set; }
        public string Plano { get; set; }
        public double JanValor { get; set; }
        public double JanQtd { get; set; }
        public double JanProg { get; set; }
        public double FevValor { get; set; }
        public double FevQtd { get; set; }
        public double FevProg { get; set; }
        public double MarValor { get; set; }
        public double MarQtd { get; set; }
        public double MarProg { get; set; }
        public double AbrValor { get; set; }
        public double AbrQtd { get; set; }
        public double AbrProg { get; set; }
        public double MaiValor { get; set; }
        public double MaiQtd { get; set; }
        public double MaiProg { get; set; }
        public double JunValor { get; set; }
        public double JunQtd { get; set; }
        public double JunProg { get; set; }
        public double JulValor { get; set; }
        public double JulQtd { get; set; }
        public double JulProg { get; set; }
        public double AgoValor { get; set; }
        public double AgoQtd { get; set; }
        public double AgoProg { get; set; }
        public double SetValor { get; set; }
        public double SetQtd { get; set; }
        public double SetProg { get; set; }
        public double OutValor { get; set; }
        public double OutQtd { get; set; }
        public double OutProg { get; set; }
        public double NovValor { get; set; }
        public double NovQtd { get; set; }
        public double NovProg { get; set; }
        public double DezValor { get; set; }
        public double DezQtd { get; set; }
        public double DezProg { get; set; }
        public double TotalValor { get; set; }
        public double TotalQtd { get; set; }
        public double TotalProg { get; set; }

        public ConsumoMaterialCCMaterial() { }

        public List<Hcrp.Framework.Classes.ConsumoMaterialCCMaterial> BuscarConsumo(bool paginacao, int paginaAtual, out int totalRegistro, int classe, string ano, string codCentroCusto, string codMaterial, string planoConta, string itemPlanoConta, string sortExpression, string sortDirection)
        {
            return new Hcrp.Framework.Dal.ConsumoMaterialCCMaterial().BuscarConsumo(paginacao, paginaAtual, out totalRegistro, classe, ano, codCentroCusto, codMaterial, planoConta, itemPlanoConta, sortExpression, sortDirection);
        }
    }
}

