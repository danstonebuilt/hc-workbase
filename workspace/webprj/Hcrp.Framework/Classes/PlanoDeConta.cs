using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class PlanoDeConta
    {
        public long IdPlanoDeConta { get; set; }
        public Int32 IdInstituicao { get; set; }
        public string NomeDoPlanoDeConta { get; set; }

        public PlanoDeConta() { }

        public List<Hcrp.Framework.Classes.PlanoDeConta> ObterListaDePlanoDeConta()
        {
            return new Hcrp.Framework.Dal.PlanoDeConta().ObterListaDePlanoDeConta();
        }
    }
}
