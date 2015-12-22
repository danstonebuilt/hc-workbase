using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class ProcedMatCenCusto
    {
        public double Seq { get; set; }
        public double UsuarioCad { get; set; }
        public DateTime DataCadastro { get; set; }
        public string CodMaterial { get; set; }
        public string CodCenCusto { get; set; }
        public double CodProcHC { get; set; }
        public double UsuarioExc { get; set; }
        public DateTime DataExclusao { get; set; }

        public ProcedMatCenCusto() { }

        public List<Hcrp.Framework.Classes.ProcedMatCenCusto> BuscaProcedimentoPorCentroCustoMaterial(string CodMaterial, string CodCenCusto)
        {
            return new Hcrp.Framework.Dal.ProcedMatCenCusto().BuscaProcedimentoPorCentroCustoMaterial(CodMaterial, CodCenCusto);
        }


    }
}
