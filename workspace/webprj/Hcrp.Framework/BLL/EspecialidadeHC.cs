using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.BLL
{
    public class EspecialidadeHC
    {

        #region Métodos para recuperação de informação [FB]
        /// <summary>
        /// Retorna lista de especialidade de um instituto e tipo de atendimento e sigla (se informado)
        /// </summary>
        /// <param name="pCodInstituto">Código do instituto</param>
        /// <param name="pCodTipoAtendimento">Código do tipo atendimento</param>
        /// <param name="pSglEspecialidadeHCExato">Sigla da Especialidade HC</param>
        /// <returns>Lista de Entity.EspecialidadeHC</returns>
        
        public List<Hcrp.Framework.Entity.EspecialidadeHC> getEspecialidadeDDL(int? pCodInstituto, int? pCodTipoAtendimento, string pSglEspecialidadeHC = "")
        {
            return Dal.Especialidade.getEspecialidadeDDL(pCodInstituto, pCodTipoAtendimento, pSglEspecialidadeHC);
        }

        public static List<Hcrp.Framework.Entity.EspecialidadeHC> getEspecialidadeGrid(int pCodInstituto, int pCodTipoAtendimento, string pSglEspecialidadeHCLike = "", string pNomEspecialidadeHCLike = "") 
        {
            return Dal.Especialidade.getEspecialidadeGrid(pCodInstituto, pCodTipoAtendimento, pSglEspecialidadeHCLike, pNomEspecialidadeHCLike );
        }
        #endregion

        #region Métodos para manutenção de dados [FB]

        #endregion

    }
}
