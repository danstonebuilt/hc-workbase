using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.BLL
{
    public class TipoAtendimento
    {

        /// <summary>
        /// Método para popular os objetos DropDownList
        /// </summary>
        /// <param name="pCodInstituto">Código do Instituto</param>        
        /// <returns></returns>
        public List<Hcrp.Framework.Entity.TipoAtendimento> getTipoAtendimentoDDL(int pCodInstituto)
        {
            return Dal.TipoAtendimento.getTipoAtendimentoDDL(pCodInstituto);
        }

    }
}
