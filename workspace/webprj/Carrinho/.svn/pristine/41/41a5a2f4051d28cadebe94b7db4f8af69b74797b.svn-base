using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.CarroUrgenciaPsicoativo.BLL
{
    public class IdentificacaoLocal
    {
        #region Recuperação de Informação [FB, MO, AJSO]
        public static Hcrp.CarroUrgenciaPsicoativo.Entity.IdentificacaoLocal getIdentificacaoLocal(long pNumID)
        {
            return Hcrp.CarroUrgenciaPsicoativo.DAL.IdentificacaoLocal.getIdentificacaoLocal(pNumID);
        }

        public static List<Hcrp.CarroUrgenciaPsicoativo.Entity.IdentificacaoLocal> getIdentificacaoLocal(string pDscIDLocal = "", long pNumID = 0)
        {
            return Hcrp.CarroUrgenciaPsicoativo.DAL.IdentificacaoLocal.getIdentificacaoLocal(pDscIDLocal, pNumID);
        }

        #endregion


        #region Gravação [FB, MO, AJSO]
        public static bool setIdentificacaoLocal(ref string pOutError, Hcrp.CarroUrgenciaPsicoativo.Entity.IdentificacaoLocal pObjetoGravar, bool pControlarTransacao = true)
        {
            bool result = false;
            pOutError = string.Empty;
            try
            {

                if (string.IsNullOrEmpty(pObjetoGravar.dsc_id_local))
                {
                    throw new Exception("A descrição da identificação de local é obrigatória.");
                }

                pObjetoGravar.dsc_id_local = pObjetoGravar.dsc_id_local.ToUpper().Trim();

                result = Hcrp.CarroUrgenciaPsicoativo.DAL.IdentificacaoLocal.setIdentificacaoLocal(ref pOutError, pObjetoGravar, pControlarTransacao);

            }
            catch (Exception err)
            {
                pOutError = err.Message;
                result = false;
            }

            return (result);
        }
        #endregion

    }
}
