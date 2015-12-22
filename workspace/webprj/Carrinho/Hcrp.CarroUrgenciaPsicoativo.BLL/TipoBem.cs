using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.CarroUrgenciaPsicoativo.BLL
{
    public class TipoBem
    {
        public List<Entity.TipoBem> ObterListaTipoBem(string codigoTipoBem, string nomeTipoBem)
        {
            DAL.TipoBem dalTipoBem = new DAL.TipoBem();

            return dalTipoBem.ObterListaTipoBem(codigoTipoBem, nomeTipoBem);
        }
    }
}
