using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class ConsumoMaterialCentroCusto
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Grupo { get; set; }
        public double Janeiro { get; set; }
        public double Fevereiro { get; set; }
        public double Marco { get; set; }
        public double Abril { get; set; }
        public double Maio { get; set; }
        public double Junho { get; set; }
        public double Julho { get; set; }
        public double Agosto { get; set; }
        public double Setembro { get; set; }
        public double Outubro { get; set; }
        public double Novembro { get; set; }
        public double Dezembro { get; set; }
        public double Total { get; set; }

        public ConsumoMaterialCentroCusto() { }

        public List<Hcrp.Framework.Classes.ConsumoMaterialCentroCusto> BuscarConsumo(bool paginacao, int paginaAtual, out int totalRegistro, string planoConta, string itemPlanoConta, string ano, string codGrupo, string sortExpression, string sortDirection)
        {
            return new Hcrp.Framework.Dal.ConsumoMaterialCentroCusto().BuscarConsumo(paginacao, paginaAtual, out totalRegistro, planoConta, itemPlanoConta, ano, codGrupo, sortExpression, sortDirection);
        }
    }
}
