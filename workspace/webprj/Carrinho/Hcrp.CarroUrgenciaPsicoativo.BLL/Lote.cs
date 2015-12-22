using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.CarroUrgenciaPsicoativo.BLL
{
    public class Lote
    {
        #region Métodos

        /// <summary>
        /// Obter por id.
        /// </summary>        
        public Entity.Lote ObterPorId(Int64 numLote)
        {
            return new DAL.Lote().ObterPorId(numLote);
        }

        #region Métodos

        /// <summary>
        /// Obter por codigo do material.
        /// </summary>        
        public List<Entity.Lote> ObterPorCodMaterial(string codMaterial)
        {
            return new DAL.Lote().ObterPorCodMaterial(codMaterial);
        }

        #endregion

        #endregion
    }
}
